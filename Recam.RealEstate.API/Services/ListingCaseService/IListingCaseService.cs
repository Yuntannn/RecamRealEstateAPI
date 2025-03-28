using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.Services.ListingCaseService
{
    public interface IListingCaseService
    {
        Task<ListingCaseDto> CreateListingCase(ListingCaseRequestDto listingCaseRequestDto);
        Task<ListingCaseDto> GetListingCaseById(int id);
        Task<List<ListingCaseDto>> GetListingCases();
        Task<ListingCaseDto> UpdateListingCaseById(int id, ListingCaseRequestDto listingCaseRequestDto);
        Task<ListingCase> DeleteListingCaseById(int id);
        Task<ListingCaseDto> ChangeListingCaseStatusById(int id, PropertyStatus propertyStatus);
    }
}
