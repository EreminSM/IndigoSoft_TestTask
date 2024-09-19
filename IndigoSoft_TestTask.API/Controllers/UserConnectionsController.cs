using IndigoSoft_TestTask.API.Models;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IndigoSoft_TestTask.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserConnectionsController : ControllerBase
{
    private readonly IDataReposity _dataReposity;

    public UserConnectionsController(IDataReposity dataReposity)
    {
        _dataReposity = dataReposity;
    }

    [HttpPost("/users/")]
    public async Task AddUser([FromBody] long accountNumber)
    {
        if (accountNumber <= 0)
        {
            throw new BadHttpRequestException(
                $"The account number should have non-negative and non-null value, value that was passed: '{accountNumber}'");
        }

        await _dataReposity.AddUser(accountNumber);
    }

    [HttpGet("/users/{partOfIpAddress}")]
    public async Task<IEnumerable<long>> GetUsersByPartOfIPAddress(string partOfIpAddress)
    {
        return await _dataReposity.GetUsersByPartOfIp(partOfIpAddress);
    }

    [HttpGet("/all_ip_addresses/{accountNumber}")]
    public async Task<IEnumerable<string>> GetAllIPAddressesForAccount(long accountNumber)
    {
        return await _dataReposity.GetIpAddressesByAccountId(accountNumber);
    }

    [HttpPost("/connections/")]
    public async Task AddUserConnection([FromBody] UserConnectionEvent userConnectionEvent)
    {
        var isValidIpAddress = IPAddress.TryParse(userConnectionEvent.IpAddress, out _);
        if (!isValidIpAddress)
        {
            throw new BadHttpRequestException($"The value '{userConnectionEvent.IpAddress}' is not correct IP4/IP6 address");
        }

        await _dataReposity.AddConnectionEvent(userConnectionEvent.UserAccount, userConnectionEvent.IpAddress);
    }

    [HttpGet("/connections/latest/{accountNumber}")]
    public async Task<KeyValuePair<string, DateTime>> GetLatestConnectionInfoForAccount(long accountNumber)
    {
        return await _dataReposity.GetIpAddressAndTimeOfLastConnection(accountNumber);
    }
}