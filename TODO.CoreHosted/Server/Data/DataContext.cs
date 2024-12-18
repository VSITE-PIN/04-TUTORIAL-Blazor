using Microsoft.EntityFrameworkCore;

namespace TODO.CoreHosted.Server.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		public DbSet<TODO.CoreHosted.Shared.TodoItem> TodoItem { get; set; } = default!;
	}
}
