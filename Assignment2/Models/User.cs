using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Photo { get; set; } = string.Empty;
        [ValidateNever]
        [NotMapped]
        [Display(Name = "Upload Photo")]
        public IFormFile FormFile { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Display(Name = "Emp No.")]
        [RegularExpression(@"^P\.T+[0-9]*$", ErrorMessage = "Must start with P.T and follow by numbers")]
        public string EmployeeNumber { get; set; }
        [Range(18, 900, ErrorMessage = "Age must be 18 or more")]
        public int Age { get; set; }
        [MinLength(8, ErrorMessage = "Minimum length 8")]
        public string Password { get; set; }
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public string Active { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateEntered { get; set; }

        public ICollection<UserAccessMap> UserAccessMaps { get; set; } = new List<UserAccessMap>();
    }
}
