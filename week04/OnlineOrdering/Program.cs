using System;

class Program
{
    static void Main(string[] args)
    {
        var addressUs = new Address("123 Main St", "Springfield", "IL", "USA");
        var customerUs = new Customer("Alice Johnson", addressUs);

        var order1 = new Order(customerUs);
        order1.AddProduct(new Product("Notebook", "NB-001", 3.5m, 5));
        order1.AddProduct(new Product("Pen", "PN-010", 1.25m, 10));
        order1.AddProduct(new Product("Water Bottle", "WB-403", 8.99m, 1));

        Console.WriteLine("=== Order 1 ===");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.CalculateTotalPrice():0.00}");
        Console.WriteLine();

        var addressNonUs = new Address("456 Oak Road", "Toronto", "ON", "Canada");
        var customerNonUs = new Customer("Bruno Silva", addressNonUs);

        var order2 = new Order(customerNonUs);
        order2.AddProduct(new Product("T-shirt", "TS-204", 15.00m, 2));
        order2.AddProduct(new Product("Hat", "HT-007", 12.50m, 1));

        Console.WriteLine("=== Order 2 ===");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.CalculateTotalPrice():0.00}");
        Console.WriteLine();
    }
}
