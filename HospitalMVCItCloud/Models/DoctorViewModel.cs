using HospitalMVCItCloud.Models.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HospitalMVCItCloud.Models
{
    public class DoctorViewModel
    {
        public Doctor Doctor { get; set; }
        public IEnumerable<SelectListItem> AllPatients { get; set; }

        private List<int> _selectedPatients;
        public List<int> SelectedPatients
        {
            get
            {
                if (_selectedPatients == null)
                {
                    _selectedPatients = Doctor.Patients.Select(m => m.Id).ToList();
                }
                return _selectedPatients;
            }
            set { _selectedPatients = value; }
        }
    }
}