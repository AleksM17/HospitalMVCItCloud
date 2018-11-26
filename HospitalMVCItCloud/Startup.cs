using System;
using Microsoft.Owin;
using Owin;
using HospitalMVCItCloud.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(HospitalMVCItCloud.Startup))]
namespace HospitalMVCItCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
     
            if (!roleManager.RoleExists("Admin"))
            {  
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                roleManager.Create(roleAdmin);


                var user = new ApplicationUser();
                user.UserName = "administrator";
                user.Email = "admin@test.com";

                string usPas = "ItCloud18!";

                var newAdm = UserManager.Create(user, usPas);
    
                if (newAdm.Succeeded)
                {
                     UserManager.AddToRole(user.Id, "Admin");
                }
            }
   
            if (!roleManager.RoleExists("Moderator"))
            {
                var roleModerator = new IdentityRole();
                roleModerator.Name = "Moderator";
                roleManager.Create(roleModerator);
            }
   
            if (!roleManager.RoleExists("Doctor"))
            {
                var roleDoctor = new IdentityRole();
                roleDoctor.Name = "Doctor";
                roleManager.Create(roleDoctor);
            }

            if (!roleManager.RoleExists("Patient"))
            {
                var roleDoctor = new IdentityRole();
                roleDoctor.Name = "Patient";
                roleManager.Create(roleDoctor);
            }

            if (!roleManager.RoleExists("User"))
            {
                var roleDoctor = new IdentityRole();
                roleDoctor.Name = "User";
                roleManager.Create(roleDoctor);
            }
        }
    }
}
