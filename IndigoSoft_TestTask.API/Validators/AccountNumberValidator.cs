namespace IndigoSoft_TestTask.API.Validators;

public static class AccountNumberValidator
{
    public static void Validate(long accountNumber)
    {
        if (accountNumber <= 0)
        {
            throw new BadHttpRequestException(
                $"The account number should have non-negative and non-null value, value that was passed: '{accountNumber}'");
        }
    }
}
