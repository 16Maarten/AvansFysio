using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Remark
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime ReamrkDate { get; }
        //public IPerson Person { get; set; }

        public Boolean Visible { get; set; }
    }
}
