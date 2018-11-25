using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Models.Classes
{
    public partial class Specialization
    {
        public int Id { get; set; }

        [Required]
        [Remote("Unique", "Specializations", AdditionalFields = "Id",
            ErrorMessage = "Specialization name already exists.")]
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}