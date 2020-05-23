namespace ProductShop
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    using Data;
    using Dtos.Import;

    using AutoMapper;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                var data = File.ReadAllText("../../../Datasets/users.xml");

                var result = ImportUsers(db, data);

                Console.WriteLine(result);
            }

        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportUserDto[]),new XmlRootAttribute("Users"));

            ImportUserDto[] userDtos;

            using (var reader = new StringReader(inputXml))
            {
              userDtos = (ImportUserDto[])serializer.Deserialize(reader);
            }

            var users = Mapper.Map<User[]>(userDtos);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }
    }
}