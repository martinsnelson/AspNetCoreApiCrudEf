using AspNetCoreApiCrudEf.DAL.Contexts;
using AspNetCoreApiCrudEf.Interface.DAL;
using AspNetCoreApiCrudEf.Models.Tarefa;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiCrudEf.DAL
{
    public class TarefaDAL : ITarefaDAL
    {
        private readonly TarefaContext _tarefaContext;

        public TarefaDAL(TarefaContext tarefaContext)
        {
            _tarefaContext = tarefaContext;
        }

        public IEnumerable<Tarefa> ObterTarefas()
        {
            return _tarefaContext.Tarefas;
            //throw new System.NotImplementedException();
        }

        public async Task<Tarefa> ObterTarefa(long id)
        {
            return await _tarefaContext.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> InserirTarefa(Tarefa tarefa)
        {
            try
            {
                _tarefaContext.Tarefas.Add(tarefa);
                await _tarefaContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw;
            }
            return tarefa;
        }

        public async Task<Tarefa> AlterarTarefa(Tarefa tarefa)
        {
            //_tarefaContext.Entry(tarefa).State = EntityState.Modified;

            //await _tarefaContext.SaveChangesAsync();            

            //return tarefa;

            throw new System.NotImplementedException();
        }

        public async Task<Tarefa> Put(Tarefa tarefa)
        {
            _tarefaContext.Entry(tarefa).State = EntityState.Modified;
            await _tarefaContext.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> DeletarTarefa(long id)
        {
            var tarefa = await _tarefaContext.Tarefas.FindAsync(id);

            _tarefaContext.Tarefas.Remove(tarefa);

            await _tarefaContext.SaveChangesAsync();

            return true;
        }
    }
}
