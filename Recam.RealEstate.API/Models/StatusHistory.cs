using Recam.RealEstate.API.Enums;

namespace Recam.RealEstate.API.Models
{
    public class StatusHistory
    {
        public int Id { get; set; }
        public int ListingCaseId { get; set; } 
        public ListingCase ListingCase { get; set; }
        public PropertyStatus OldStatus { get; set; }
        public PropertyStatus NewStatus { get; set; }
        public string ChangedById { get; set; }
        public User ChangedBy { get; set; } 
        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}
