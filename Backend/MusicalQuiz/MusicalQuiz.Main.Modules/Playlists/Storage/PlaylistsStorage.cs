using Microsoft.EntityFrameworkCore;

namespace MusicalQuiz.Main.Modules.Playlists.Storage
{
    public class PlaylistsStorage : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        public PlaylistsStorage(DbContextOptions<PlaylistsStorage> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>().Property(u => u.DateCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Playlist>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Playlist>().Property(u => u.DateCreated).ValueGeneratedOnAdd();
        }
    }
}