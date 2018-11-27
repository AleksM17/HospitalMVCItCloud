using HospitalMVCItCloud.Dal;
using HospitalMVCItCloud.Models;
using HospitalMVCItCloud.Models.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private HospitalContext db = new HospitalContext();
        ApplicationDbContext context = new ApplicationDbContext();
       
        public AdminController()
        { }
        
        // GET: Admin
        public ActionResult Index()
        {
            //ViewBag.rolesName = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
            //return View(context.Users);

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      Id = p.UserId,
                                      Name = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(context.Users.First(u => u.Id == id));
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(string userId)
        {
            string currentUser=User.Identity.GetUserId();
            if (currentUser == userId)
            {
                ModelState.AddModelError(string.Empty, "You can't delete yourself");
                return Redirect("/Home/Index");
            }
            //Patient patient = db.Patients.Find(userId);
            //db.Patients.Remove(patient);
            var user = context.Users.First(u => u.Id == userId);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            if (id==string.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
            var user = context.Users.First(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolesName = userManager.GetRoles(user.Id).First();

            return View(user);
        }
        // GET: /User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == string.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
            var user = context.Users.First(u => u.Id == id);
            if (user.Id == string.Empty)
                return HttpNotFound();
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name=user.UserName,
                Email = user.Email,
                Role = userManager.GetRoles(user.Id)[0].ToString()
            };

            return View(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userView)
        {
            if (userView == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                var userUpdate = context.Users.First(u => u.Id == userView.Id);
                userUpdate.UserName = userView.Name;
                ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                           .GetUserManager<ApplicationUserManager>();
                //string origRole = userManager.GetRoles(userView.Id).First().ToString();
                foreach (var role in context.Roles.ToArray())
                {
                    userManager.RemoveFromRole(userUpdate.Id, role.Name);
                }
                //if (!origRole.ToUpper().Equals(userUpdate.Roles.ToString().ToUpper()))
                //{
                //    if (userView.Role.ToString().Equals("Doctor"))
                //    {
                //        Doctor newDoc = new Doctor()
                //        {
                //            Name = userUpdate.UserName,
                //        };
                //    }
                //    else if (userView.Role.ToString().Equals("Patient"))
                //    {
                //        Patient newPatient = new Patient()
                //        {
                //            Name = userUpdate.UserName
                //        };
                //    }
                //}
                userManager.AddToRole(userUpdate.Id, userView.Role);
               
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rolesName = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
            return View(userView);
        }
    }
}