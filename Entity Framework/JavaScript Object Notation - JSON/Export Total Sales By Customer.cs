public static string GetTotalSalesByCustomer(CarDealerContext context)
{
	var customers = context
		.Customers
		.Where(c => c.Sales.Any())
		.Select(c => new
		{
			fullName = c.Name,
			boughtCars = c.Sales.Count,
			spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
		})
		.OrderByDescending(c => c.spentMoney)
		.ThenByDescending(c => c.boughtCars)
		.ToArray();

	return JsonConvert.SerializeObject(customers, Formatting.Indented);
}