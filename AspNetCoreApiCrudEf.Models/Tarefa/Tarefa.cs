using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AspNetCoreApiCrudEf.Models.Tarefa
{
    [DataContract]
    public class Tarefa
    {
        [Key]
        [DataMember]
        public long Id { get; set; }
        [Required]
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public bool Concluido { get; set; }
    }
}
