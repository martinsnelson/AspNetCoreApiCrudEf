using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApiCrudEf.Models
{
    //Um modelo é um conjunto de classes que representam os dados gerenciados pelo aplicativo.
    //TodoItem -> TodoItem
    public class TarefaItem
    {
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public bool Concluido { get; set; }
    }
}
