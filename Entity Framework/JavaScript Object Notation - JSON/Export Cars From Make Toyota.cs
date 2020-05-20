public static string GetCarsFromMakeToyota(CarDealerContext context)
{
	var cars = context
		.Cars
		.Where(c => c.Make == "Toyota")
		.Select(c => new 
		{
			c.Id,
			c.Make,
			c.Model,
			c.TravelledDistance
		})
		.OrderBy(c => c.Model)
		.ThenByDescending(c => c.TravelledDistance)
		.ToArray();

	return JsonConvert.SerializeObject(cars, Formatting.Indented);
}