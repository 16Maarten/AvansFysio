using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TreatmentPlan
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfTreatmentsPerWeek { get; set; }
        [Required]
        public int DurationTreatment { get; set; }
        [Required]
        public int DiagnosticCode { get; set; }
        [Required]
        public string DescriptionDiagnosticCode { get; set; }
    }
}
