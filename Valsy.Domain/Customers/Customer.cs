using Valsy.Domain.Common;
using Valsy.Domain.Orders;

namespace Valsy.Domain.Customers;

public class Customer : AggregateRoot<int>
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

    public Customer Create(Customer customer)
    {
        return customer;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string phoneNumber,
        string addressLine1,
        string city,
        string country)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        AddressLine1 = addressLine1;
        City = city;
        Country = country;
    }
}
