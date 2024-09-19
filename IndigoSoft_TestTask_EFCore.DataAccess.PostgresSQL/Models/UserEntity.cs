using System.ComponentModel.DataAnnotations;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;

public class UserEntity
{
    public Guid Id { set; get; }

    [Required]
    public long AccountNumber { set; get; }
    public List<ConnectionEventEntity> ConnectionEvents { set; get; } = new();
}
