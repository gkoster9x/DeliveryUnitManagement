using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryUnitManager.Reponsitory.Models.Users
{
    public class UserRoles: BaseModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }
      


        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }

    }
}
