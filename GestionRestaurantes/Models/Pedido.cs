using System.ComponentModel.DataAnnotations;

namespace GestionRestaurantes.Models
{
    public class Pedido
    {
        [Key]

        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre cliente requerido")]
        public string Nombre_Cliente { get; set; }
        [Required(ErrorMessage = "Orden requerida")]
        public string Orden { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public bool estado { get; set; }
        [Display(Name = "Mesa")]
        public int MesaId { get; set; }
        public Mesa? Mesa { get; set; }
    }
}
