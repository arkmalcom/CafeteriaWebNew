using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CafeteriaWebNew.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Campus> Campus { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Cafeteria> Cafeterias { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Marca> Marcas { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Proveedor> Proveedors { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Articulo> Articuloes { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Tipo_Usuario> IdentityRoles { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Empleado> Empleadoes { get; set; }

        public System.Data.Entity.DbSet<CafeteriaWebNew.Models.Factura> Facturas { get; set; }
    }
}