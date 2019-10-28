using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Cafeteria
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(60)]
        [Display(Name = "Nombre de cafeteria")]
        public string Descripcion { get; set; }
        public virtual int CampusId { get; set; }
        [ForeignKey("CampusId")]
        public virtual Campus Campus { get; set; }
        public string Encargado { get; set; }
        public Boolean Estado { get; set; }
    }
    public class CafeteriaDbContext : ApplicationDbContext
    {
        public new DbSet<Cafeteria> Cafeterias { get; set; }
    }
}