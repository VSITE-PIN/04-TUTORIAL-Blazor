using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TODO.CoreHosted.Shared.TodoItem> TodoItem { get; set; } = default!;
    }
}
