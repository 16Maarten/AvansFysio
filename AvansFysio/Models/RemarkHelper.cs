using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public static class RemarktHelper
    {
        public static List<RemarkViewModel> ToViewModel(this IEnumerable<Remark> remarks)
        {
            var remarkViewModel = new List<RemarkViewModel>();
            foreach (Remark remark in remarks)
            {
                remarkViewModel.Add(remark.ToViewModel());
            }
            return remarkViewModel;

        }
        public static RemarkViewModel ToViewModel(this Remark remark)
        {
            return new RemarkViewModel
            {
                Id = remark.Id,
                RemarkDate = remark.RemarkDate,
                Description = remark.Description,
                Student = remark.Student,
                Physiotherapist = remark.Physiotherapist,
                Visible = remark.Visible,
            };
        }
    }
}
