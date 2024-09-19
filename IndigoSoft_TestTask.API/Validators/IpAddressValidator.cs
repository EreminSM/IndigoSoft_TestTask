using System.Net;

namespace IndigoSoft_TestTask.API.Validators;

public static class IpAddressValidator
{
    public static void Validate(string ipAddress)
    {
        var isValidIpAddress = IPAddress.TryParse(ipAddress, out _);
        if (!isValidIpAddress)
        {
            throw new BadHttpRequestException($"The value '{ipAddress}' is not correct IP4/IP6 address");
        }
    }
}
