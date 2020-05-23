namespace ProductShop.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class ImportCategorieDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
