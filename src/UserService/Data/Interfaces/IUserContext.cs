

using StackExchange.Redis;
namespace UserService.Data.Interfaces

{

    public interface IUserContext
    {
        IDatabase Redis { get; }
    }

}