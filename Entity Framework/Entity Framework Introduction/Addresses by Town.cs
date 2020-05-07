public static string GetAddressesByTown(SoftUniContext context)
{
	var sb = new StringBuilder();

	var addresses = context
		.Addresses
		.Select(a => new
		{
			a.AddressText,
			TownName = a.Town.Name,
			EmployeeCount = a.Employees.Count
		})
		.OrderByDescending(a => a.EmployeeCount)
		.ThenBy(a => a.TownName)
		.ThenBy(a => a.AddressText)
		.Take(10)
		.ToArray();

	foreach (var addres in addresses)
	{
		sb.AppendLine($"{addres.AddressText}, {addres.TownName} - {addres.EmployeeCount} employees");
	}

	return sb.ToString().TrimEnd();
}