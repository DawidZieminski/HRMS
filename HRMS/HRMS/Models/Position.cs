namespace HRMS.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public int UserID { get; set; }
        public string PositionName { get; set; }
        public float Salary { get; set; }
        public float Bonus { get; set; }

        public virtual User User { get; set; }

    }
}