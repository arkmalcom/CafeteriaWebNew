using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaWebNew.Models
{
    public class Tipo_Usuario : IdentityRole
    {
        public Tipo_Usuario() : base() { }
        public Tipo_Usuario(string name) : base(name) { }
        public Boolean Estado { get; set; }
    }

    public class Tipo_UsuarioDbContext : IdentityDbContext
    {
        public DbSet<IdentityRole> AspNetRoles { get; set; }
    }
}