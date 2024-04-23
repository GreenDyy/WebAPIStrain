using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebAPIStrain.Data;

namespace WebAPIStrain.Data
{
    public class MyDbContex : DbContext
    {
        public MyDbContex(DbContextOptions<MyDbContex> options) : base(options)
        {

        }
        #region DbSet
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });
        }

    }
}
