namespace IndigoSoft_TestTask.API.Models
{
    public class UserConnectionEvent
    {
        public required long UserAccountNumber { set; get; }
        public required string IpAddress { set; get; }
    }
}
