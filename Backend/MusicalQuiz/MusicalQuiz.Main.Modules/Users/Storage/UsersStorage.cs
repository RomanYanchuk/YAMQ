using Microsoft.EntityFrameworkCore;

namespace MusicalQuiz.Main.Modules.Users.Storage
{
    public class UsersStorage : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersStorage(DbContextOptions<UsersStorage> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.DateCreated).ValueGeneratedOnAdd();
        }
    }
}