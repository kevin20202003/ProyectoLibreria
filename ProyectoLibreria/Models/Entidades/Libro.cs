using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_libro { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoria.")]
        public Categoria Categorias { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Categoria { get; set; }
        public int id_categoria { get; set; }
        public Autor Autores { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un autor.")]
        [NotMapped]
        public IEnumerable<SelectListItem> Autor { get; set; }
        public int id_autor { get; set; }
        public Editorial Editoriales { get; set; }
        public int id_editorial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? titulo { get; set; }
        public int anio { get; set; }
        public bool estado { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal precio { get; set; }
        public DateTime fecha_registro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? url_libro { get; set; }
    }
}
