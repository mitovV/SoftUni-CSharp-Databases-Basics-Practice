public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
{
	var sb = new StringBuilder();

	var departments = context
		.Departments
		.Where(d => d.Employees.Count > 5)
		.OrderBy(d => d.Employees.Count)
		.ThenBy(d => d.Name)
		.Select(d => new
		{
            d.Name,
            ManagerFullName = d.Manager.FirstName + " " + d.Manager.LastName,
            Employees = d.Employees.Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle
            })
			.OrderBy(e => e.FirstName)
			.ThenBy(e => e.LastName)
            .ToArray()
         })
        .ToArray();

	foreach (var department in departments)
	{
		sb.AppendLine($"{department.Name} - {department.ManagerFullName}");

		foreach (var employee in department.Employees)
		{
			var fullName = employee.FirstName + " " + employee.LastName;
			sb.AppendLine($"{fullName} - {employee.JobTitle}");
		}
	}

	return sb.ToString().TrimEnd();
}