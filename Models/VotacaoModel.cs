using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrnaDigital.Models
{
    public class VotacaoModel
    {
        [Key]
        public int IdVotacao { get; set; }
        public DateTime DtVotacao { get; set; }

        [ForeignKey("IdFuncionario")]
        public int IdFuncionario { get; set; }

        [ForeignKey("IdRestaurante")]
        public int IdRestaurante { get; set; }
    }
}
