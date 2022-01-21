using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Presence
    {
        public int Id { get; set; }

        public TimeSpan StartMonday { get; set; }
        public TimeSpan EndMonday { get; set; }
        public TimeSpan StartTuesday { get; set; }
        public TimeSpan EndTuesday { get; set; }
        public TimeSpan StartWednesday { get; set; }
        public TimeSpan EndWednesday { get; set; }
        public TimeSpan StartThursday { get; set; }
        public TimeSpan EndThursday { get; set; }
        public TimeSpan StartFriday { get; set; }
        public TimeSpan EndFriday { get; set; }

        public Presence(int Id, TimeSpan StartMonday, TimeSpan EndMonday, TimeSpan StartTuesday, TimeSpan EndTuesday, TimeSpan StartWednesday, TimeSpan EndWednesday, TimeSpan StartThursday, TimeSpan EndThursday, TimeSpan StartFriday, TimeSpan EndFriday)
        {
            this.Id = Id;
            this.StartMonday = StartMonday;
            this.EndMonday = EndMonday;
            this.StartTuesday = StartTuesday;
            this.EndTuesday = EndTuesday;
            this.StartWednesday = StartWednesday;
            this.EndWednesday = EndWednesday;
            this.StartThursday = StartThursday;
            this.EndThursday = EndThursday;
            this.StartFriday = StartFriday;
            this.EndFriday = EndFriday;
        }


    }
}
