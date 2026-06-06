using Valsy.Domain.Common.RegisteringServices;

namespace Valsy.Application.Common.Interfaces
{
    public interface ISecureEncryption : ITransientService
    {
        string DecryptData(string EncryptedText);
        string EncryptData(string textData);
    }
}
