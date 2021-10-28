using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class AddRemarkViewModel
    {
        public int PatientFileId { get; set; }
        [Required(ErrorMessage = "Vul de beschrijving in")]
        public string Description { get; set; }
        public Boolean Visibility { get; set; }
    }
}
