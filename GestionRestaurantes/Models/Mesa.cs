using System.ComponentModel.DataAnnotations;

namespace GestionRestaurantes.Models
{
    public class Mesa
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Numero de mesa requerido")]
        public string Numero_Mesa { get; set; }
        public string Tipo_Mesa { get; set; }
        public string Estado_Reservado { get; set; }
        public bool Estado { get; set; }

        [Display(Name = "Restaurante")]
        public int RestauranteId { get; set; }
        public Restaurante? Restaurante { get; set; }
        public virtual IEnumerable<Pedido>? Pedidos { get; set; }
    }
}
