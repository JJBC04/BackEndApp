using BackEndApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndApp.Data
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options)
        {

        }
        //Dbset
        public DbSet<Comment> Comments { get; set; }
    }
}
