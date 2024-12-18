using ProjectPersonal.Domain.Common;
namespace ProjectPersonal.Domain.Entities
{
    public partial class RefreshToken : EntityBase
    {
        public Guid UserId { get; set; }

        public string RefreshTokenHash { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string? IsRevoked { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
