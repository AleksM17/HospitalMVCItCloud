using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Models.Classes
{
    public enum Status
    {
        Arrived = 1,
        Sick = 2,
        Healthy = 3
    }
    public partial class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Name";

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DayOfBirth { get; set; } = new DateTime();

        public Status? Status { get; set; }

        [Required]
        public string TaxCode { get; set; }

        public virtual ICollection<Patient> Doctors { get; set; }
    }
}