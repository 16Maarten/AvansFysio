using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public static class PatientFileHelper
    {
        public static List<PatientFileViewModel> ToViewModel(this IEnumerable<PatientFile> patientFiles)
        {
            var patientFileViewModel = new List<PatientFileViewModel>();
            foreach (PatientFile patientFile in patientFiles)
            {
                patientFileViewModel.Add(patientFile.ToViewModel());
            }
            return patientFileViewModel;

        }
        public static PatientFileViewModel ToViewModel(this PatientFile patientFile)
        {
            return new PatientFileViewModel
            {
                Id = patientFile.Id,
                Patient = patientFile.Patient,
                Age = patientFile.Age,
                Description = patientFile.Description,
                DiagnosticCode = patientFile.DiagnosticCode,
                DescriptionDiagnosticCode = patientFile.DescriptionDiagnosticCode,
                IsStudent = patientFile.IsStudent,
                Physiotherapist = patientFile.Physiotherapist,
                Student = patientFile.Student,
                IntakeDate = patientFile.IntakeDate,
                DischargeDate = patientFile.DischargeDate,
                TreatmentPlan = patientFile.TreatmentPlan,
                Remarks = patientFile.Remarks,
                Treatments = patientFile.Treatments
            };
        }
    }
}
