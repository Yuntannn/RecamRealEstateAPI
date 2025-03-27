using Microsoft.AspNetCore.Identity;

namespace Recam.RealEstate.API.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string UserRole { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfileImageUrl { get; set; }


        public ICollection<ListingCase> CreatedListings { get; set; } // A User can create many ListingCases (CreatedBy/Admin)
        public ICollection<ListingCase> AssignedListings { get; set; }  // A User  can be assigned to many ListingCases (AssignedTo/Agent)
        public ICollection<MediaAsset> MediaAssets { get; set; } // A User can uploads many MediaAssets (Admin)
        public ICollection<StatusHistory> StatusHistories { get; set; } // A User performs the status change (ChangedBy/Admin)
    }
}
