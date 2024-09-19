using IndigoSoft_TestTask.API.Models;

namespace IndigoSoft_TestTask.API.Validators;

public class UserConnectionEventValidator
{
    public static void Validate(UserConnectionEvent userConnectionEvent)
    {
        IpAddressValidator.Validate(userConnectionEvent.IpAddress);
        AccountNumberValidator.Validate(userConnectionEvent.UserAccount);
    }
}
