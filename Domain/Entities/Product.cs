using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "Money")]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}
