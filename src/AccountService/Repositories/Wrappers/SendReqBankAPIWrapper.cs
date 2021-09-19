using AccountService.Repositories.Interfaces.StaticClassesInterfaces;
using Newtonsoft.Json;

namespace AccountService.Repositories.Wrappers
{
    public class SendReqBankAPIWrapper : ISendReqBankAPI
    {
        public string SendRequest(string userName, string bankUrl)
        {
            return SendReqBankAPI.SendRequest(userName, bankUrl);
        }
    }
}
