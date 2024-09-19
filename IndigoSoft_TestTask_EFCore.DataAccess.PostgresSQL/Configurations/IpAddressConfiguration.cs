using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Configurations;

public class IpAddressConfiguration : IEntityTypeConfiguration<IpAddressEntity>
{
    public void Configure(EntityTypeBuilder<IpAddressEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasMany(i => i.ConnectionEvents).WithOne(c => c.IpAddress);
    }
}
