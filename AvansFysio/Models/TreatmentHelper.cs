using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
        public static class TreatmentHelper
        {
            public static List<TreatmentViewModel> ToViewModel(this IEnumerable<Treatment> treatments)
            {
                var treatmentViewModel = new List<TreatmentViewModel>();
                foreach (Treatment treatment in treatments)
                {
                    treatmentViewModel.Add(treatment.ToViewModel());
                }
                return treatmentViewModel;

            }
            public static TreatmentViewModel ToViewModel(this Treatment treatment)
            {
                return new TreatmentViewModel
                {
                    Id = treatment.Id,
                    Type = treatment.Type,
                    Description = treatment.Description,
                    Room = treatment.Room,
                    Student = treatment.Student,
                    Physiotherapist = treatment.Physiotherapist,
                    Specifics = treatment.Specifics,
                    TreatmentDate = treatment.TreatmentDate,
                    CreationDate = treatment.CreationDate
                };
            }
        }
    }
