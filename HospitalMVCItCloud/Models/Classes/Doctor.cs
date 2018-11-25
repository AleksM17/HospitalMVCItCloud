using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalMVCItCloud.Models.Classes
{
    public partial class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}