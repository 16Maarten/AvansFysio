using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace AvansFysio.Models
{
    public class AddPatientViewModel
    {
        [Required(ErrorMessage = "Vul je Naam in")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul je emailadres in")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vul je telefoonnummer in")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vul je studentNummer/personeelnummer in")]
        public int IdentificationNumber { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public byte[] Img { get; set; }
        [Required(ErrorMessage = "Vul je geboortedatum in")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Vul je geslacht")]
        public String Gender { get; set; }

    }
}
