using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRL.Infrastructure.Data.Models
{
    [Table("person")]
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public ObjectId? Id { get; set; }

        [BsonElement("documentNumber")]
        public string? DocumentNumberPerson { get; set; }

        [BsonElement("name")]
        public string? NamePerson { get; set; }

        [BsonElement("lastName")]
        public string? LastNamePerson { set; get; }

        [BsonElement("cellPhone")]
        public string? CellPhonePerson { set; get; }

        [BsonElement("active")]
        public bool Active { set; get; }
    }
}