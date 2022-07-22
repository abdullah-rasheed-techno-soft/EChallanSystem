using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)       
        {

        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<TrafficWarden> TrafficWardens { get; set; }
        public DbSet<Challan> Challans { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ChallanEmail> ChallanEmails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //modelBuilder.Entity<Citizen>(b =>
            //{
            //    b.HasData(new Citizen
            //    {
            //        Id = 1,
            //        UserId = 2,

            //    });
            //    b.OwnsOne(c => c.User).HasData(new
            //    {
            //        Id = 2,
            //        Name = "Ali",
            //        Email = "ali@gmail.com",
            //        Role = Roles.Citizen
            //    });
            //    b.HasMany(c => c.Vehicle);
            //    b.HasMany(c => c.ChallanEmails);
            //});
            //modelBuilder.Entity<Vehicle>(b =>
            //{
            //    b.HasData(new Vehicle
            //    {
            //        Id = 1,
            //        CitizenId = 1,
            //        Name="Mehran",

            //    });
            //    b.HasOne(c => c.Citizen);
            //    b.HasMany(c => c.Challans);
               
            //});
            //modelBuilder.Entity<ChallanEmail>(b =>
            //{
            //    b.HasData(new ChallanEmail
            //    {
            //        Id = 1,
            //        CitizenId = 1,
            //        Subject="Pay Fine",
            //        Message= "Pay Fine",

            //    });
            //    b.HasOne(c => c.Citizen);

            //});
            //modelBuilder.Entity<Manager>(b =>
            //{
            //    b.HasData(new Manager
            //    {
            //        Id = 1,
            //        UserId = 1,
            //    });
            //    b.OwnsOne(c => c.User).HasData(new
            //    {
            //        Id = 1,
            //        Name = "Abdullah",
            //        Email = "abdullah@gmail.com",
            //        Role = Roles.Manager
            //    });
            //});
            //modelBuilder.Entity<TrafficWarden>(b =>
            //{
            //    b.HasData(new TrafficWarden
            //    {
            //        Id = 1,
            //        UserId = 3,
            //    });
            //    b.OwnsOne(c => c.User).HasData(new
            //    {
            //        Id = 3,
            //        Name = "Hayan",
            //        Email = "hayan@gmail.com",
            //        Role = Roles.TrafficWarden
            //    });
            //});
        




            //modelBuilder.Entity<Manager>().HasData(
            //    new Manager
            //    {
            //        Id = 1,
            //        UserId =1,
            //        User = new ApplicationUser()
            //        {
            //            Id=1,
            //            Name="Abdullah",
            //            Email="abdullah@gmail.com",
            //            Role=Roles.Manager

            //        }    
            //    },
            //    new Manager
            //    {
            //        Id = 2,
            //        UserId = 2,
            //        User = new ApplicationUser()
            //        {
            //            Id = 2,
            //            Name = "Ali",
            //            Email = "ali@gmail.com",
            //            Role = Roles.Manager

            //        }
            //    }
            //    );
            //modelBuilder.Entity<Citizen>().HasMany(a => a.Emails).WithOne(b => b.citizen);
            //modelBuilder.Entity<Citizen>().HasOne(a=>a.vehicle).WithOne(b=>b.citizen).HasForeignKey<Vehicle>(b=>b.CitizenId);
            //modelBuilder.Entity<TrafficWarden>().HasMany(a => a.challans).WithOne(b => b.warden);
            //modelBuilder.Entity<Vehicle>().HasMany(a => a.challans).WithOne(b => b.vehicle);

        }
    }
}
