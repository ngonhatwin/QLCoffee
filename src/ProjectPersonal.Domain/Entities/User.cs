using ProjectPersonal.Domain.Common;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;
namespace ProjectPersonal;

public partial class User : EntityAuditBase
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public Role? Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
