using System.ComponentModel.DataAnnotations;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;

public class IpAddressEntity
{
    public Guid Id { set; get; }

    [Required]
    public string Value { set; get; } = string.Empty;
    public List<ConnectionEventEntity> ConnectionEvents { set; get; } = new();
}
