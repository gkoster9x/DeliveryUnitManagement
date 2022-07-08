using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Reponsitory.Models.ApiModels
{
    public class UserAPI
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { set; get; }
        public DateTime? DoB { get; set; }

        public string? PhoneNumber { get; set; }
        public long PositionID { get; set; }
        public string? Gender { get; set; }

        public UserAPI(Models.Users.Users user)
        {
            Id = user.Id;
            Username = user.Username;
            FullName= user.Fullname;
            Email = user.Email;
            Address = user.Address;
            DoB = user.DoB;
            PhoneNumber = user.PhoneNumber;
            Gender = user.Gender;
            PositionID = user.PositionId;
        }
    }
}
