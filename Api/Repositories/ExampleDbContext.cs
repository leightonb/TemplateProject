using Microsoft.EntityFrameworkCore;
using TemplateProject.Repositories.Models;

namespace TemplateProject.Repositories;

public class TemplateProjectDbContext : DbContext
{
   public DbSet<Manufacturer> Manufacturers { get; set; }
   public DbSet<Brand> Brands { get; set; }
   public DbSet<Region> Regions { get; set; }

   public DbSet<Locale> Locales { get; set; }

   public DbSet<Dealership> Dealerships { get; set; }
   public DbSet<UserAccess> UserAccess { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Vehicle> Vehicles { get; set; }

   public string DbPath { get; }

   public TemplateProjectDbContext()
   {
#if DEBUG
      DbPath = "TemplateProject.db";
#else
      var folder = Environment.SpecialFolder.LocalApplicationData;
      var path = Environment.GetFolderPath(folder);
      DbPath = Path.Join(path, "TemplateProject.db");
#endif
   }

   protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      /* Manufacturer */
      modelBuilder.Entity<Manufacturer>()
         .HasMany(e => e.Brands)
         .WithOne(e => e.Manufacturer)
         .HasForeignKey(e => e.ManufacturerId)
         .HasPrincipalKey(e => e.Id);
      modelBuilder.Entity<Manufacturer>()
         .HasMany(e => e.Vehicles)
         .WithOne(e => e.Manufacturer)
         .HasForeignKey(e => e.ManufacturerId)
         .HasPrincipalKey(e => e.Id);

      /* Brand */
      modelBuilder.Entity<Brand>()
         .HasMany(e => e.Regions)
         .WithOne(e => e.Brand)
         .HasForeignKey(e => e.BrandId)
         .HasPrincipalKey(e => e.Id);

      /* Region */
      modelBuilder.Entity<Region>()
         .HasMany(e => e.Locales)
         .WithOne(e => e.Region)
         .HasForeignKey(e => e.RegionId)
         .HasPrincipalKey(e => e.Id);

      /* Locale */
      modelBuilder.Entity<Locale>()
         .HasMany(e => e.Dealerships)
         .WithOne(e => e.Locale)
         .HasForeignKey(e => e.LocaleId)
         .HasPrincipalKey(e => e.Id);

      /* User Access */
      modelBuilder.Entity<UserAccess>()
         .HasOne(e => e.User)
         .WithMany(e => e.UserAccess)
         .HasForeignKey(e => e.UserId)
         .HasPrincipalKey(e => e.Id);
      modelBuilder.Entity<UserAccess>()
         .HasOne(e => e.Manufacturer)
         .WithMany(e => e.UserAccess)
         .HasForeignKey(e => e.ManufacturerId)
         .HasPrincipalKey(e => e.Id);
      modelBuilder.Entity<UserAccess>()
         .HasOne(e => e.Brand)
         .WithMany(e => e.UserAccess)
         .HasForeignKey(e => e.BrandId)
         .HasPrincipalKey(e => e.Id);
      modelBuilder.Entity<UserAccess>()
         .HasOne(e => e.Dealership)
         .WithMany(e => e.UserAccess)
         .HasForeignKey(e => e.DealershipId)
         .HasPrincipalKey(e => e.Id);
   }

   public async Task<User?> FindUser(string username, string password)
   {
      return await Users.FindAsync(username, password);
   }
}
