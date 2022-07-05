using System.ComponentModel.DataAnnotations;

namespace DeliveryUnitManager.Reponsitory.Models.Users
{
    public class Permissions : BaseModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string? Description { get; set; }


        public virtual ICollection<RolePermissions> RolePermissions { get; set; }

    }
}
