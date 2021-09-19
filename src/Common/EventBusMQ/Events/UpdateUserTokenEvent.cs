namespace EventBusMQ.Events
{
    public class UpdateUserTokenEvent : IntegrationBaseEvent
    {
        public string UserName { get; set; }

        public decimal UserToken { get; set; }
    }
}
