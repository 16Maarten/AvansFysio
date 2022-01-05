using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VektisTreatment
    {
        [Key]
        public string Code { get; set; }
        public string Description { get; set; }
        public Boolean RemarkRequired { get; set; }
    }
}
