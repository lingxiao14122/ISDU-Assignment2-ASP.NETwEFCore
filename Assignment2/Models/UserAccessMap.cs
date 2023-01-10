using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class UserAccessMap
    {
        public int UserAccessMapID { get; set; }
        public int UserID { get; set; }
        public int UserAccessID { get; set; }
        [Display(Name = "User Name")]
        public User? User { get; set; }
        [Display(Name = "Access Name")]
        public UserAccess? UserAccess { get; set; }
    }
}
