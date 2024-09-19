namespace IndigoSoft_TestTask.API.Models
{
    public class UserConnectionEvent
    {
        public required long UserAccount { set; get; }
        public required string IpAddress { set; get; }
    }
}
