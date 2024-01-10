using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_rol { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? nombre_rol { get; set; }
    }
}
