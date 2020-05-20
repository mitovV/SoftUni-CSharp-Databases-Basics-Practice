public static string ImportCustomers(CarDealerContext context, string inputJson)
{
	var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

	context.AddRange(customers);
	context.SaveChanges();

	return $"Successfully imported {customers.Length}.";
}