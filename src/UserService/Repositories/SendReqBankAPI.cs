using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Repositories
{
    public static class ReqBankApi
    {
        static string
            accessToken =
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1OTI3NjY4ODQsIm5iZiI6MTU5Mjc2Njg4NCwianRpIjoiZDIwMzYyZWItY2RlZi00NGM2LWFjNzMtMzdlMTI5MGU4MTdjIiwiZXhwIjoxNTkyNzY3Nzg0LCJpZGVudGl0eSI6IkFrc2hhdHRyaXBhYSIsImZyZXNoIjpmYWxzZSwidHlwZSI6ImFjY2VzcyJ9.DGX-FsLKBcwtrSbgKLKUjij0TU4KeuqON1zSYtKNpJk";

        public static string
        SendRequest(string bankUrl, Dictionary<string, string> headers)
        {
            string[] scopes = new string[] { "openApi" };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            foreach (var item in headers)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
            return client.GetAsync(bankUrl).Result.Content.ToString();
        }
    }
}
