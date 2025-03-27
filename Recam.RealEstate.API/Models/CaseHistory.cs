using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Recam.RealEstate.API.Models
{
    public class CaseHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ListingCaseId")]
        public int ListingCaseId { get; set; }  // FK reference to SQL

        [BsonElement("FieldName")]
        public string FieldName { get; set; }   // Price, Address, Status...

        [BsonElement("OldValue")]
        public string? OldValue { get; set; }

        [BsonElement("NewValue")]
        public string? NewValue { get; set; }

        [BsonElement("ChangedByUserId")]
        public string ChangedByUserId { get; set; }  // From SQL User

        [BsonElement("ChangedAt")]
        public DateTime ChangedAt { get; set; }
    }
}
