using AspNetCoreApiCrudEf.Models.Tarefa;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApiCrudEf.DAL.Contexts
{
    //[Table("Tarefa")]
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tarefa>()
                .HasIndex(u => u.Id)
                .IsUnique();
        }
    }
}
