namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Data;
    using Data.Models.Enums;
    using DataProcessor.ExportDto;

    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new ExportAuthorDto
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    Books = a.AuthorsBooks
                    .Select(ab => ab.Book)
                    .OrderByDescending(b => b.Price)
                    .Select(b => new ExportBooksJsonDto
                    {
                        BookName = b.Name,
                        BookPrice = b.Price.ToString("F2")
                    })
                    .ToArray()
                })
                .ToArray()
                .OrderByDescending(a => a.Books.Count())
                .ThenBy(a => a.AuthorName);

            return JsonConvert.SerializeObject(authors, Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context
                .Books
                .Where(b => b.PublishedOn.Ticks < date.Ticks && b.Genre.ToString() == "Science")
                .Select(b => new ExportOldestBookDto
                {
                    Name = b.Name,
                    Pages = b.Pages,
                    Date = b.PublishedOn.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                })
                .OrderByDescending(b => b.Pages)
                .ThenBy(b => b.Date)
                .Take(10)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportOldestBookDto[]), new XmlRootAttribute("Books"));

            var namespaces = new XmlSerializerNamespaces();

            var sb = new StringBuilder();
            namespaces.Add("", "");

            serializer.Serialize(new StringWriter(sb), books, namespaces);

            return sb.ToString().Trim();
        }
    }
}