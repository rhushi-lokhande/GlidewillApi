using Microsoft.EntityFrameworkCore;

namespace GlidewillApi.Models
{
    public class MySQLDBContext:DbContext
    {
        public DbSet<User> User { get; set; }
        public MySQLDBContext(DbContextOptions<MySQLDBContext> options) : base(options) { }
    }
}
