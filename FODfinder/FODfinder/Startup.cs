using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using FODfinder.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(FODfinder.Startup))]
namespace FODfinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }
        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new IdentityRole("SuperAdmin");
                IdentityResult result = roleManager.Create(role);

                string username = System.Web.Configuration.WebConfigurationManager.AppSettings["SuperAdminUsername"];
                string email = System.Web.Configuration.WebConfigurationManager.AppSettings["SuperAdminEmail"];
                string password = System.Web.Configuration.WebConfigurationManager.AppSettings["SuperAdminPassword"];
                ApplicationUser user = new ApplicationUser { UserName = username, Email = email };

                result = UserManager.Create(user, password);

                if (result.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");
                }
            }

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                IdentityResult result = roleManager.Create(role);
            }
        }
    }
}
