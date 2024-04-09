using Exams.Models;
using Microsoft.EntityFrameworkCore;

namespace Exams
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Question> Questions { get; set; }
    }
}
