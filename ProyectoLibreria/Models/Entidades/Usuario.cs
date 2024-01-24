using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoLibreria.Models.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Rol? Rol { get; set; }
        public string? nombre_usuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? correo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? contrasena { get; set; }
        public string? URLFotoPerfil { get; set; }
        [Display(Name = "Role")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un rol.")]
        public int RolId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
