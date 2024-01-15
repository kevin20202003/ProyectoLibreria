using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? nombre_usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? correo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? contrasena { get; set; }
        public string? URLFotoPerfil { get; set; }
    }
}
