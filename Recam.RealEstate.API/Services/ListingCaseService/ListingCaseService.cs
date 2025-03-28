using AutoMapper;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;
using Recam.RealEstate.API.Repositories.ListingCaseRepository;

namespace Recam.RealEstate.API.Services.ListingCaseService
{
    public class ListingCaseService : IListingCaseService
    {
        private readonly IListingCaseRepository _listingCaseRepository;
        private readonly IMapper _mapper;
        public ListingCaseService(IListingCaseRepository listingCaseRepository, IMapper mapper)
        {
            _listingCaseRepository = listingCaseRepository;
            _mapper = mapper;
        }
        public async Task<ListingCaseDto> CreateListingCase(ListingCaseRequestDto listingCaseRequestDto)
        {
            return await _listingCaseRepository.CreateListingCase(listingCaseRequestDto);   
        }

        public async Task<ListingCaseDto> GetListingCaseById(int id)
        {
            return await _listingCaseRepository.GetListingCaseById(id);
        }

        public async Task<List<ListingCaseDto>> GetListingCases()
        {
            return await _listingCaseRepository.GetListingCases();
        }

        public async Task<ListingCaseDto> UpdateListingCaseById(int id, ListingCaseRequestDto listingCaseRequestDto)
        {
            return await _listingCaseRepository.UpdateListingCaseById(id, listingCaseRequestDto);
        }

        public async Task<ListingCase> DeleteListingCaseById(int id)
        {
            return await _listingCaseRepository.DeleteListingCaseById(id);
        }

        public async Task<ListingCaseDto> ChangeListingCaseStatusById(int id, PropertyStatus propertyStatus)
        {
            return await _listingCaseRepository.ChangeListingCaseStatusById(id, propertyStatus);
        }

        

        

        

        

        
    }
}
