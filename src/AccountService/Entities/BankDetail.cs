using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
namespace AccountService.Entities
{
    public class BankDetail
    {
        public BankDetail()
        {
           
        }
        public string BankName { get; set; }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string BankID {get; set;}
        public Dictionary<string,string> BankUrls { get ;set;} // requestType, Urls
    }
}