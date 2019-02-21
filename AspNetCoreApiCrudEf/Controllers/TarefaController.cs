using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiCrudEf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApiCrudEf.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;

        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Construtor
        /// Cria um novo TarefaItem se a coleção estiver vazia,
        /// o que significa que você não pode excluir todos as TarefasItems.
        /// </summary>
        /// <param name="context"></param>
        public TarefaController(TarefaContext context)
        {
            _context = context;

            if (_context.TarefaItems.Count() == 0)
            {
                _context.TarefaItems.Add(new TarefaItem { Nome = "Item1" });
                _context.SaveChanges();
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
        public async Task<ActionResult<IEnumerable<TarefaItem>>> ObterTarefaItems()
        {
            return await _context.TarefaItems.ToListAsync();
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
        public async Task<ActionResult<TarefaItem>> ObterTarefaItem(long id)
        {
            var tarefaItem = await _context.TarefaItems.FindAsync(id);

            if (tarefaItem == null)
            {
                return NotFound();
            }

            return tarefaItem;
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
        //  POST: api/Tarefa
        [HttpPost]
        public async Task<ActionResult<TarefaItem>> InserirTarefaItem(TarefaItem item)
        {
            _context.TarefaItems.Add(item);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(ObterTarefaItem), new { id = item.Id }, item);
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
        public async Task<IActionResult> AlterarTarefaItem(long id, TarefaItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Tarefa/1
        [HttpPatch("{id}")]
        public async Task<IActionResult> AlteracaoParcial(long id, TarefaItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <Author>Nelson Martins</Author>
        /// <Date>21/02/19</Date>
        /// <summary>
        /// Status code 204 No Content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Tarefa/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefaItem(long id)
        {
            var tarefaItem = await _context.TarefaItems.FindAsync(id);

            if (tarefaItem == null)
            {
                return NotFound();
            }

            _context.TarefaItems.Remove(tarefaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}