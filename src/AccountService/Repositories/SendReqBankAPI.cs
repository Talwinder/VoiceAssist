using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;
using System.Net.Http;

namespace AccountService.Repositories

{
    public static class SendReqBankAPI
    {

        static string accessToken  = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1OTI3NjY4ODQsIm5iZiI6MTU5Mjc2Njg4NCwianRpIjoiZDIwMzYyZWItY2RlZi00NGM2LWFjNzMtMzdlMTI5MGU4MTdjIiwiZXhwIjoxNTkyNzY3Nzg0LCJpZGVudGl0eSI6IkFrc2hhdHRyaXBhYSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyJ9.DGX-FsLKBcwtrSbgKLKUjij0TU4KeuqON1zSYtKNpJk";

        public  static string SendRequest(string username, string bankUrl)
        {

            string[] scopes = new string[]{"openApi"};
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
          
            client.DefaultRequestHeaders.Add("userName", username);
            return  client.GetAsync(bankUrl).Result.Content.ToString();

        }
    }
}