using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Patient : IPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int IdentificationNumber { get; set; }
        [Key]
        public int PatientNumber { get; set; }
        public byte[] Img { get; set; }
        public DateTime Birthday { get; set; }
        public String Gender { get; set; }

    }
}
