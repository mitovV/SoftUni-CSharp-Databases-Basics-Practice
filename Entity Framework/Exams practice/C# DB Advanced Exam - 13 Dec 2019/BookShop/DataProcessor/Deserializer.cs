﻿namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportBookDto[]), new XmlRootAttribute("Books"));

            var dtos = (ImportBookDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var books = new List<Book>();

            foreach (var dto in dtos)
            {
                if (IsValid(dto))
                {
                    books.Add(new Book
                    {
                        Name = dto.Name,
                        Genre = (Genre)dto.Genre,
                        Price = dto.Price,
                        Pages = dto.Pages,
                        PublishedOn = DateTime.Parse(dto.PublishedOn, CultureInfo.InvariantCulture)
                    });

                    sb.AppendLine(string.Format(SuccessfullyImportedBook, dto.Name, dto.Price));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Books.AddRange(books);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<ImportAuthorDto[]>(jsonString);


            var sb = new StringBuilder();
            var authorBooks = new List<AuthorBook>();

            foreach (var dto in dtos)
            {
                if (IsValid(dto))
                {
                    var booksCount = 0;

                    var fullName = string.Empty;

                    foreach (var bookDto in dto.Books)
                    {
                        if (context.Books.Any(b => b.Id == bookDto.Id))
                        {
                            booksCount++;

                            var author = new Author
                            {
                                FirstName = dto.FirstName,
                                LastName = dto.LastName,
                                Phone = dto.Phone,
                                Email = dto.Email
                            };

                            context.Authors.Add(author);

                            fullName = author.FirstName + " " + author.LastName;

                            var authorBook = new AuthorBook
                            {
                                AuthorId = author.Id,
                                BookId = (int)bookDto.Id
                            };

                            authorBooks.Add(authorBook);
                        }
                    }

                    if (booksCount == 0)
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                    else
                    {
                        sb.AppendLine(string.Format(SuccessfullyImportedAuthor, fullName, booksCount));
                    }
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.AuthorsBooks.AddRange(authorBooks);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}