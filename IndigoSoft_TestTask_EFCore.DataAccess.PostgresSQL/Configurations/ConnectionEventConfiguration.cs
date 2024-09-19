using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Configurations;

public class ConnectionEventConfiguration : IEntityTypeConfiguration<ConnectionEventEntity>
{
    public void Configure(EntityTypeBuilder<ConnectionEventEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User).WithMany(u => u.ConnectionEvents).HasForeignKey(c => c.UserId);
        builder.HasOne(c => c.IpAddress).WithMany(i => i.ConnectionEvents).HasForeignKey(c => c.IpAddressId);
    }
}
