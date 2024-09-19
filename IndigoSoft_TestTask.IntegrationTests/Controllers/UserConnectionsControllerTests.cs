using IndigoSoft_TestTask.API.Controllers;
using IndigoSoft_TestTask.API.Models;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Xunit;

namespace IndigoSoft_TestTask.IntegrationTests.Controllers;

public class UserConnectionsControllerTests : IClassFixture<TestsFixture>
{
    private readonly IndigoSoftTestTaskDbContext _dbContext;
    private readonly UserConnectionsController _controller;
    public UserConnectionsControllerTests(TestsFixture data)
    {
        _dbContext = data.DbContext;
        _controller = new UserConnectionsController(new DataReposity(_dbContext));
    }

    [Fact]
    public async Task When_AddUserCallsWithValidRequest_UserAddsToDb()
    {
        // Arrange
        await RecreateDb();

        var accountNumbers = new List<long>() { 111111111, 222222222, 333333333, 444444444, 555555555 };

        // Act
        foreach (var accountNumber in accountNumbers)
        {
            await _controller.AddUser(accountNumber);
        }

        // Assert
        var existedUsers = await _dbContext.Users.ToListAsync();
        Assert.Equal(accountNumbers.Count, existedUsers.Count);

        for(int i = 0; i < existedUsers.Count; i++)
        {
            Assert.Equal(accountNumbers[i], existedUsers[i].AccountNumber);
        }
    }

