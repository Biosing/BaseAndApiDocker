using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace Database
{
    public class DatabaseContext : AppDbContextBase<DatabaseContext>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}