using DeliveryUnitManager.Reponsitory.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.ApiModels
{
    public class PositionApi
    {
        public long ID { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public PositionApi(Positions position)
        {
            ID = position.ID;
            Name = position.Name;
            Code=position.Code;
            Description = position.Description;

        }
    }
}
