using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Articulo
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public virtual int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public virtual Marca Marca { get; set; }
        [Range(1, double.MaxValue)]
        public double Costo { get; set; }
        public virtual int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }
        [Range(0,999)]
        public int Existencia { get; set; }
        public Boolean Estado { get; set; }
    }

    public class ArticuloDbContext : ApplicationDbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
    }
}