using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Recam.RealEstate.API.Models
{
    public class UserActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }  // FK to SQL User (Identity)

        [BsonElement("Action")]
        public string Action { get; set; }  // "CreateListing", "UploadPhoto", "Login"

        [BsonElement("Resource")]
        public string? Resource { get; set; }  // ListingCase:1, Media:2 ...

        [BsonElement("Description")]
        public string? Description { get; set; }  

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
