using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Treatment
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string Specifics { get; set; }
        public Student Student { get; set; }

        public Physiotherapist Physiotherapist { get; set; }
        [Required]

        public DateTime TreatmentDate { get; set; }
    }
}
