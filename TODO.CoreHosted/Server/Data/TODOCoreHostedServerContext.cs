using Microsoft.EntityFrameworkCore;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; } = default!;
    }
}