using Recam.RealEstate.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recam.RealEstate.API.Models
{
    public class ListingCase
    {
        public int Id { get; set; }
        public string Address { get; set; } 
        public PropertyType PropertyType{ get; set; } // House/Townhouse/Apartment...
        public PropertyStatus PropertyStatus { get; set; } // For Sale/For Rent/Auction...
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garage { get; set; }
        public decimal Landsize { get; set; }
        public decimal Areasize { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreateById { get; set; }
        public User Creator { get; set; }
        public string? AssignedToId { get; set; }
        public User? Assignee { get; set; }
        public ICollection<MediaAsset> MediaAssets { get; set; } // A ListingCase can have many MediaAssets
        public ICollection<StatusHistory> StatusHistories { get; set; } //A ListingCase has many StatusHistory records

    }
}
