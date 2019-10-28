using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Empleado
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre de empleado")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(13)]
        public string Cedula { get; set; }
        public Tanda Tanda { get; set; }
        [Range(0,100)]
        [Display(Name = "Porcentaje de comision")]
        public double PorcientoComision { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }
        public Boolean Estado { get; set; }
    }

    public class EmpleadoDbContext : ApplicationDbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
    }

    public enum Tanda
    {
        Matutina,
        Vespertina,
        Nocturna
    }
}