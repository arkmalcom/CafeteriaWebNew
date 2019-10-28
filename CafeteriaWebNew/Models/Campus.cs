using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Campus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(60)]
        [Display(Name = "Nombre de campus")]
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; }
    }

    public class CampusDbContext : ApplicationDbContext
    {
        public new DbSet<Campus> Campus { get; set; }
    }
}