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
        public string Type { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string Specifics { get; set; }

        //public IPerson Therapist { get; set; }
        
        public DateTime TreatmentDate { get; set; }
    }
}
