using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetail> GetBasket(string userName);

       

        Task<string> ValidateUserPin(UserDetail user);
    }
}
