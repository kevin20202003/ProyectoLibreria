using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Editorial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_editorial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? nombre { get; set; }
    }
}
