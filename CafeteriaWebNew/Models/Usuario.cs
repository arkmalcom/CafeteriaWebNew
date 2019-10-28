using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre de usuario")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(13)]
        public string Cedula { get; set; }
        public string Tipo_UsuarioId { get; set; }
        [ForeignKey("Tipo_UsuarioId")]
        public virtual Tipo_Usuario Tipo_Usuario { get; set; }
        [Range(0, double.MaxValue)]
        [Display(Name = "Limite de credito")]
        public double LimiteCredito { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }
        public Boolean Estado { get; set; }

    }

    public class UsuarioDbContext : ApplicationDbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}