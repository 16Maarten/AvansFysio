using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Remark
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime RemarkDate { get; set; }
        public Student Student { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        [Required]
        public Boolean Visible { get; set; }
    }
}
