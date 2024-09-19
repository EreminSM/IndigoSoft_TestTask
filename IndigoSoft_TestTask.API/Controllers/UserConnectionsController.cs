using IndigoSoft_TestTask.API.Models;
using IndigoSoft_TestTask.API.Validators;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        AccountNumberValidator.Validate(accountNumber);

        await _dataReposity.AddUser(accountNumber);
    }

    [HttpGet("/users/{partOfIpAddress}")]
    public async Task<IEnumerable<long>> GetUsersByPartOfIpAddress(string partOfIpAddress)
    {
        PartOfIPAddressValidator.Validate(partOfIpAddress);

        return await _dataReposity.GetUsersByPartOfIp(partOfIpAddress);
    }

    [HttpGet("/all_ip_addresses/{accountNumber}")]
    public async Task<IEnumerable<string>> GetAllIpAddressesForAccount(long accountNumber)
    {
        AccountNumberValidator.Validate(accountNumber);

        return await _dataReposity.GetIpAddressesByAccountId(accountNumber);
    }

    [HttpPost("/connections/")]
    public async Task AddUserConnection([FromBody] UserConnectionEvent userConnectionEvent)
    {
        IpAddressValidator.Validate(userConnectionEvent.IpAddress);

        await _dataReposity.AddConnectionEvent(userConnectionEvent.UserAccount, userConnectionEvent.IpAddress);
    }

    [HttpGet("/connections/latest/{accountNumber}")]
    public async Task<KeyValuePair<string, DateTime>> GetLatestConnectionInfoForAccount(long accountNumber)
    {
        return await _dataReposity.GetIpAddressAndTimeOfLastConnection(accountNumber);
    }
}