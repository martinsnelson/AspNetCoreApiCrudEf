using AspNetCoreApiCrudEf.Models.Tarefa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiCrudEf.Interface.DAL
{
    public interface ITarefaDAL
    {
        IEnumerable<Tarefa> ObterTarefas();
        Task<Tarefa> ObterTarefa(long id);
        Task<Tarefa> InserirTarefa(Tarefa tarefa);
        Task<Tarefa> AlterarTarefa(Tarefa tarefa);
        Task<Tarefa> Put(Tarefa tarefa);
        Task<bool> DeletarTarefa(long id);
    }
}
