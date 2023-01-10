﻿using Assignment2.Models;

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
                new User{UserName="Jason",UserEmail="jason@gmail.com",EmployeeNumber="E001", Age=22, Password="j���*\u0011�r\u001d\u0015B�", DepartmentID=1, Active="Yes"},
                new User{UserName="Jasmine",UserEmail="jasmine@gmail.com",EmployeeNumber="E002", Age=22, Password="j���*\u0011�r\u001d\u0015B�", DepartmentID=2, Active="Yes"}
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var userAccess = new UserAccess[]
            {
                new UserAccess{UserAccessName="View User Module",Description="User can read only"},
                new UserAccess{UserAccessName="Edit Access",Description="User can create and update"},
            };

            context.UserAccess.AddRange(userAccess);
            context.SaveChanges();
        }
    }
}
