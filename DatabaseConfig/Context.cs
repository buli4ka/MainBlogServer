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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // // modelBuilder.Entity<Post>()
            // //     .HasOne(p => p.Author)
            // //     .WithMany(b => b.Posts)
            // //     .HasForeignKey(p=>p.AuthorId);
            // //
            // // modelBuilder.Entity<User>()
            // //     .HasMany(p => p.Posts)
            // //     .WithOne();
            // // modelBuilder.Entity<PostComment>()
            // //     .HasOne(p => p.Author)
            // //     .WithOne()
            // modelBuilder.Entity<PostComment>()
            // .HasOptional<User>(p => p.Author)
            // modelBuilder.Entity<PostComment>()
            //     .HasOne(e => e.User)
            //     .WithMany()
            //     .HasForeignKey(x => x.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.Entity<User>()
            //     .HasMany(e => e.CommentedPosts)
            //     .WithOne(e => e.Author)
            //     .onDelete(DeleteBehavior.)
            //     ;
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Post>()
            .HasMany(e => e.PostImages)
            .WithOne()
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);

        }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}