using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApiCrudEf.Models
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext>options) : base(options)
        {
        }

        public DbSet<TarefaItem> TarefaItems { get; set; }
    }
}
