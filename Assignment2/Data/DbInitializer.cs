using Assignment2.Models;

namespace Assignment2.Data
{
    public class DbInitializer
    {
        public static void Initialize(AssignmentContext context)
        {
            if (context.Departments.Any())
            {
                return; // database has data already
            }

            var departments = new Department[]
            {
                new Department{Name="Finance Department",Description="The Finance Department"},
                new Department{Name="IT Department",Description="The IT Department"}
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var users = new User[]
            {
                new User{Photo="person1.jpg", UserName="Jasmine",UserEmail="jasmine@gmail.com",EmployeeNumber="E002", Age=22, Password="^�H��(\u0004qQ��o��)'s`=\rj���*\u0011�r\u001d\u0015B�", DepartmentID=2, Active="Yes"},
                new User{Photo="person2.jpg", UserName="Mark",UserEmail="Mark@gmail.com",EmployeeNumber="E002", Age=22, Password="^�H��(\u0004qQ��o��)'s`=\rj���*\u0011�r\u001d\u0015B�", DepartmentID=2, Active="Yes"},
                new User{Photo="person3.jfif", UserName="Jason",UserEmail="jason@gmail.com",EmployeeNumber="E003", Age=22, Password="^�H��(\u0004qQ��o��)'s`=\rj���*\u0011�r\u001d\u0015B�", DepartmentID=1, Active="Yes"},
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var userAccess = new UserAccess[]
            {
                new UserAccess{UserAccessName="Normal Access",Description="Normal Access"},
                new UserAccess{UserAccessName="Business Access",Description="Business Access"},
            };

            context.UserAccess.AddRange(userAccess);
            context.SaveChanges();
        }
    }
}
