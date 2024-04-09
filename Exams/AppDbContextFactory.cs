using Microsoft.EntityFrameworkCore;

namespace Exams
{
    internal class AppDbContextFactory
    {
        private readonly string _connectionString;

        public AppDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseNpgsql(_connectionString)
                .Options); ;
        }
    }
}
