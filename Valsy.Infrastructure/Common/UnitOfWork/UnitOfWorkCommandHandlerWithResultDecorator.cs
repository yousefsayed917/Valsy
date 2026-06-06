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
using Valsy.Domain.Common.Enums;

namespace BuildingBlocks.Infrastructure.UnitOfWork
{
    public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : CommandBase<TResult>
    {
        private readonly IRequestHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>> _logger;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            IRequestHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork, AppSettings appSettings, ILogger<UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>> logger)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            // Retry policy for DbUpdateConcurrencyException
            AsyncRetryPolicy retryPolicy = Policy
                .Handle<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(
                    retryCount: int.Parse(_appSettings.PipelineConfig.RetryConfig.RetryCount),
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(
                        double.Parse(_appSettings.PipelineConfig.RetryConfig.SleepDuration)
                    ),
                    onRetry: (exception, delay, retryCount, context) =>
                    {
                        _logger.LogInformation($"Retry {retryCount} due to concurrency issue: {exception.Message}");
                    });

            // Fallback policy if all retries fail
            AsyncFallbackPolicy<TResult> fallbackPolicy = Policy<TResult>
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

            // Combine retry + fallback
            AsyncPolicyWrap<TResult> combinedPolicy = fallbackPolicy.WrapAsync(retryPolicy);

            // Execute the policy-wrapped operation
            return await combinedPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    if (command is not INonDbCommand)
                        await _unitOfWork.BeginAsync();

                    TResult result = await _decorated.Handle(command, cancellationToken);

                    if (command is not INonDbCommand && command.InitSource == InitSource.Api)
                        await _unitOfWork.CommitAsync(cancellationToken);

                    if (command.InitSource == InitSource.Test)
                        await RollbackAsync(command, null);

                    return result;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await RollbackAsync(command, ex);

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync();
                    }
                    _logger.LogError(ex, ex.Message);
                    throw new DbUpdateConcurrencyException(ex.Message, ex); // Rethrow so Polly retries
                }
                catch (DbUpdateException ex)
                {
                    await RollbackAsync(command, ex);

                    foreach (var entry in ex.Entries)
                    {
                        await entry.ReloadAsync();
                    }
                    _logger.LogError(ex, ex.Message);
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
                _logger.LogError(ex1, ex1.Message);
            }
        }
    }
}
