using AvansFysio.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class UpdatePatientViewModel
    {
        [Required(ErrorMessage = "Vul je Naam in")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul je telefoonnumeer in")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vul je geboortedatum in")]
        [DataType(DataType.Date)]
        [MinimumAge(16, ErrorMessage = "Je moet minimaal 16 zijn")]
        public DateTime Birthday { get; set; }
        public int PatientId { get; set; }
    }
}
