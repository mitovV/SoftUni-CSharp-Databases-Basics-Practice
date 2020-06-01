namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Data;
    using Data.Models;
    using ImportDtos;

    using Microsoft.EntityFrameworkCore.Internal;
    using MusicHub.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writerDtos = JsonConvert.DeserializeObject<ImportWriterDto[]>(jsonString);

            var sb = new StringBuilder();

            var writers = new List<Writer>();

            foreach (var writerDto in writerDtos)
            {
                if (IsValid(writerDto))
                {
                    var writer = new Writer
                    {
                        Name = writerDto.Name,
                        Pseudonym = writerDto.Pseudonym
                    };

                    writers.Add(writer);
                    sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerDtos = JsonConvert.DeserializeObject<ImportProducerAndAlbumDto[]>(jsonString);

            var sb = new StringBuilder();

            foreach (var producerDto in producerDtos)
            {
                if (IsValid(producerDto))
                {

                    var producer = new Producer
                    {
                        Name = producerDto.Name,
                        Pseudonym = producerDto.Pseudonym,
                        PhoneNumber = producerDto.PhoneNumber
                    };

                    var albums = new List<Album>();

                    var hasInvalidAlbum = false;

                    foreach (var albumDto in producerDto.Albums)
                    {
                        if (hasInvalidAlbum)
                        {
                            break;
                        }

                        if (IsValid(albumDto))
                        {
                            DateTime releaseDate;

                            if (DateTime.TryParseExact(albumDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate))
                            {
                                albums.Add(new Album
                                {
                                    Name = albumDto.Name,
                                    ReleaseDate = releaseDate
                                });
                            }
                            else
                            {
                                sb.AppendLine(ErrorMessage);
                                hasInvalidAlbum = true;
                            }
                        }
                        else
                        {
                            sb.AppendLine(ErrorMessage);
                            hasInvalidAlbum = true;
                        }
                    }

                    if (!hasInvalidAlbum)
                    {
                        context.Producers.Add(producer);
                        context.SaveChanges();

                        producer.Albums = albums;
                        context.Albums.AddRange(albums);
                        context.SaveChanges();

                        if (producer.PhoneNumber != null)
                        {
                            sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, albums.Count));
                        }
                        else
                        {
                            sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, albums.Count));
                        }
                    }
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            return sb.ToString().Trim();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportSongDto[]), new XmlRootAttribute("Songs"));

            var sb = new StringBuilder();

            var songDtos = (ImportSongDto[])serializer.Deserialize(new StringReader(xmlString));

            var songs = new List<Song>();

            foreach (var songDto in songDtos)
            {
                TimeSpan duration;
                DateTime createdOn;
                Genre genre;

                if (IsValid(songDto)
                    && context.Albums.Any(a => a.Id == songDto.AlbumId)
                    && context.Writers.Any(w => w.Id == songDto.WriterId)
                    && TimeSpan.TryParse(songDto.Duration, out duration)
                    && DateTime.TryParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out createdOn)
                    && Enum.TryParse(songDto.Genre, out genre))
                {
                    var song = new Song
                    {
                        Name = songDto.Name,
                        Duration = TimeSpan.ParseExact(songDto.Duration, "c", CultureInfo.InvariantCulture),
                        CreatedOn = DateTime.ParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Genre = Enum.Parse<Genre>(songDto.Genre),
                        AlbumId = songDto.AlbumId,
                        WriterId = songDto.WriterId,
                        Price = songDto.Price
                    };

                    songs.Add(song);
                    sb.AppendLine(string.Format(SuccessfullyImportedSong, song.Name, song.Genre, song.Duration));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Songs.AddRange(songs);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportSongPerformerDto[]), new XmlRootAttribute("Performers"));

            var sb = new StringBuilder();

            var performerDtos = (ImportSongPerformerDto[])serializer.Deserialize(new StringReader(xmlString));

            var songs = new List<Song>();
            var ps = new List<SongPerformer>();

            foreach (var dto in performerDtos)
            {
                if (IsValid(dto))
                {
                    var isInvalid = false;
                    var songIds = new List<int>();

                    var performer = new Performer
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        NetWorth = dto.NetWorth
                    };

                    foreach (var songDto in dto.PerformersSongs)
                    {
                        if (context.Songs.Any(s => s.Id == songDto.Id))
                        {
                            songIds.Add(songDto.Id);
                        }
                        else
                        {
                            sb.AppendLine(ErrorMessage);
                            isInvalid = true;
                            break;
                        }
                    }

                    if (!isInvalid)
                    {
                        context.Performers.Add(performer);
                        context.SaveChanges();

                        foreach (var songId in songIds)
                        {
                            var sp = new SongPerformer
                            {
                                SongId = songId,
                                PerformerId = performer.Id
                            };

                            ps.Add(sp);
                        }

                        sb.AppendLine(string.Format(SuccessfullyImportedPerformer, performer.FirstName, songIds.Count));
                    }
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.SongsPerformers.AddRange(ps);
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