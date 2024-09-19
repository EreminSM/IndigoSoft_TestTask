using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.ConnectionEvents).WithOne(c => c.User);
    }
}