﻿using System;
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
        public string Nombre { get; set; }
        [Required]
        [MaxLength(13)]
        public string Cedula { get; set; }
        public string Tanda { get; set; }
        [Range(0,100)]
        public double PorcientoComision { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public Boolean Estado { get; set; }
    }

    public class EmpleadoDbContext : ApplicationDbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
    }
}