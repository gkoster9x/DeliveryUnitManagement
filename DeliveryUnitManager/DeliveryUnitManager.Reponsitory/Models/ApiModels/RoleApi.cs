using DeliveryUnitManager.Reponsitory.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.ApiModels
{
    public class RoleApi
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public RoleApi(Roles role)
        {
            Name = role.Name;
            Id = role.Id;
        }

    }
}
