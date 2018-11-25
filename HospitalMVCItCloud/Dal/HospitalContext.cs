using HospitalMVCItCloud.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HospitalMVCItCloud.Dal
{
    public partial  class HospitalContext : DbContext
    {
        public HospitalContext():base("DefaultConnection") { }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
    }
}