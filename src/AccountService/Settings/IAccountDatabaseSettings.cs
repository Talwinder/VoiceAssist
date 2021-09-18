namespace  AccountService.Settings
{
    public interface IAccountDatabaseSettings
    {

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }




    }
}