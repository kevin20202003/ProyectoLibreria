using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idcategoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? categoria { get; set; }

        [DataType(DataType.MultilineText)]
        public string? descripcion { get; set; }
    }
}
