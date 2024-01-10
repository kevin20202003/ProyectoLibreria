using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLibreria.Models.Entidades
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_venta { get; set; }
        public Usuario Usuarios { get; set; }
        public int id_usuario { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal subtotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal descuento { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal iva { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal total { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime fecha_de_venta { get; set; }
    }
}
