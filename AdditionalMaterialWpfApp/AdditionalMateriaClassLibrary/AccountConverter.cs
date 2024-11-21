using AdditionalMaterialWpfApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace AdditionalMateriaClassLibrary
{
    public class AccountConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IAccount<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Десериализация JSON в класс Account<string>
            var jsonObject = JObject.Load(reader);
            var account = new Account<string>(
                jsonObject["Type"].ToObject<string>(),
                jsonObject["Balance"].ToObject<decimal>()
            );
            return account;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Сериализация Account<string> обратно в JSON
            var account = (Account<string>)value;
            var jsonObject = new JObject
        {
            { "Type", JToken.FromObject(account.Type) },
            { "Balance", JToken.FromObject(account.Balance) }
        };
            jsonObject.WriteTo(writer);
        }
    }
}
