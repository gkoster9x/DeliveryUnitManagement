using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models
{
    public class BaseModel
    {
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
        public DateTime? Updated { get; set; }
        public string? UpdateBy { get; set; }

        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
