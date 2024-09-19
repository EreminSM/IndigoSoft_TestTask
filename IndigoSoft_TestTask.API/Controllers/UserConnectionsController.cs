using IndigoSoft_TestTask.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IndigoSoft_TestTask.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserConnectionsController : ControllerBase
{
    private readonly ILogger<UserConnectionsController> _logger;

    public UserConnectionsController(ILogger<UserConnectionsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/users/{partOfIpAddress}")]
    public async Task<IEnumerable<long>> GetUsersByPartOfIPAddress(string partOfIpAddress)
    {
        throw new NotImplementedException();
    }

    [HttpGet("/all_ip_addresses/{accountNumber}")]
    public async Task<IEnumerable<string>> GetAllIPAddressesForAccount(long accountNumber)
    {
        throw new NotImplementedException();
    }

    [HttpGet("/connections/latest/{accountNumber}")]
    public async Task<KeyValuePair<string, DateTime>> GetLatestConnectionInfoForAccount(long accountNumber)
    {
        throw new NotImplementedException();
    }

    [HttpPost("/connections/")]
    public async Task AddUserConnection([FromBody] UserConnectionEvent userConnectionEvent)
    {
        throw new NotImplementedException();
    }
}