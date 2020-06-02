namespace MusicHub.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Data;
    using ExportDtos;

    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var sb = new StringBuilder();

            var albums = context
                .Albums
                .Where(a => a.ProducerId == producerId)
                .Select(a => new ExportAlbumDto
                {
                    AlbumName = a.Name,
                    ProducerName = a.Producer.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Songs = a.Songs.Select(s => new ExportSongDto
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("F2"),
                        Writer = $"{s.Writer.Name}"
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.Writer)
                    .ToArray(),
                    AlbumPrice = a.Songs.Sum(s => s.Price).ToString("F2")
                })
                .OrderByDescending(a => a.AlbumPrice)
                .ToArray();

            return JsonConvert.SerializeObject(albums, Formatting.Indented);
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context
                .Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new ExportSongAboveDurationDto
                {
                    Name = s.Name,
                    Writer = s.Writer.Name,
                    Performer = s.SongPerformers.Select(sP => $"{sP.Performer.FirstName} {sP.Performer.LastName}").FirstOrDefault(),
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportSongAboveDurationDto[]), new XmlRootAttribute("Songs"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            serializer.Serialize(new StringWriter(sb), songs, namespaces);

            return sb.ToString().Trim();
        }
    }
}