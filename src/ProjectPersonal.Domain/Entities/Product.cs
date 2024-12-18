using ProjectPersonal.Domain.Common;
using ProjectPersonal.Domain.Entities;
namespace ProjectPersonal;

public partial class Product : EntityAuditBase
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? Stock { get; set; }
    public Guid CategoryID { get; set; }
    public string ImageURL { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public virtual Categories Categories { get; set; }
    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
