namespace CreditCardService.Settings
{
    public interface ICreditCardDatabaseSettings
    {

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }




    }
}