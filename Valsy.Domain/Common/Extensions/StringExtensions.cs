using System.Security.Cryptography;

namespace Valsy.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool SecondaryNameValidation(this string nameSecondary)
        {
            return string.IsNullOrEmpty(nameSecondary) || nameSecondary.NameLengthValidation();
        }
        public static bool NameLengthValidation(this string nameSecondary)
        {
            return (nameSecondary.Length >= 2 && nameSecondary.Length <= 25);
        }
        /// <summary>
        /// Provides a function to build otp with length.
        /// </summary>
        public static string GenerateNumericOTP(this string otp, int lengthOfOTP)
        {
            const string validChars = "0123456789";

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                char[] otpChars = new char[lengthOfOTP];
                byte[] randomBytes = new byte[lengthOfOTP];

                rng.GetBytes(randomBytes);

                for (int i = 0; i < lengthOfOTP; i++)
                {
                    // Convert the random byte to an index within our valid characters
                    otpChars[i] = validChars[randomBytes[i] % validChars.Length];
                }
                otp = new string(otpChars);
                return otp;
            }
        }

        /// <summary>
        /// Provides a function to mask contact.
        /// </summary>
        public static string MaskAllButLastTwoDigits(this string mobile)
        {
            for (int i = 0; i < mobile.Length - 2; i++)
            {
                mobile = mobile.Remove(i, 1).Insert(i, "*");
            }
            return mobile;
        }
    }
}
