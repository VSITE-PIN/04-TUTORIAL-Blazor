using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODO.CoreHosted.Client.Shared;

namespace TODO.CoreHosted.Server.Data
{
    public class TODOCoreHostedServerContext : DbContext
    {
        public TODOCoreHostedServerContext (DbContextOptions<TODOCoreHostedServerContext> options)
            : base(options)
        {
        }

        public DbSet<TODO.CoreHosted.Client.Shared.ToDoItem> ToDoItem { get; set; } = default!;
    }
}
