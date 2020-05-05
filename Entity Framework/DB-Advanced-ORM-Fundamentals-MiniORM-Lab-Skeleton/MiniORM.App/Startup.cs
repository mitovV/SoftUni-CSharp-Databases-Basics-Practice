using MiniORM.App.Data;
using MiniORM.App.Data.Entities;
using System.Linq;

namespace MiniORM.App
{
    public class Startup
    {
        public static void Main()
        {
            var connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MiniORM;Integrated Security=true;";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirsName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();
            employee.FirsName = "Modified";

            context.SaveChanges();
        }
    }
}
