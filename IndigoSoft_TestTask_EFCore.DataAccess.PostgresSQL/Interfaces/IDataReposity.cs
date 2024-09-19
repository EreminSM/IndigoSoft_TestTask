namespace IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;

public interface IDataReposity
{
    Task AddUser(long accountNumber);
    Task AddConnectionEvent(long accountNumber, string ipAddress);
    Task<List<long>> GetUsersByPartOfIp(string partOfIp);
    Task<List<string>> GetIpAddressesByAccountId(long accountId);    
    Task<KeyValuePair<string, DateTime>> GetIpAddressAndTimeOfLastConnection(long accountId);

}