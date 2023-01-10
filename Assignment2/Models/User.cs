using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string UserEmail { get; set; }
        [Display(Name = "Emp No.")]
        public string EmployeeNumber { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public string Active { get; set; }
        [Timestamp]
        public string? DateEntered { get; set; }

        public ICollection<UserAccessMap> UserAccessMaps { get; set; } = new List<UserAccessMap>();
    }
}
