using Microsoft.EntityFrameworkCore;
using Models.Docs;
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
        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<Doc> Docs { get; set; }
    }
}