using System.IO;
using System.Text;

namespace Knorr.Core.FileDb
{
    public class JsonSerializer
    {
        public string ToJson<T>(T model) where T : IModel
        {
            string json;
            using (var ms = new MemoryStream())
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(model.GetType());
                serializer.WriteObject(ms, model);
                ms.Position = 0;
                using(var sr = new StreamReader(ms))
                    json = sr.ReadToEnd();
            }
            return json;
        }
        public T To<T>(string json) where T : IModel, new()
        {
            var obj = new T();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                obj = (T) serializer.ReadObject(ms);
                ms.Close();
            }
            return obj;
        }
    }
}
