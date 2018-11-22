using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public int ObiectID { get; set; }
        public string NameProject { get; set; }
        public DateTime DataProjectStart  { get; set; }
        public DateTime DataProjectEnd { get; set; }
        public string Description { get; set; }

        public virtual Obiect Obiect { get; set; }
    }
}