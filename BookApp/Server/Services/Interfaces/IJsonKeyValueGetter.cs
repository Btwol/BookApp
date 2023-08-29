namespace BookApp.Server.Services.Interfaces
{
    public interface IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key);
    }
}
