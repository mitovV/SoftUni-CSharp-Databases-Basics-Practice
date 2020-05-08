public static string RemoveTown(SoftUniContext context)
{
	var town = context
		.Towns
		.First(t => t.Name == "Seattle");

	var addresses = context
		.Addresses
		.Where(a => a.TownId == town.TownId)
		.ToArray();

	var employees = context
		.Employees
		.Where(e => addresses.Any(a => a.AddressId == e.AddressId));

	foreach (var employee in employees)
	{
		employee.AddressId = null;
	}

	foreach (var addres in addresses)
	{
		context.Addresses.Remove(addres);
	}

	context.Towns.Remove(town);

	context.SaveChanges();

	return $"{addresses.Count()} addresses in Seattle were deleted";
}