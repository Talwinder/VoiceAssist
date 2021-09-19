namespace AccountService.Repositories.Interfaces.StaticClassesInterfaces
{
    public interface IJsonUtility
    {
        T DeserializeObject<T>(string value);
    }
}
