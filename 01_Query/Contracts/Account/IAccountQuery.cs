namespace _01_Query.Contracts.Account
{
    public interface IAccountQuery
    {
        AccountQueryModel GetDetails(string username);
    }
}
