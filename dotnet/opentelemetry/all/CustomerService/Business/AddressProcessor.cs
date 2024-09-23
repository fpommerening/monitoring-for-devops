namespace CustomerService.Business;

public class AddressProcessor(IZipCodeRepository zipCodeRepository) : IAddressProcessor
{
    private static readonly Random Random = new();

    public async Task<bool> IsValidAsync(Address address)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(Random.Next(10, 30)));
        if (!await zipCodeRepository.IsValidAsync(address.ZipCode))
        {
            return false;
        }
        await Task.Delay(TimeSpan.FromMilliseconds(Random.Next(30, 60)));
        return ((address.Street.Length + address.Town.Length) % 7) == 0;
    }
}