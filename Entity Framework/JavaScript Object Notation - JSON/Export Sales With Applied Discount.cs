public static string GetSalesWithAppliedDiscount(CarDealerContext context)
{
	var sales = context
		.Sales
		.Take(10)
		.Select(s => new
		{
			car = new
			{
				Make = s.Car.Make,
				Model = s.Car.Model,
				TravelledDistance = s.Car.TravelledDistance
			},
			customerName = s.Customer.Name,
			Discount = $"{s.Discount:F2}",
			price = $"{s.Car.PartCars.Sum(p => p.Part.Price):F2}",
			priceWithDiscount = $@"{(s.Car.PartCars.Sum(p => p.Part.Price) - s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100):F2}"
		})
		.ToList();

	return JsonConvert.SerializeObject(sales, Formatting.Indented);
}