using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Marca
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; }
    }

    public class MarcaDbContext : ApplicationDbContext
    {
        public new DbSet<Marca> Marcas { get; set; }
    }
}