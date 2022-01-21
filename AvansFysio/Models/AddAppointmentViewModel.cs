using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class AddAppointmentViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PatientId { get; set; }
    }
}
