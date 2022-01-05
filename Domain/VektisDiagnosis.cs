using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VektisDiagnosis
    {
        [Key]
        public int Code { get; set; }
        public string BodyLocation { get; set; }
        public string Pathology { get; set; }
    }
}
