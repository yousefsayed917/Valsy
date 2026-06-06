namespace Valsy.Application.Common.Exceptions
{
    public class StorageException : Exception
    {
        public string StorageMessage { get; set; }
        public StorageException(string message)
        {
            StorageMessage = message;
        }

    }
}
