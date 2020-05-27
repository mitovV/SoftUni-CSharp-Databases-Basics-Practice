namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AuthorBook
    {
        [Required]
        [Key, Column(Order = 0)]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Key, Column(Order = 1)]
        public int BookId { get; set; }

        public Book Book { get; set; }

    }
}
