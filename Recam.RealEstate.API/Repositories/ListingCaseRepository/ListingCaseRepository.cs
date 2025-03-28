using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.Repositories.ListingCaseRepository
{
    public class ListingCaseRepository : IListingCaseRepository
    {
        private readonly RecamDbContext _recamDbContext;
        private readonly IMapper _mapper;
        public ListingCaseRepository(RecamDbContext recamDbContext, IMapper mapper)
        {
            _recamDbContext = recamDbContext;
            _mapper = mapper;
        }

        public async Task<ListingCaseDto> CreateListingCase(ListingCaseRequestDto listingCaseRequestDto)
        {
            var listingCase = _mapper.Map<ListingCase>(listingCaseRequestDto);
            await _recamDbContext.ListingCases.AddAsync(listingCase);
            await _recamDbContext.SaveChangesAsync();
            var listingCaseDto =  _mapper.Map<ListingCaseDto>(listingCase);
            return listingCaseDto;

        }
        public async Task<ListingCaseDto> GetListingCaseById(int id)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            var listingCaseDto = _mapper.Map<ListingCaseDto>(listingCase);
            return listingCaseDto;
        }

        public async Task<List<ListingCaseDto>> GetListingCases()
        {
            var listingCaseDtos = new List<ListingCaseDto>();
            var listingCases = await _recamDbContext.ListingCases.ToListAsync();
            foreach(var listingCase in listingCases)
            {
                var listingCaseDto = _mapper.Map<ListingCaseDto>(listingCase);
                listingCaseDtos.Add(listingCaseDto);
            }
            return listingCaseDtos;
        }

        public async Task<ListingCaseDto> UpdateListingCaseById(int id, ListingCaseRequestDto listingCaseRequestDto)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            if(listingCase == null)
            {
                throw new Exception("Listingcase not found...");
            }
            listingCase.Address = listingCaseRequestDto.Address;
            listingCase.CreateById = listingCaseRequestDto.CreateById;
            listingCase.AssignedToId = listingCaseRequestDto.AssignedToId;
            listingCase.PropertyType = listingCaseRequestDto.PropertyType;
            listingCase.PropertyStatus = listingCaseRequestDto.PropertyStatus;
            listingCase.Bedrooms = listingCaseRequestDto.Bedrooms;
            listingCase.Bathrooms = listingCaseRequestDto.Bathrooms;
            listingCase.Garage = listingCaseRequestDto.Garage;
            listingCase.Landsize = listingCaseRequestDto.Landsize;
            listingCase.Areasize = listingCaseRequestDto.Areasize;
            listingCase.Price = listingCaseRequestDto.Price;

            await _recamDbContext.SaveChangesAsync();
            return _mapper.Map<ListingCaseDto>(listingCase);
        }

        public async Task<ListingCase> DeleteListingCaseById(int id)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            if(listingCase == null)
            {
                return null;
            }
            _recamDbContext.ListingCases.Remove(listingCase);
            await _recamDbContext.SaveChangesAsync();
            return listingCase;
        }

        public async Task<ListingCaseDto> ChangeListingCaseStatusById(int id, PropertyStatus propertyStatus)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            if(listingCase == null)
            {
                return null;
            }
            listingCase.PropertyStatus = propertyStatus;
            await _recamDbContext.SaveChangesAsync();
            return _mapper.Map<ListingCaseDto>(listingCase);
        }


        

        

        

        
    }
}
