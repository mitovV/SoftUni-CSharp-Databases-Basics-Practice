public static string GetLatestProjects(SoftUniContext context)
{
	var sb = new StringBuilder();

	var projects = context
		.Projects
		.OrderByDescending(p => p.StartDate)
		.Take(10)
		.OrderBy(p => p.Name)
		.Select(p => new
		{
			p.Name,
			p.Description,
			p.StartDate
		})
		.ToArray();

	foreach (var project in projects)
	{
		sb.AppendLine($"{project.Name}");
		sb.AppendLine($"{project.Description}");
		sb.AppendLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
	}

	return sb.ToString().TrimEnd();
}