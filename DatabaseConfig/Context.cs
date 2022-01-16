using System.Linq;
using Microsoft.EntityFrameworkCore;
using Server.Models.PostModels;
using Server.Models.UserModels;

namespace Server.DatabaseConfig
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<UserIcon> UserIcons { get; set; }
        
        public DbSet<PostComment> PostComments { get; set; }
        
        public DbSet<PostLike> PostLikes { get; set; }
        
        // public DbSet<PostSubComment> PostSubComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Post>()
                .HasOne(e => e.Author)
                .WithMany(e => e.Posts)
                .OnDelete(DeleteBehavior.ClientCascade);
            
          
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}