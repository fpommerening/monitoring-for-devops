namespace CustomerService.Business;

public class CreditRateProvider(IHttpClientFactory httpClientFactory) : ICreditRateProvider
{
    private static readonly Random Random = new();

    public async Task<uint> GetRateValueAsync(Customer customer, Address address)
    {
        var client = httpClientFactory.CreateClient("credit-rate");
        var result = await client.GetAsync("/");
        if (result.IsSuccessStatusCode)
        {
            return (uint)Random.Next(50, 98);
        }

        return (uint)Random.Next(0, 10);
    }
}