using System.Text.Json;

namespace Valsy.Domain.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be cast</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
        public static string ObjectSerialization(this object obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            string Payload = JsonSerializer.Serialize(obj, options);
            return Payload;
        }
    }
}
