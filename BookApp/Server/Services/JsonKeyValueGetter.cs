namespace BookApp.Server.Services
{
    public class JsonKeyValueGetter : IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key)
        {
            var json = (JObject)JsonConvert.DeserializeObject(jsonString);
            return json[key].Value<string>();
        }
    }
}
