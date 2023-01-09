namespace Assignment2.Models
{
    public class UserAccessMap
    {
        public int UserAccessMapID { get; set; }
        public int UserID { get; set; }
        public int UserAccessID { get; set; }

        public User User { get; set; }
        public UserAccess UserAccess { get; set; }
    }
}
