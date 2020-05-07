public static string GetEmployee147(SoftUniContext context)
{
	var sb = new StringBuilder();

	var employee = context
		.Employees
		.Where(e => e.EmployeeId == 147)
		.Select(e => new
		{
			e.FirstName,
			e.LastName,
			e.JobTitle,
			Projects = e.EmployeesProjects
						.Select(p => p.Project.Name)
                        .OrderBy(p => p)
                        .ToArray()
        })
        .First();

		sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
		sb.AppendLine(string.Join(Environment.NewLine, employee.Projects));

	return sb.ToString().TrimEnd();
}