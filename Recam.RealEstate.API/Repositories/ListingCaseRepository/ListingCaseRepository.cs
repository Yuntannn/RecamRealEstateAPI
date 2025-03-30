using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Models;
using System.Security.Claims;

namespace Recam.RealEstate.API.Repositories.ListingCaseRepository
{
    public class ListingCaseRepository : IListingCaseRepository
    {
        private readonly RecamDbContext _recamDbContext;
        private readonly IMapper _mapper;
        private readonly MongoDbContext _mongoDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ListingCaseRepository(RecamDbContext recamDbContext, 
                                     IMapper mapper,
                                     MongoDbContext mongoDbContext,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _recamDbContext = recamDbContext;
            _mapper = mapper;
            _mongoDbContext = mongoDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ListingCaseDto> CreateListingCase(ListingCaseRequestDto listingCaseRequestDto)
        {
            var listingCase = _mapper.Map<ListingCase>(listingCaseRequestDto);
            await _recamDbContext.ListingCases.AddAsync(listingCase);
            await _recamDbContext.SaveChangesAsync();
            var listingCaseDto =  _mapper.Map<ListingCaseDto>(listingCase);

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = listingCase.CreateById,
                Action = "Create New ListingCase",
                Resource = "Listing Case",
                Description = $"A/An {listingCase.PropertyType} ListingCase created successfully.",
                Timestamp = DateTime.Now
            });

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
            var listingCases = await _recamDbContext.ListingCases.ToListAsync();
            return _mapper.Map<List<ListingCaseDto>>(listingCases);
        }

        public async Task<ListingCaseDto> UpdateListingCaseById(int id, ListingCaseRequestDto listingCaseRequestDto)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            if(listingCase == null)
            {
                throw new Exception("Listingcase not found...");
            }
            
            var fieldChange = new List<CaseHistory>();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);


            if(listingCase.Address != listingCaseRequestDto.Address)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Address",
                    OldValue = listingCase.Address,
                    NewValue = listingCaseRequestDto.Address,
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.AssignedToId != listingCaseRequestDto.AssignedToId)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "AssignedToId",
                    OldValue = listingCase.AssignedToId,
                    NewValue = listingCaseRequestDto.AssignedToId,
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.PropertyType != listingCaseRequestDto.PropertyType)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "PropertyType",
                    OldValue = listingCase.PropertyType.ToString(),
                    NewValue = listingCaseRequestDto.PropertyType.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.PropertyStatus != listingCaseRequestDto.PropertyStatus)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "PropertyStatus",
                    OldValue = listingCase.PropertyStatus.ToString(),
                    NewValue = listingCaseRequestDto.PropertyStatus.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Bedrooms != listingCaseRequestDto.Bedrooms)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Bedrooms",
                    OldValue = listingCase.Bedrooms.ToString(),
                    NewValue = listingCaseRequestDto.Bedrooms.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Bathrooms != listingCaseRequestDto.Bathrooms)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Bathrooms",
                    OldValue = listingCase.Bathrooms.ToString(),
                    NewValue = listingCaseRequestDto.Bathrooms.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Garage != listingCaseRequestDto.Garage)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Garage",
                    OldValue = listingCase.Garage.ToString(),
                    NewValue = listingCaseRequestDto.Garage.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Landsize != listingCaseRequestDto.Landsize)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Landsize",
                    OldValue = listingCase.Landsize.ToString(),
                    NewValue = listingCaseRequestDto.Landsize.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Areasize != listingCaseRequestDto.Areasize)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Areasize",
                    OldValue = listingCase.Areasize.ToString(),
                    NewValue = listingCaseRequestDto.Areasize.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
            }

            if (listingCase.Price != listingCaseRequestDto.Price)
            {
                fieldChange.Add(new CaseHistory
                {
                    ListingCaseId = id,
                    FieldName = "Price",
                    OldValue = listingCase.Price.ToString(),
                    NewValue = listingCaseRequestDto.Price.ToString(),
                    ChangedByUserId = userId,
                    ChangedAt = DateTime.Now,
                });
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

            if(fieldChange.Any())
            {
                await _mongoDbContext.CaseHistories.InsertManyAsync(fieldChange);
            }

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = listingCase.CreateById,
                Action = "Update ListingCase",
                Resource = "Listing Case",
                Description = $"ListingCase {listingCase.Id} info updated...",
                Timestamp = DateTime.Now
            });

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

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = listingCase.CreateById,
                Action = "Delete ListingCase",
                Resource = "Listing Case",
                Description = $"ListingCase{listingCase.Id} deleted successfully.",
                Timestamp = DateTime.Now
            });

            return listingCase;
        }

        public async Task<ListingCaseDto> ChangeListingCaseStatusById(int id, PropertyStatus propertyStatus, string changedById)
        {
            var listingCase = await _recamDbContext.ListingCases.FirstOrDefaultAsync(l => l.Id == id);
            if(listingCase == null)
            {
                return null;
            }
            var oldStatus = listingCase.PropertyStatus;
            listingCase.PropertyStatus = propertyStatus;

            var statusHistory = new StatusHistory
            {
                ListingCaseId = id,
                OldStatus = oldStatus,
                NewStatus = propertyStatus,
                ChangedById = changedById,
                ChangedAt = DateTime.Now
            };
            await _recamDbContext.StatusHistories.AddAsync(statusHistory);
            await _recamDbContext.SaveChangesAsync();

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = listingCase.CreateById,
                Action = "Change ListingCase's status",
                Resource = "Listing Case",
                Description = $"ListingCase{listingCase.Id}'s status changed to {listingCase.PropertyStatus} successfully.",
                Timestamp = DateTime.Now
            });

            return _mapper.Map<ListingCaseDto>(listingCase);
        }


        

        

        

        
    }
}
