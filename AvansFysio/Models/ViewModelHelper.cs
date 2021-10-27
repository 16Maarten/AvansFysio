using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public static class ViewModelHelper
    {
        public static Patient ToDomain(this AddPatientViewModel patient)
        {
            return new Patient
            {
                Name = patient.Name,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                IdentificationNumber = patient.IdentificationNumber,
                Birthday = patient.Birthday,
                Gender = patient.Gender,
                Student = patient.Student
            };
        }

        public static List<PatientsViewModel> ToViewModel(this IEnumerable<Patient> patients)
        {
            var  patientsViewModel = new List<PatientsViewModel>();
            foreach (Patient patient in patients)
            {
                patientsViewModel.Add(patient.ToViewModel());
            }
            return patientsViewModel;

        }
        public static PatientsViewModel ToViewModel(this Patient patient)
        {
            return new PatientsViewModel
            {
                Name = patient.Name,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Img = patient.Img,
                Birthday = patient.Birthday,
                Gender = patient.Gender,
                Student = patient.Student
            };
        }
    }
}
