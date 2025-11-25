using System.Collections.Generic;
using System.Text;

public class Order
{
    private Customer customer;
    private List<Product> products;
    private const decimal UsShipping = 5m;
    private const decimal InternationalShipping = 35m;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        if (product != null)
        {
            products.Add(product);
        }
    }

    public decimal CalculateTotalPrice()
    {
        decimal sum = 0m;
        foreach (var p in products)
        {
            sum += p.GetTotalCost();
        }

        decimal shipping = customer != null && customer.LivesInUSA() ? UsShipping : InternationalShipping;
        return sum + shipping;
    }

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Packing Label:");
        foreach (var p in products)
        {
            sb.AppendLine($"- {p.Name} (ID: {p.ProductId})");
        }
        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Shipping Label:");
        sb.AppendLine(customer.Name);
        sb.AppendLine(customer.Address.GetFullAddress());
        return sb.ToString();
    }

    public IReadOnlyList<Product> Products => products.AsReadOnly();
    public Customer Customer => customer;
}
