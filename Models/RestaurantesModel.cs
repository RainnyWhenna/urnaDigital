using System.ComponentModel.DataAnnotations;

namespace UrnaDigital.Models
{
    public class RestaurantesModel
    {
        [Key]
        public int IdRestaurante { get; set; }
        public string NomeRestaurante { get; set; }
        public DateTime? EscolhidoEm { get; set; }
    }
}
