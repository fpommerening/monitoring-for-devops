namespace CustomerService.Business;

public class CustomerProcessor(ICustomerRepository customerRepository) : ICustomerProcessor
{
    private static readonly Random Random = new();

    public async Task<Customer> ValidateAsync(string lastName, string firstName)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(Random.Next(15, 25)));
        var customer = await customerRepository.LoadCustomerAsync(lastName, firstName);
        await Task.Delay(TimeSpan.FromMilliseconds(Random.Next(15, 25)));
        return customer;
    }
}