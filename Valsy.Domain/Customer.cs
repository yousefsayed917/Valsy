namespace Valsy.Domain;

public class Customer : AggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string AddressLine1 { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public List<Order> Orders { get; private set; } = new();

    private Customer() { }

    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string addressLine1,
        string city,
        string country,
        string createdBy)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            AddressLine1 = addressLine1,
            City = city,
            Country = country
        };

        customer.MarkCreated(Guid.NewGuid(), createdBy);
        return customer;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string phoneNumber,
        string addressLine1,
        string city,
        string country,
        string modifiedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        AddressLine1 = addressLine1;
        City = city;
        Country = country;

        MarkModified(modifiedBy);
    }
}
