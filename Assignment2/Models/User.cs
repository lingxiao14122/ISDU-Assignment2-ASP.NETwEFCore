namespace Assignment2.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int UserName { get; set; }
        public string UserEmail { get; set; }
        public string EmployeeNumber { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        public string Active { get; set; }
        public DateTime DateEntered { get; set; }

        public ICollection<UserAccessMap> UserAccessMaps { get; set; }
    }
}
