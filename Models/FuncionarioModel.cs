using System.ComponentModel.DataAnnotations;

namespace UrnaDigital.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int IdFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public int Matricula { get; set; }
    }
}
