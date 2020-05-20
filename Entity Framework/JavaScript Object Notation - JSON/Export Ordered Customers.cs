public static string GetOrderedCustomers(CarDealerContext context)
{
	var customers = context
		.Customers
		.OrderBy(c => c.BirthDate)
		.ThenBy(c => c.IsYoungDriver)
		.Select(c => new
		{
			c.Name,
			BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
			c.IsYoungDriver
		})
		.ToArray();

	return JsonConvert.SerializeObject(customers, Formatting.Indented);
}