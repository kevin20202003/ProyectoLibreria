using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_autor { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? apellido { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime fechanacimiento { get; set; }
    }
}
