using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class UpdatePresenceViewModel
    {
        [DataType(DataType.Time)]
        public TimeSpan StartMonday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndMonday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTuesday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTuesday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartWednesday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndWednesday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartThursday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndThursday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartFriday { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndFriday { get; set; }
    }
}
