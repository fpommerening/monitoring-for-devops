namespace CustomerService.Business;

public class Customer(string firstName, string lastName)
{
    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName;

    public bool IsValid { get; set; }
}