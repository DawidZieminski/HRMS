using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Presence
    {
        public int PresenceID { get; set; }
        public int UserID { get; set; }
        public int ObiectID { get; set; }
        public TypePresence TypePresence { get; set; }
        public Nullable<System.DateTime> DatePresenceStart { get; set; }
        public Nullable<System.DateTime> DatePresenceEnd { get; set; }
        public Nullable<System.DateTime> TimePresenceStart { get; set; }
        public Nullable<System.DateTime> TimePresenceEnd { get; set; }
        public virtual User User { get; set; }
        public virtual Obiect Obiect { get; set; }
    }
    public enum TypePresence
    {
        Roboczy,
        Urlop,
        Chorobowe
    }
}