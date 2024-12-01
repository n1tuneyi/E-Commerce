namespace Domain.Request.Product;

public class ProductParameters : RequestParameters
{
    public ProductParameters()
    {
        OrderBy = "Price";
    }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; } = int.MaxValue;
    public bool ValidPriceRange => MaxPrice > MinPrice && MinPrice >= 0;

    public string? SearchTerm { get; set; }
}
