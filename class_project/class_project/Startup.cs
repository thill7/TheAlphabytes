using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using class_project.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(class_project.Startup))]
namespace class_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                IdentityResult result = roleManager.Create(role);

                string email = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminEmail"];
                string password = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"];
                ApplicationUser user = new ApplicationUser { UserName = email, Email = email };

                result = UserManager.Create(user, password);

                if (result.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
