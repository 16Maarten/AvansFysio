using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Patient : IPerson
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int IdentificationNumber { get; set; }
        [Key]
        public int PatientNumber { get; set; }

        public byte[] Img { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public Boolean Student { get; set; }

    }
}
