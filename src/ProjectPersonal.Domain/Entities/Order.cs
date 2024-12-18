using ProjectPersonal.Domain.Common;
using ProjectPersonal.Domain.Enum;
namespace ProjectPersonal;

public partial class Order : EntityAuditBase
{
    public Guid? UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public EStatus? Status { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }

}
