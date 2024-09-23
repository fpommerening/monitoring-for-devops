namespace CustomerService.Business;

public class Address(string street, string town, string zipCode)
{
    public string Street { get; } = street;

    public string Town { get; } = town;

    public string ZipCode { get; } = zipCode;
}