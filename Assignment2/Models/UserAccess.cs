using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class UserAccess
    {
        public int UserAccessID { get; set; }
        [Display(Name = "Name")]
        public string UserAccessName { get; set; }
        public string Description { get; set; }

        public ICollection<UserAccessMap> UserAccessMaps { get; set; } = new List<UserAccessMap>();
    }
}
