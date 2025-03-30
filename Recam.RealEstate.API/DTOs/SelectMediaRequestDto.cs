namespace Recam.RealEstate.API.DTOs
{
    public class SelectMediaRequestDto
    {
        public int ListingCaseId { get; set; }
        public List<int> MediaAssetId { get; set; } = new List<int>();
        public string AgentId { get; set; }
    }
}
