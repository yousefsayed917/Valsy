using BuildingBlocks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;
using Valsy.Application.Common.Abstracts;
using Valsy.Application.Common.Exceptions;
using Valsy.Application.Common.Interfaces;

namespace BuildingBlocks.Infrastructure.UnitOfWork
{
    public class UnitOfWorkCommandHandlerDecorator<T> : IRequestHandler<T>
        where T : CommandBase
    {
        private readonly IRequestHandler<T> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UnitOfWorkCommandHandlerDecorator<T>> _logger;

        public UnitOfWorkCommandHandlerDecorator(
            IRequestHandler<T> decorated,
            IUnitOfWork unitOfWork, AppSettings appSettings, ILogger<UnitOfWorkCommandHandlerDecorator<T>> logger)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _logger = logger;
        }
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            // Retry policy
            AsyncRetryPolicy retryPolicy = Policy
                .Handle<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(
                    retryCount: int.Parse(_appSettings.PipelineConfig.RetryConfig.RetryCount),
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(double.Parse(_appSettings.PipelineConfig.RetryConfig.SleepDuration)),
                    onRetry: (exception, delay, retryCount, context) =>
                    {
                        _logger.LogInformation($"Retry {retryCount} due to concurrency issue: {exception.Message}");
                    });

            // Fallback policy if all retries fail
            AsyncFallbackPolicy<bool> fallbackPolicy = Policy<bool>
                .Handle<DbUpdateConcurrencyException>()
                .FallbackAsync(
                    fallbackAction: (ct) =>
                    {
                        RetryFailedException retryFailedException = new RetryFailedException();
                        _logger.LogError(retryFailedException, retryFailedException.Message);

                        throw retryFailedException;
                    },
                    onFallbackAsync: async (delegateResult) => // Only one parameter here
                    {
                        _logger.LogError(delegateResult.Exception, $"Fallback triggered due to: {delegateResult.Exception?.Message}");
                        await Task.CompletedTask;
                    });

            // Combine
            AsyncPolicyWrap<bool> combinedPolicy = fallbackPolicy.WrapAsync(retryPolicy);

            // Execute with retry + fallback
            await combinedPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    if (command is not INonDbCommand)
                        await _unitOfWork.BeginAsync();

                    await _decorated.Handle(command, cancellationToken);

                    if (command is not INonDbCommand)
                        await _unitOfWork.CommitAsync(cancellationToken);

                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await RollbackAsync(command, ex);

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync();
                    }

                    throw new DbUpdateConcurrencyException(ex.Message, ex); // Rethrow so Polly retries
                }
                catch (DbUpdateException ex)
                {
                    await RollbackAsync(command, ex);

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync();
                    }

                    throw new DbUpdateConcurrencyException(ex.Message, ex); // Rethrow so Polly retries
                }
                catch (Exception ex)
                {
                    await RollbackAsync(command, ex);
                    throw;
                }
                finally
                {
                    if (command is not INonDbCommand)
                        _unitOfWork.Dispose();
                }
            });

        }
        private async Task RollbackAsync(T command, Exception ex)
        {
            try
            {
                if (command is not INonDbCommand)
                    await _unitOfWork.RollbackAsync();
            }
            catch (Exception ex1)
            {
                _logger.LogDebug("RollbackAsync throw exception:" + ex1.Message + "\n" + ex1.StackTrace);
            }
        }
    }

}
