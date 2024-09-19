namespace IndigoSoft_TestTask.API.Validators;

public static class PartOfIPAddressValidator
{
    public static void Validate(string partOfIpAddress)
    {
        if(string.IsNullOrEmpty(partOfIpAddress))
        {
            throw new BadHttpRequestException("The part of IP address could not be empty");
        }

        foreach (char c in partOfIpAddress)
        {
            if (c != '.' && (c < '0' || c > '9'))
                throw new BadHttpRequestException(
                    $"The part of IP address could have the digits and point chars, value that was passed: '{partOfIpAddress}'");
        }
    }
}