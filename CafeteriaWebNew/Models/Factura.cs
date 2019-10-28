using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Factura
    {
        [Key]
        public int ID { get; set; }
        public virtual int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public virtual Empleado Empleado { get; set; }
        public virtual int ArticuloId { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
        public virtual int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de venta")]
        public DateTime FechaVenta { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Monto { get; set; }
        [Required]
        [Range(0, 999)]
        public int Cantidad { get; set; }
        public string Comentario { get; set; }
        public Boolean Estado { get; set; }
        
    }

    public class FacturaDbContext : ApplicationDbContext
    {
        public DbSet<Factura> Facturas { get; set; }
    }
}