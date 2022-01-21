using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Physiotherapist : IPerson
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int IdentificationNumber { get; set; }
        [Required]
        public int BIGNumber { get; set; }
        public Presence Presence { get; set; }

        public Physiotherapist(int Id, string Name, string Email, string PhoneNumber, int IdentificationNumber, int BIGNumber)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.IdentificationNumber = IdentificationNumber;
            this.BIGNumber = BIGNumber;
        }
    }
}
