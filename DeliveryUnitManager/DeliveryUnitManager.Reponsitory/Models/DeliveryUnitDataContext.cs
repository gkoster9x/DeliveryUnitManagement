using DeliveryUnitManager.Reponsitory.Models.BankingQuestionInterview;
using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DeliveryUnitManager.Repository.Models
{
    public class DeliveryUnitDataContext : DbContext
    {
        #region user login
        public DbSet<Users> Users { set; get; }
        public DbSet<Roles> Roles { set; get; }
        public DbSet<Permissions> Permissions { set; get; }

        public DbSet<UserRoles> UserRoles { set; get; }
        public DbSet<RolePermissions> RolePermissions { set; get; }

        public DbSet<Positions> Positions { set; get; }
        #endregion

        #region BankingQuestionInterview
        public DbSet<QuestionInterviews> Questions { set; get; }
        public DbSet<AnswerInterviews> Answers { set; get; }
        public DbSet<ProjectDevelop> ProjectDevelops { set; get; }

        #endregion

        const string connectionString = @"Server=192.168.86.142;Database=DeliveryUnitManager;User Id=webapp;Password=123;";
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(connectionString);
        }
    }
}
