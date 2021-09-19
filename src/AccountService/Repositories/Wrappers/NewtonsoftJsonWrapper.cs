using Newtonsoft.Json;
using AccountService.Repositories.Interfaces.StaticClassesInterfaces;
namespace AccountService.Repositories.Wrappers
{
    public class NewtonsoftJsonWrapper : IJsonUtility
    {
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
