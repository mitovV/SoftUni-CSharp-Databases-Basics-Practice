public static string GetCarsWithTheirListOfParts(CarDealerContext context)
{
	var cars = context
		.Cars
		.Select(c => new
	{
		car = new
		{
			c.Make,
			c.Model,
			c.TravelledDistance
		},
		parts = c.PartCars.Select(p => new
		{
			p.Part.Name,
			Price = p.Part.Price.ToString("F2")
		})
		.ToArray()
	})
	.ToArray();

	return JsonConvert.SerializeObject(cars, Formatting.Indented);
}