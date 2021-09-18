namespace CreditCardService.Settings
{
    public class CreditCardDatabaseSettings : ICreditCardDatabaseSettings
    {
        public string ConnectionString { get; 
        set; }
        public string DatabaseName { get; 
        set; }
        public string CollectionName { get; 
        set; }
    }
}