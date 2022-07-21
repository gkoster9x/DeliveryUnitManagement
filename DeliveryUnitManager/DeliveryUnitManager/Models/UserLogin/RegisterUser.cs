namespace DeliveryUnitManager.Models.UserLogin
{
    public class RegisterUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { set; get; }
        public DateTime? DoB { get; set; }

        public string? PhoneNumber { get; set; }
        public long PositionID { get; set; }
        public string? Gender { get; set; }
    }
}
