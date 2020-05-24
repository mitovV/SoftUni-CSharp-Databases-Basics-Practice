namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TravelledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public ImportPartCarDto[] Parts { get; set; }
    }
}
