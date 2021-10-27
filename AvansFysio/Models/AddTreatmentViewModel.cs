using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class AddTreatmentViewModel
    {
        [Required(ErrorMessage = "Vul Het type in")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Vul het patientdossier in")]
        public int PatientFileId { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string Specifics { get; set; }
        public string PersonEmail { get; set; }
        [Required(ErrorMessage = "Vul de behandeldatum in")]
        public DateTime TreatmentDate { get; set; }
    }
}
