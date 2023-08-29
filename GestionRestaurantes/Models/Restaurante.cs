using System.ComponentModel.DataAnnotations;

namespace GestionRestaurantes.Models
{
    public class Restaurante
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre_Restaurante { get; set; }
        [Required(ErrorMessage = "Telefono requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Correo requerido")]
        public string Correo { get; set; }
        public DateTime Fecha_Apertura { get; set; }
        public bool Estado { get; set; }

        public virtual IEnumerable<Mesa>? Mesas { get; set; }
    }
}
