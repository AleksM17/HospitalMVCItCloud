using HospitalMVCItCloud.Dal;
using HospitalMVCItCloud.Models;
using HospitalMVCItCloud.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly HospitalContext db = new HospitalContext();
 
        // GET: /Doctors/
        public ActionResult Index(string searchname)
        {
            var doctors = db.Doctors.Include(j => j.Specialization);
            return View(doctors.Where(p => p.Name.ToLower().Contains(searchname.ToLower()) || searchname == null).ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: /Doctor/Create
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create()
        {
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "Name");
            return View();
        }
       
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SpecializationId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "Name", doctor.SpecializationId);
            return View(doctor);
        }


        // GET: /Doctor/Edit/5
        [Authorize(Roles = "Admin, Moderator", Users ="admin@test.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctorViewModel = new DoctorViewModel
            {
                Doctor = db.Doctors.Include(i => i.Patients).First(i => i.Id == id)
            };

            if (doctorViewModel.Doctor == null)
                return HttpNotFound();
            var allPatientsList = db.Patients.ToList();

            doctorViewModel.AllPatients = allPatientsList.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });

            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "Name", doctorViewModel.Doctor.SpecializationId);
            return View(doctorViewModel);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Moderator", Users = "admin@test.com")]
        [HttpPost]
        [ValidateAntiForgeryToken]//[Bind(Include = "Name,Id,SpecializationID,SelectedPatients")]
        public ActionResult Edit(DoctorViewModel doctorView)
        {
            if (doctorView == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                var docUpdate = db.Doctors
                    .Include(i => i.Patients).First(i => i.Id == doctorView.Doctor.Id);

                if (TryUpdateModel(docUpdate, "Doctor", new string[] { "Name", "SpecializationID" }))
                {
                    var newPatients = db.Patients.Where(
                        p => doctorView.SelectedPatients.Contains(p.Id)).ToList();
                    var updatedPatients = new HashSet<int>(doctorView.SelectedPatients);
                    foreach (Patient patient in db.Patients)
                    {
                        if (!updatedPatients.Contains(patient.Id))
                        {
                            docUpdate.Patients.Remove(patient);
                        }
                        else
                        {
                            docUpdate.Patients.Add((patient));
                        }
                    }

                    db.Entry(docUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "Name", doctorView.Doctor.SpecializationId);
            return View(doctorView);
        }

        // GET: /Doctor/Delete/5
        [Authorize(Roles = "Admin", Users = "admin@test.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Doctor doctor = db.Doctors
                .Include(j => j.Patients)
                .First(j => j.Id == id);

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [Authorize(Roles = "Admin", Users = "admin@test.com")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors
                 .Include(j => j.Patients)
                 .First(j => j.Id == id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}