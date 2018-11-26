//using HospitalMVCItCloud.Models.Classes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace HospitalMVCItCloud.Dal
//{
//    public class HospitalInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<HospitalContext>

//    {
//            protected override void Seed(HospitalContext context)
//            {
               
//                var specializations = new List<Specialization>
//            {
//            new Specialization{Id=1050,Name="Surgion"},
//            new Specialization{Id=4022,Name="Dantist"},
//            new Specialization{Id=4041,Name="Ginecologist"},
//            new Specialization{Id=1045,Name="Anastezilogist"},
//            new Specialization{Id=3141,Name="Cardiologist"},
//            new Specialization{Id=2021,Name="Neirosurdion"},
//            new Specialization{Id=2042,Name="Patalogist"}
//            };
//            specializations.ForEach(s => context.Specializations.Add(s));
//                context.SaveChanges();
//            var patients = new List<Patient>
//            {
//            new Patient{Name="Carson",Status=Status.Arrived,DayOfBirth=DateTime.Parse("2005-09-01")},
//            new Patient{Name="Meredith",Status=Status.Healthy,DayOfBirth=DateTime.Parse("2002-09-01")},
//            new Patient{Name="Arturo",Status=Status.Sick,DayOfBirth=DateTime.Parse("2003-09-01")},
//            new Patient{Name="Gytis",Status=Status.Sick,DayOfBirth=DateTime.Parse("2002-09-01")},
//            new Patient{Name="Yan",Status=Status.Sick,DayOfBirth=DateTime.Parse("2002-09-01")},
//            new Patient{Name="Peggy",Status=Status.Arrived,DayOfBirth=DateTime.Parse("2001-09-01")},
//            new Patient{Name="Laura",Status=Status.Sick,DayOfBirth=DateTime.Parse("2003-09-01")},
//            new Patient{Name="Nino",Status=Status.Healthy,DayOfBirth=DateTime.Parse("2005-09-01")}
//            };

//            patients.ForEach(s => context.Patients.Add(s));
//            context.SaveChanges();
//            var doctors = new List<Doctor>
//            {
//            new Doctor{Name="Alexeev"},
//            new Doctor{Name="Ivanova"},
//            new Doctor{Name="Mihko"},
//            new Doctor{Name="Amosova"},
//            new Doctor{Name="Alexeenko"}
//            };
//                doctors.ForEach(s => context.Doctors.Add(s));
//                context.SaveChanges();
//            }
        
//    }
//}