using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.DTOs
{
    public class MediaRequestDto
    {
        public MediaType MediaType { get; set; } // Photo/Video/Floorplan
        public string FileUrl { get; set; }
        public bool IsHero { get; set; }
        public bool DisplayInGallery { get; set; }
        public string UploadById { get; set; }
        public DateTime UploadAt { get; set; } = DateTime.Now;
    }
}
