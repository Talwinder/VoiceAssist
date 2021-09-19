using UserService.Data.Interfaces;
using UserService.Entities;
using UserService.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;


namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<UserDetail> GetCacheToken(string userName)
        {
            var userInfo = await _context
                                .Redis
                                .StringGetAsync(userName);

        
            return JsonConvert.DeserializeObject<UserDetail>(userInfo);
        }
        
        public async Task<string> ValidateUserPin(UserDetail user)
        {
             
            var result  =  ReqBankApi.SendRequest("http://107.23.94.249/verifypin", null);
            var token =  JsonConvert.DeserializeObject(result).ToString();
            if (token != null)
            {
             UpdateTokenForUsers(user.UserName,token);
             return "Message: Login Successful, access_token: "+ token;
            }
            else
                  return "Message: Invalid Pin";

            
        }


        private void UpdateTokenForUsers(string userid, string Token)
        {
         string file_path = Path.Combine(Directory.GetCurrentDirectory(),"/UserTokenMaping.json") ;
         JObject jsonObject = JObject.Parse(File.ReadAllText(file_path));
         try  
         {  
       
          jsonObject["username"] = Token;  
          string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject,  
                               Newtonsoft.Json.Formatting.Indented);  
          File.WriteAllText(file_path, newJsonResult);  
         }  
        catch (Exception ex)  
        {  
            Console.WriteLine("Add Error : " + ex.Message.ToString());  
        }  
        }

        
    }
}