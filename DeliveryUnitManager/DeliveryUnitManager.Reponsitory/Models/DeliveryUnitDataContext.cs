using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DeliveryUnitManager.Repository.Models
{
    public class DeliveryUnitDataContext : DbContext
    {
        public DbSet<Users> Users { set; get; }
        public DbSet<Roles> Roles { set; get; }
        public DbSet<Permissions> Permissions { set; get; }

        public DbSet<UserRoles> UserRoles { set; get; }
        public DbSet<RolePermissions> RolePermissions { set; get; }

        public DbSet<Positions> Positions { set; get; }

        public DeliveryUnitDataContext(DbContextOptions<DeliveryUnitDataContext> options) : base(options)
        {

        }
        const string connectionString = @"Server=(local);Database=DeliveryUnitManager;Trusted_Connection=True;MultipleActiveResultSets=true";
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(connectionString);
        }
    }
}
