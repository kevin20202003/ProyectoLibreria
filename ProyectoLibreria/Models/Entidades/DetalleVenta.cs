using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_detalle_venta { get; set; }
        public Venta Ventas { get; set; }
        public int id_venta { get; set; }
        public int id_libro { get; set; }
        public Libro Libros { get; set; }
        public int cantidad { get; set; }
    }
}
