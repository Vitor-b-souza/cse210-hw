using System.Text;

public class Address
{
    private string street;
    private string city;
    private string stateOrProvince;
    private string country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateOrProvince = stateOrProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return !string.IsNullOrWhiteSpace(country) && country.Trim().ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        var sb = new StringBuilder();
        sb.AppendLine(street);
        sb.AppendLine($"{city}, {stateOrProvince}");
        sb.Append(country);
        return sb.ToString();
    }

    public string Street => street;
    public string City => city;
    public string StateOrProvince => stateOrProvince;
    public string Country => country;
}
