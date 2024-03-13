using Microsoft.EntityFrameworkCore;
using UTIN.Entities;

namespace UTIN.DataContext
{
    public class MyDataContext: DbContext
    {
        public MyDataContext(DbContextOptions options):base(options) { }

        public DbSet<users> Users { get; set; }
        public DbSet<user_login> User_login { get; set; }
        public DbSet<menu> Menu { get; set; }
        public DbSet<orders> Orders { get; set; }
        public DbSet<offers> Offers { get; set; }
        public DbSet<admin_login> admin_Login { get; set; }
    }
}
