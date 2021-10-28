using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class AddPatientFileViewModel
    {
        [Required(ErrorMessage = "Voer de patient in")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required(ErrorMessage = "Voer een beschrijving in")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Voer een Code in")]
        public int DiagnosticCode { get; set; }
        public Boolean IsStudent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [Required(ErrorMessage = "Voer de fysiotherapeut in")]
        public int PhysiotherapistId { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        [DataType(DataType.Date)]
        public DateTime DischargeDate { get; set; }
        [Required(ErrorMessage = "Vul de intakedatum in")]
        [DataType(DataType.Date)]
        public DateTime IntakeDate { get; set; }
        [Required(ErrorMessage = "Voer het aantal behandelingen per week in")]
        public int NumberOfTreatmentsPerWeek { get; set; }
        [Required(ErrorMessage = "Voer de duur van 1 sessie in")]
        public int DurationTreatment { get; set; }
    }
}
