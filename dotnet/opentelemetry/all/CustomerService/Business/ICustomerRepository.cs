namespace CustomerService.Business;

public interface ICustomerRepository
{
    Task<Customer> LoadCustomerAsync(string lastName, string firstName);
}