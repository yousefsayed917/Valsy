using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class UserOTPLockedException : AppException
    {
        public string MessageError { get; set; }
        public DateTime LockedTime { get; set; }
        public UserOTPLockedException(DateTime lockedTime)
        {
            LockedTime = lockedTime;
            MessageError = "Your account is locked due to too many failed OTP attempts or too many OTP resend. Please contact support to unlock your account.";
        }
    }
}
