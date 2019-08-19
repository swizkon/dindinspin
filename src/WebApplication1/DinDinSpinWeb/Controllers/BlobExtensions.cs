using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace DinDinSpinWeb.Controllers
{
    public static class BlobExtensions
    {
        public static async Task SerializeObjectToBlobAsync(this CloudBlockBlob blob, object obj)
        {
            using (var stream = await blob.OpenWriteAsync())
            using (var sw = new StreamWriter(stream))
            using (var jtw = new JsonTextWriter(sw))
            {
                var ser = new JsonSerializer();
                ser.Serialize(jtw, obj);
            }
        }

        public static async Task<T> DeserializeObjectFromBlobAsync<T>(this CloudBlockBlob blob)
        {
            using (var stream = await blob.OpenReadAsync())
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var ser = new JsonSerializer();
                return ser.Deserialize<T>(jtr);
            }
        }
    }
}