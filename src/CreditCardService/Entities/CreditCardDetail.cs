using System.Collections.Generic;

namespace CreditCardService.Entities
{
    public class CreditCardDetail
    {
        public string Id { get; set; }

       
        public string CardName { get; set; }

        public string Description { get; set; }

        public double Outstanding { get; set; }

        public string BilledAmount { get; set; }

    }
}