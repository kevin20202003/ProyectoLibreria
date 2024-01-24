﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLibro { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; }

        public Autor Autor { get; set; }

        public Categoria Categoria { get; set; }

        public Editorial Editorial { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        [Display(Name = "Imagen")]
        public string? URLImagen { get; set; }
        public int year { get; set; }
        public DateTime FechaRegistro { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un autor.")]
        public int AutorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoria.")]
        public int CategoriaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una editorial.")]
        public int EditorialId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Autores { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Editoriales { get; set; }
    }
}
