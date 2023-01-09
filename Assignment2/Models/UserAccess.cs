namespace Assignment2.Models
{
    public class UserAccess
    {
        public int UserAccessID { get; set; }
        public string UserAccessName { get; set; }
        public string Description { get; set; }

        public ICollection<UserAccessMap> UserAccessMaps { get; set; }
    }
}
