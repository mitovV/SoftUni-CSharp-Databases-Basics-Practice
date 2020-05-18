public static string ImportUsers(ProductShopContext context, string inputJson)
{
	var users = JsonConvert.DeserializeObject<User[]>(inputJson);

	context.AddRange(users);
	context.SaveChanges();

	return $"Successfully imported {users.Length}";
}