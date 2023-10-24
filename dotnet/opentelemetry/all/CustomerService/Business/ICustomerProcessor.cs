namespace CustomerService.Business;

public interface ICustomerProcessor
{
    Task<Customer> ValidateAsync(string name, string firstName);
}