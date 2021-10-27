using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class PatientFile
    {
        public int Id { get; set; }
        [Required]
        public Patient Patient { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int DiagnosticCode { get; set; }
        [Required]
        public string DescriptionDiagnosticCode { get; set; }
        [Required]
        public Boolean IsStudent { get; set; }
        public Student Student { get; set; }
        [Required]
        public Physiotherapist Physiotherapist { get; set; }
        [Required]
        public DateTime IntakeDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public ICollection<Remark> Remarks { get; set; }
        [Required]
        public TreatmentPlan TreatmentPlan { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
