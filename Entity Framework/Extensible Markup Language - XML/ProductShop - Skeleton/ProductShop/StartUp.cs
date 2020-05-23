namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                var data = File.ReadAllText("../../../Datasets/categories-products.xml");

                var result = GetProductsInRange(db);

                Console.WriteLine(result);
            }

        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

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

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            ImportProductDto[] productDtos;

            using (var reader = new StringReader(inputXml))
            {
                productDtos = (ImportProductDto[])serializer.Deserialize(reader);
            }

            var products = Mapper.Map<Product[]>(productDtos);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportCategorieDto[]), new XmlRootAttribute("Categories"));

            ImportCategorieDto[] importCategorieDtos;

            using (var reader = new StringReader(inputXml))
            {
                importCategorieDtos = (ImportCategorieDto[])serializer.Deserialize(reader);
            }

            var categories = Mapper.Map<Category[]>(importCategorieDtos);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            ImportCategoryProductDto[] importCategoryProductDtos;

            using (var reader = new StringReader(inputXml))
            {
                importCategoryProductDtos = (ImportCategoryProductDto[])serializer.Deserialize(reader);
            }

            var categoryProducts = Mapper.Map<CategoryProduct[]>(importCategoryProductDtos);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsDto = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .Select(p => new ExportProductDto() 
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("Products"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, productsDto, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}