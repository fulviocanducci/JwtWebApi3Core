using JwtModels;
using Microsoft.EntityFrameworkCore;

namespace JwtDatabase
{
   public class DatabaseContext : DbContext
   {
      public DatabaseContext(DbContextOptions options) : base(options)
      {
      }

      public DbSet<User> User { get; set; }
      public DbSet<Todo> Todo { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {         
         modelBuilder.Entity<User>(o =>
         {
            o.HasKey(x => x.Id);               
            o.Property(x => x.Id);
            o.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode(false);
            o.Property(x => x.Email)
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode(false);
            o.Property(x => x.Password)
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode(false);
         });
         modelBuilder.Entity<Todo>(o =>
         {
            o.HasKey(x => x.Id);
            o.Property(x => x.Id);
            o.Property(x => x.Title)
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode(false);
            o.Property(x => x.Done)
               .IsRequired();
         });
      }
   }
}
