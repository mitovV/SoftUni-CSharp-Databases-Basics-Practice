public static string IncreaseSalaries(SoftUniContext context)
{
	var sb = new StringBuilder();

	var employees = context
		.Employees
		.Where(e => e.Department.Name == "Engineering"
		|| e.Department.Name == "Tool Design"
		|| e.Department.Name == "Marketing"
		|| e.Department.Name == "Information Services")
		.OrderBy(e => e.FirstName)
		.ThenBy(e => e.LastName);

	foreach (var employee in employees)
	{
		employee.Salary *= 1.12m;
	}

		context.SaveChanges();

	foreach (var employee in employees)
	{
		sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
	}

	return sb.ToString().TrimEnd();
}