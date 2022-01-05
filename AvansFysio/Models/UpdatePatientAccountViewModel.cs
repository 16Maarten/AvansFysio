using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class UpdatePatientAccountViewModel
    {
        [Required(ErrorMessage = "Vul een telefoonnummer in")]
        [Phone]
        public string PhoneNumber { get; set; }
        public int PatientId { get; set; }
    }
}
