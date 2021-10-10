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
        public Patient Patient { get; set; }
        public int Age { get; set; }
        public string Discription { get; set; }
        public Boolean Student { get; set; }
        //public IPerson IntakePerson { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        public DateTime IntakeDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public ICollection<Remark> Remarks { get; set; }
        public String Treatment { get; set; }
        public int NumberOfTreatments { get; set; }
    }
}
