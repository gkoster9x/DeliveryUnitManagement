using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryUnitManager.Reponsitory.Models.Users
{
    public class RolePermissions : BaseModel
    {

        [Key]
        public long Id { get; set; }
        public long PermissionId { get; set; }
        public long RoleId { get; set; }
      

        [ForeignKey("PermissionId")]
        public virtual Permissions Permission { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
    }
}
