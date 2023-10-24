namespace CustomerService.Business;

public interface ICreditRateProvider
{
    Task<uint> GetRateValueAsync(Customer customer, Address address);
}