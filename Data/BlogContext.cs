using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
     public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options):base(options){}
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

       /*protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");*/

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           modelBuilder.Entity<Blog>().ToTable("Blog");
           modelBuilder.Entity<Post>().ToTable("Post");
       }
    }
}