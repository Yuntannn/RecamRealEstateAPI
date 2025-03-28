using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.DTOs
{
    public class ListingCaseRequestDto
    {
        public string? Address { get; set; }
        public string CreateById { get; set; }
        public string AssignedToId { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garage { get; set; }
        public decimal Landsize { get; set; }
        public decimal Areasize { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
