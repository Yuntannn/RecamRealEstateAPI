using Recam.RealEstate.API.Enums;

namespace Recam.RealEstate.API.Models
{
    public class MediaAsset
    {
        public int Id { get; set; }
        public int ListingCaseId { get; set; }
        public ListingCase ListingCase { get; set; }
        public MediaType MediaType{ get; set; } // Photo/Video/Floorplan
        public string FileUrl { get; set; }
        public bool IsHero { get; set; }
        public bool DisplayInGallery { get; set; }
        public string UploadById { get; set; }
        public User UploadedBy { get; set; }
        public DateTime UploadAt { get; set; } = DateTime.Now;

    }
}
