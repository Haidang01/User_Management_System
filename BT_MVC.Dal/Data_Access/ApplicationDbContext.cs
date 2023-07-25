using BT_MVC_Web.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BT_MVC_Web.Data_Access
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> db) : base(db)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<Ward> Wards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<District>()
                .HasMany(d => d.Wards)
                .WithOne(w => w.District)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasOne(x => x.Ward).WithMany()
               .HasForeignKey(x => x.WardId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasOne(x => x.District).WithMany()
                .HasForeignKey(x => x.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasOne(x => x.City).WithMany()
                .HasForeignKey(x => x.CityId);
            modelBuilder.Entity<Employee>().HasOne(x => x.Ethnicity).WithMany()
                .HasForeignKey(x => x.EthnicityId);
            modelBuilder.Entity<Employee>().HasOne(x => x.Occupation).WithMany()
                .HasForeignKey(x => x.OccupationId);

        }
    }
}
