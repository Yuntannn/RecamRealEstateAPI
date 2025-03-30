namespace Recam.RealEstate.API.Models
{
    public class SelectMedia
    {
        public int Id { get; set; }
        public int ListingCaseId { get; set; }
        public ListingCase ListingCase { get; set; }
        public string SelectById { get; set; }
        public User Agent { get; set; }
        public int MediaAssetId { get; set; }
        public MediaAsset MediaAsset { get; set; }
        public bool IsHero { get; set; }
        public bool DisplayInGallery { get; set; }
        public DateTime SelectAt { get; set; } = DateTime.Now;

    }
}
