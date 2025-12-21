public class Product
{
    private decimal _price;

    public decimal Price => _price;
    public string Name { get; }

    void Product(string name, decimal price)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace");
        }
        Name = name;
        SetPrice(price);
    }

    void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price must be non-negative");

        _price = price;
    }

    void ApplyDiscount(decimal percent)
    {
        if (percent < 0 || percent > 100)
            throw new ArgumentException("Discount percent must be between 0 and 100");

        _price = _price - (_price * percent / 100m);
    }
}