    [Fact]
    public async Task When_AddUserConnectionCallsAndThenGetUsersByPartOfIpAddressWithFullIpAddressCalls_ThenItReturnsExceptedResult()
    {
        // Arrange
        const int IP_ADDRESSES_COUNT = 20;
        await RecreateDb();

        var userAccuntNumbers = await AddFiveUsersAndConnectionEventsToThem();
        var ipAddresses = GetRandomIpAddresses(IP_ADDRESSES_COUNT);
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[3] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[4] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });

        // Act
        var resultForIpAddress_0 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[0]);
        var resultForIpAddress_1 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[1]);
        var resultForIpAddress_2 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[2]);
        var resultForIpAddress_3 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[3]);
        var resultForIpAddress_4 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[4]);
        var resultForIpAddress_5 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[5]);
        var resultForIpAddress_6 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[6]);
        var resultForIpAddress_9 = await _controller.GetUsersByPartOfIpAddress(ipAddresses[9]);

        // Assert
        Assert.Equal(2, resultForIpAddress_0.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress_0);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress_0);

        Assert.Empty(resultForIpAddress_1);

        Assert.Equal(2, resultForIpAddress_2.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress_2);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress_2);

        Assert.Equal(3, resultForIpAddress_3.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress_3);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress_3);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress_3);

        Assert.Single(resultForIpAddress_4);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress_4);

        Assert.Empty(resultForIpAddress_5);

        Assert.Single(resultForIpAddress_6);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress_6);

        Assert.Empty(resultForIpAddress_9);
    }

    [Fact]
    public async Task When_AddUserConnectionCallsAndThenGetUsersByPartOfIpAddressWithPartOfIpAddressCalls_ThenItReturnsExceptedResult()
    {
        // Arrange
        await RecreateDb();

        var userAccuntNumbers = await AddFiveUsersAndConnectionEventsToThem();
        var ipAddresses = new List<string>
        {
            "111.22.222.222",
            "111.12.222.222",
            "111.11.222.222",
            "111.11.122.222",
            "111.11.112.222",
            "111.11.111.222",
            "111.11.111.122",
        };
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[3] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[4] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });

        // Act
        var resultForIpAddress1 = await _controller.GetUsersByPartOfIpAddress("111");
        var resultForIpAddress2 = await _controller.GetUsersByPartOfIpAddress("111.1");
        var resultForIpAddress3 = await _controller.GetUsersByPartOfIpAddress("111.11");
        var resultForIpAddress4 = await _controller.GetUsersByPartOfIpAddress("111.11.");
        var resultForIpAddress5 = await _controller.GetUsersByPartOfIpAddress("111.11.1");
        var resultForIpAddress6 = await _controller.GetUsersByPartOfIpAddress("111.11.11");
        var resultForIpAddress7 = await _controller.GetUsersByPartOfIpAddress("111.11.111");
        var resultForIpAddress8 = await _controller.GetUsersByPartOfIpAddress("111.11.111.1");

        // Assert
        Assert.Equal(3, resultForIpAddress1.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress1);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress1);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress1);

        Assert.Equal(3, resultForIpAddress2.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress2);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress2);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress2);

        Assert.Equal(3, resultForIpAddress3.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress3);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress3);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress3);

        Assert.Equal(3, resultForIpAddress4.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress4);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress4);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress4);

        Assert.Equal(3, resultForIpAddress5.Count());
        Assert.Contains(userAccuntNumbers[0], resultForIpAddress5);
        Assert.Contains(userAccuntNumbers[2], resultForIpAddress5);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress5);

        Assert.Single(resultForIpAddress6);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress6);

        Assert.Single(resultForIpAddress6);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress6);

        Assert.Single(resultForIpAddress7);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress7);

        Assert.Single(resultForIpAddress8);
        Assert.Contains(userAccuntNumbers[3], resultForIpAddress8);
    }

    [Fact]
    public async Task When_AddUserConnectionCallsAndThenGetAllIpAddressesForAccountCalls_ThenItReturnsExceptedResult()
    {
        // Arrange
        const int IP_ADDRESSES_COUNT = 20;
        await RecreateDb();

        var userAccuntNumbers = await AddFiveUsersAndConnectionEventsToThem();
        var ipAddresses = GetRandomIpAddresses(IP_ADDRESSES_COUNT);
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[0], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[0] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[2] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[2], IpAddress = ipAddresses[3] });

        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[3] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[3] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[4] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[4] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });
        await _controller.AddUserConnection(
            new UserConnectionEvent() { UserAccountNumber = userAccuntNumbers[3], IpAddress = ipAddresses[6] });

        // Act
        var resultForUserAccuntNumbers_0 = await _controller.GetAllIpAddressesForAccount(userAccuntNumbers[0]);
        var resultForUserAccuntNumbers_1 = await _controller.GetAllIpAddressesForAccount(userAccuntNumbers[1]);
        var resultForUserAccuntNumbers_2 = await _controller.GetAllIpAddressesForAccount(userAccuntNumbers[2]);
        var resultForUserAccuntNumbers_3 = await _controller.GetAllIpAddressesForAccount(userAccuntNumbers[3]);

        // Assert
        Assert.Equal(3, resultForUserAccuntNumbers_0.Count());
        Assert.Contains(ipAddresses[0], resultForUserAccuntNumbers_0);
        Assert.Contains(ipAddresses[2], resultForUserAccuntNumbers_0);
        Assert.Contains(ipAddresses[3], resultForUserAccuntNumbers_0);

        Assert.Empty(resultForUserAccuntNumbers_1);

        Assert.Equal(3, resultForUserAccuntNumbers_2.Count());
        Assert.Contains(ipAddresses[0], resultForUserAccuntNumbers_2);
        Assert.Contains(ipAddresses[2], resultForUserAccuntNumbers_2);
        Assert.Contains(ipAddresses[3], resultForUserAccuntNumbers_2);

        Assert.Equal(3, resultForUserAccuntNumbers_3.Count());
        Assert.Contains(ipAddresses[3], resultForUserAccuntNumbers_3);
        Assert.Contains(ipAddresses[4], resultForUserAccuntNumbers_3);
        Assert.Contains(ipAddresses[6], resultForUserAccuntNumbers_3);
    }

    [Fact]
    public async Task When_AddUserConnectionCallsAndGetLatestConnectionInfoForAccountCalls_ThenItReturnsExceptedResult()
    {
        // Arrange
        const int IP_ADDRESSES_COUNT = 20;
        const int DELAY_BETWEN_CONNECTION_EVENTS_MS = 200;

        await RecreateDb();

        var userAccuntNumber = await AddSingleAccount();
        var ipAddresses = GetRandomIpAddresses(IP_ADDRESSES_COUNT);
        var timeOfStarting = DateTime.UtcNow;
        foreach (var ipAddress in ipAddresses)
        {
            await _controller.AddUserConnection(
                new UserConnectionEvent() { UserAccountNumber = userAccuntNumber, IpAddress = ipAddress });
            await Task.Delay(TimeSpan.FromMilliseconds(DELAY_BETWEN_CONNECTION_EVENTS_MS));
        }

        // Act
        var result = await _controller.GetLatestConnectionInfoForAccount(userAccuntNumber);

        // Assert
        Assert.Equal(ipAddresses[ipAddresses.Count - 1], result.Key);
        Assert.True(timeOfStarting < result.Value);
    }

    private async Task RecreateDb() 
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();
    }
    private async Task<List<long>> AddFiveUsersAndConnectionEventsToThem()
    {
        List<long> result = new List<long>() { 111111111, 222222222, 333333333, 444444444, 555555555 };


        foreach (var accountNumber in result)
        {            
            await _dbContext.Users.AddAsync(new UserEntity() { AccountNumber = accountNumber });
        }

        await _dbContext.SaveChangesAsync();

        return result;
    }

    private async Task<long> AddSingleAccount()
    {
        var result = 111111111;

        await _dbContext.Users.AddAsync(new UserEntity() { AccountNumber = result });
        await _dbContext.SaveChangesAsync();

        return result;
    }

    private List<string> GetRandomIpAddresses(int count)
    {
        var result = new List<string>();

        for (int i = 0; i < count; i++)
        {
            string randomIpAddress;
            while (true)
            {
                randomIpAddress = GetRandomIpAddress();
                if(!result.Contains(randomIpAddress))
                {
                    break;
                }
            }
            result.Add(randomIpAddress);
        }

        return result;
    }
    private string GetRandomIpAddress()
    {
        var data = new byte[4];
        new Random().NextBytes(data);
        IPAddress ip = new IPAddress(data);

        return ip.ToString();
    }
}
