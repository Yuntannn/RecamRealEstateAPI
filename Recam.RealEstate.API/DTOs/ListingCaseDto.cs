using Recam.RealEstate.API.Enums;

namespace Recam.RealEstate.API.DTOs
{
    public class ListingCaseDto
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garage { get; set; }
        public decimal Landsize { get; set; }
        public decimal Areasize { get; set; }
        public decimal Price { get; set; }
    }
}
