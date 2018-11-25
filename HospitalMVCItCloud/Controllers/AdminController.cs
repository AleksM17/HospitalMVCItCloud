using HospitalMVCItCloud.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public AdminController()
        { }
        
        // GET: Admin
        public ActionResult Index()
        {
            return View(context.Users);
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
                return View("Delete");
            }
            var user = context.Users.First(u => u.Id == userId);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /User/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

       
    }
}