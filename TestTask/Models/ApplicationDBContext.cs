using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;

namespace TestTask.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PasswordHasher<User> passwordHasher = new();

            User user1 = new User { Id = 1, Username = "User1", Password = "passwd", First_Name = "Name1", Last_Name = "Surname1" };
            User user2 = new User { Id = 2, Username = "User2", Password = "superpasswd", First_Name = "Name2", Last_Name = "Surname2" };
            User user3 = new User { Id = 3, Username = "User3", Password = "coolpasswd", First_Name = "Name3", Last_Name = "Surname3" };

            string hashedPassword;
            hashedPassword = passwordHasher.HashPassword(user1, user1.Password);
            user1.Password = hashedPassword;

            hashedPassword = passwordHasher.HashPassword(user2, user2.Password);
            user2.Password = hashedPassword;

            hashedPassword = passwordHasher.HashPassword(user3, user3.Password);
            user3.Password = hashedPassword;

            modelBuilder.Entity<User>().HasData(user1, user2, user3);
        }
    }
}
