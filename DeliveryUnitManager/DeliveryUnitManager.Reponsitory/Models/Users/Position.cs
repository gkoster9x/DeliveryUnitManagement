using System.ComponentModel.DataAnnotations;

namespace DeliveryUnitManager.Reponsitory.Models.Users
{
    public class Positions : BaseModel
    {
        [Key]
        public long ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
