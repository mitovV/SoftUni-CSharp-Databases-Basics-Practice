 public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context
                 .Employees
                 .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                 .Select(e => new
                 {
                     FirstName = e.FirstName,
                     LastName = e.LastName,
                     ManagerFirstName = e.Manager.FirstName,
                     ManagerLastName = e.Manager.LastName,
                     Projects = e.EmployeesProjects.Select(p => new
                     {
                         p.Project.Name,
                         p.Project.StartDate,
                         p.Project.EndDate
                     })
                     .ToArray()

                 })
                 .Take(10)
                 .ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate
                        .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    var endDate = project.EndDate == null ?
                        "not finished" :
                        project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    sb.AppendLine($"--{project.Name} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }