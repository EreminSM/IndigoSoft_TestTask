using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL;

public class DataReposity : IDataReposity
{
    private readonly IndigoSoftTestTaskDbContext _dbContext;

    public DataReposity(IndigoSoftTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUser(long accountNumber)
    {
        var userAlreadyExists = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.AccountNumber == accountNumber).AnyAsync();

        if (userAlreadyExists)
        {
            throw new ArgumentException($"A user with account number '{accountNumber}' already exists");
        }

        await _dbContext.Users.AddAsync(new UserEntity()
        {
            AccountNumber = accountNumber
        });

        await _dbContext.SaveChangesAsync();
    }
    public async Task AddConnectionEvent(long accountNumber, string ipAddress)
    {
        var userId = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.AccountNumber == accountNumber).Select(x => x.Id).SingleOrDefaultAsync();

        if (userId == Guid.Empty)
        {
            throw new ArgumentException($"A user with account number '{accountNumber}' was not found");
        }

        var ipAddressEntity = await _dbContext.IpAddresses.Where(x => x.Value == ipAddress).FirstOrDefaultAsync();

        if (ipAddressEntity == null)
        {
            ipAddressEntity = new IpAddressEntity() { Value = ipAddress };
            await _dbContext.IpAddresses.AddAsync(ipAddressEntity);
        }

        await _dbContext.ConnectionEvents.AddAsync(new ConnectionEventEntity()
        {
            UserId = userId,
            IpAddress = ipAddressEntity,
            DateAndTime = DateTime.UtcNow,
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<long>> GetUsersByPartOfIp(string partOfIp)
    {
        var query = _dbContext.ConnectionEvents
            .AsNoTracking()
            .Where(x => x.User != null && x.IpAddress != null && x.IpAddress.Value.StartsWith(partOfIp)).Select(x => x.User!.AccountNumber)
            .Distinct();

        return await query.ToListAsync();
    }

    public async Task<List<string>> GetIpAddressesByAccountId(long accountId)
    {
        var query = _dbContext.ConnectionEvents
            .AsNoTracking()
            .Where(x => x.User != null && x.IpAddress != null && x.User.AccountNumber == accountId).Select(x => x.IpAddress!.Value)
            .Distinct();

        return await query.ToListAsync();
    }

    public async Task<KeyValuePair<string, DateTime>> GetIpAddressAndTimeOfLatestConnection(long accountId)
    {
        var query = _dbContext.ConnectionEvents
            .AsNoTracking()
            .Where(x => x.User != null && x.IpAddress != null && x.User.AccountNumber == accountId)
            .OrderByDescending(x => x.DateAndTime)
            .Select(x => new KeyValuePair<string, DateTime>(x.IpAddress!.Value, x.DateAndTime))
            .Take(1);

        return await query.FirstOrDefaultAsync();
    }
}
