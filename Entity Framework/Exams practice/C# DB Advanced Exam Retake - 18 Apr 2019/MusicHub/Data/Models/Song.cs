namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime CreatedOn  { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        [Required]
        public int WriterId { get; set; }

        public Writer Writer { get; set; }

        [Required]
        [Range(typeof(string), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public ICollection<SongPerformer> SongPerformers { get; set; } 
            = new HashSet<SongPerformer>();
    }
}
