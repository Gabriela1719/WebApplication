using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                Settings = new AppConfiguration();
                OpsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                OpsBuilder.UseSqlServer(Settings.SqlConnectionStrnig);
                DbOptions = OpsBuilder;
            }
            public DbContextOptionsBuilder<DatabaseContext> OpsBuilder { get; set; }
            public DbContextOptionsBuilder<DatabaseContext> DbOptions { get; set; }

            private AppConfiguration Settings { get; set; }
        }
        public static OptionsBuild ops = new OptionsBuild();
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
