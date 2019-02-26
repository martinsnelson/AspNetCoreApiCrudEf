using AspNetCoreApiCrudEf.Interface.DAL;
using AspNetCoreApiCrudEf.Models.Tarefa;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiCrudEf.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        //private readonly TarefaContext _context; // remover

        private readonly ITarefaDAL _iTarefaDAL;

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Construtor
        /// Cria um novo TarefaItem se a coleção estiver vazia,
        /// o que significa que você não pode excluir todos as TarefasItems.
        /// </summary>
        /// <param name="context"></param>
        public TarefaController(ITarefaDAL iTarefaDAL)
        {
            _iTarefaDAL = iTarefaDAL;

            if (_iTarefaDAL.ObterTarefas().Count()  == 0)
            {
                //_context.TarefaItems.Add(new Tarefa { Nome = "Item1" });
                //_iTarefaDAL.Ins .Add(new Tarefa { Nome = "Item1" });
                //_context.SaveChanges();
            }
        }

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 200 OK
        /// </summary>
        /// <returns></returns>
        // GET: api/Tarefa
        [HttpGet]
        public IEnumerable<Tarefa> ObterTarefas()
        {
            //return await _context.TarefaItems.ToListAsync();
            return _iTarefaDAL.ObterTarefas();
        }

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 200 OK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //  GET: api/Tarefa/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterTarefa(long id)
        {
            var tarefa = await _iTarefaDAL.ObterTarefa(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
            //return Ok(livro);
        }

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 201 Created
        /// O método CreatedAtAction
        ///  retorna um código de status HTTP 201 em caso de êxito. HTTP 201 é a resposta padrão para um método HTTP POST que cria um novo recurso no servidor.
        ///  Faz referência à ação GetTodoItem para criar o URI de Location do cabeçalho
        ///  palavra-chave nameof do C# é usada para evitar o hard-coding do nome da ação, na chamada CreatedAtAction
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        // POST: api/Tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefa>> InserirTarefa([FromBody] Tarefa tarefa)
        {
            //_iTarefaDAL.ObterTarefa.Add(tarefa);
            await _iTarefaDAL.InserirTarefa(tarefa);
            
            return CreatedAtAction(nameof(ObterTarefa), new { id = tarefa.Id }, tarefa);
        }

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 204 No Content
        ///  De acordo com a especificação de HTTP, uma solicitação PUT exige que o cliente envie a 
        ///  entidade inteira atualizada, não apenas as alterações. Para dar suporte a atualizações parciais, 
        ///  use HTTP PATCH.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        //  PUT: api/Tarefa/1
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarTarefa(long id, [FromBody] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }
            try
            {
                await _iTarefaDAL.Put(tarefa);
            }
            catch (System.Exception e)
            {

                throw;
            }
            return NoContent();
        }

        //// PATCH: api/Tarefa/1
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> AlteracaoParcial(long id, Tarefa item)
        //{
        //    await _iTarefaDAL.AlterarTarefa(item);

        //    return NoContent();
        //}


        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 204 No Content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Tarefa/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(long id)
        {
            var tarefa = await _iTarefaDAL.ObterTarefa(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            await _iTarefaDAL.DeletarTarefa(id);

            return NoContent();
        }
    }
}