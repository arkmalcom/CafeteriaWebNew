using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Proveedor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Proveedor")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(9)]
        public string RNC { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }
        public Boolean Estado { get; set; }
    }

    public class ProveedorDbContext : ApplicationDbContext
    {
        public DbSet<Proveedor> Proveedores { get; set; }
    }

}