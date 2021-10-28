using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using AvansFysio.Attributes;

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
        public Byte[] Img { get; set; }

        [Required(ErrorMessage = "Vul je geboortedatum in")]
        [DataType(DataType.Date)]
        [MinimumAge(16, ErrorMessage = "Je moet minimaal 16 zijn")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Vul je geslacht")]
        public String Gender { get; set; }
    }
}
