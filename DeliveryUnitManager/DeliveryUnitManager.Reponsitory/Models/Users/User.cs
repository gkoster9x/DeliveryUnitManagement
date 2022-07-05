using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryUnitManager.Reponsitory.Models.Users
{
    public class Users : BaseModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Code { get; set; }
        public long UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(500)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string? Email { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(15,MinimumLength =10)]
        public string? PhoneNumber { get; set; }
        public long PositionId { get; set; }
        public string? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DoB { get; set; }

        [ForeignKey("PositionId")]
        public virtual Positions Position { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }

    }
}
