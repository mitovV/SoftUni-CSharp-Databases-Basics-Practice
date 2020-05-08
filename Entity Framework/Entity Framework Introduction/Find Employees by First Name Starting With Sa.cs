public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
{
	var sb = new StringBuilder();

	var employees = context
		.Employees
		.Where(e => e.FirstName.StartsWith("Sa"))
		.Select(e => new
		{
			e.FirstName,
			e.LastName,
			e.JobTitle,
			e.Salary
		})
		.OrderBy(e => e.FirstName)
		.ThenBy(e => e.LastName)
		.ToArray();

	foreach (var employee in employees)
	{
		sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
	}

	return sb.ToString().TrimEnd();
}