using System.ComponentModel.DataAnnotations;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;

public class ConnectionEventEntity
{
    public Guid Id { set; get; }

    [Required]
    public DateTime DateAndTime { set; get; } = DateTime.MinValue;

    public Guid UserId { set; get; }
    public UserEntity? User { set; get; }

    public Guid IpAddressId { set; get; }
    public IpAddressEntity? IpAddress { set; get; }
}
