public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address != null && address.IsInUSA();
    }

    public string Name => name;
    public Address Address => address;
}
