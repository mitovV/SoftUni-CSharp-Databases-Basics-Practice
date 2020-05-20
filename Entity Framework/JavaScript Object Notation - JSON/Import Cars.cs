public static string ImportCars(CarDealerContext context, string inputJson)
{
	var cars = JsonConvert.DeserializeObject<ImortCarDto[]>(inputJson);

	foreach (var car in cars)
	{
		var newCar = new Car() 
		{ 
			Make = car.Make,
			Model = car.Model,
			TravelledDistance = car.TravelledDistance
		};

		context.Add(newCar);

		foreach (var partId in car.PartsId)
		{
			var partCar = new PartCar()
			{
				CarId = newCar.Id,
				PartId = partId
			};

			if (!newCar.PartCars.Any(pc => pc.PartId == partId))
			{
				context.PartCars.Add(partCar);
			}
		}
	}

	context.SaveChanges();

	return $"Successfully imported {cars.Length}.";
}