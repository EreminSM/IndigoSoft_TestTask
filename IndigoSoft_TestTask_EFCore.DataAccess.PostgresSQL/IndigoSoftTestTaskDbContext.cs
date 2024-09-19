using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Configurations;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL;

public class IndigoSoftTestTaskDbContext : DbContext
{
    public IndigoSoftTestTaskDbContext(DbContextOptions<IndigoSoftTestTaskDbContext> options)
    : base(options)
    {

    }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<IpAddressEntity> IpAddresses { get; set; }
    public DbSet<ConnectionEventEntity> ConnectionEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new IpAddressConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectionEventConfiguration());

        modelBuilder.Entity<IpAddressEntity>().Property(i => i.Value).HasColumnType("varchar").HasMaxLength(45);
        modelBuilder.Entity<IpAddressEntity>().HasIndex(i => i.Value).IsUnique();

        modelBuilder.Entity<UserEntity>().HasIndex(u => u.AccountNumber).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
