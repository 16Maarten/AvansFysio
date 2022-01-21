using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int SessionLength { get; set; }
        [Required]
        public Patient Patient { get; set; }

        public Physiotherapist Physiotherapist { get; set; }
        public Student Student { get; set; }
    }
}
