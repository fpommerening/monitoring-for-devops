namespace CustomerService.Business;

public interface IAddressProcessor
{
    Task<bool> IsValidAsync(Address address);
}