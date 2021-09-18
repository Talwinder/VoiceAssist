using System.Collections.Generic;

namespace UserService.Entities
{
    public class UserDetail
    {
        public string UserName { get; set; }
        public int UserId  { get; set; }
        public string ContactNumber {get; set;}
        public string Email {get; set;}
        public string UserToken { get; set;}

    }
}