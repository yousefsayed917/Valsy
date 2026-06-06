using Valsy.Domain.Common.Enums;

namespace Valsy.Application.Common.Interfaces
{
    public interface IExecutionContextAccessor 
    {
        Guid? CorrelationId { get; }
        Language Language { get; }
        int UserId { get; }
        string Email { get; }
        string RemoteIpAddress { get; }

        public UserType UserType { get; }
        public string UserRoleIds { get; }
        bool IsAuthorized { get; }
        public string UserMacAddress { get; }

    }
}
