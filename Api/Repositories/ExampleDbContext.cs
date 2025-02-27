using Microsoft.EntityFrameworkCore;
using TemplateProject.Repositories.Models;

namespace TemplateProject.Repositories;

public class TemplateProjectDbContext : DbContext
{
   public DbSet<User> Users { get; set; }

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
   }

   public async Task<User?> FindUser(string username, string password)
   {
      return await Users.FindAsync(username, password);
   }
}
