using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Attributes
{
    public class MinimumAge : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAge(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }

            return false;
        }
    }
}
