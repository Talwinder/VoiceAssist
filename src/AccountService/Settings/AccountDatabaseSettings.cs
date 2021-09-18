namespace AccountService.Settings
{
    public class AccountDatabaseSettings : IAccountDatabaseSettings
    {
        public string ConnectionString { get; 
        set; }
        public string DatabaseName { get; 
        set; }
        public string CollectionName { get; 
        set; }
    }
}