public static string DeleteProjectById(SoftUniContext context)
{
	var sb = new StringBuilder();

	var project = context
		.Projects
		.Find(2);

	var eP = context
		.EmployeesProjects
		.Where(p => p.ProjectId == project.ProjectId);

	foreach (var item in eP)
	{
		context.EmployeesProjects.Remove(item);
	}

		context.Projects.Remove(project);
		context.SaveChanges();

	var projects = context
		.Projects
		.Take(10)
		.Select(e => e.Name)
		.ToArray();

	foreach (var projectName in projects)
	{
		sb.AppendLine(projectName);
	}

	return sb.ToString().TrimEnd();
}