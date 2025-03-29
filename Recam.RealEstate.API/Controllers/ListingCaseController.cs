using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Enums;
using Recam.RealEstate.API.Services.ListingCaseService;

namespace Recam.RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingCaseController : ControllerBase
    {
        private readonly IListingCaseService _listingCaseService;
        private readonly IMapper _mapper;

        public ListingCaseController(IListingCaseService listingCaseService, IMapper mapper)
        {
            _listingCaseService = listingCaseService;
            _mapper = mapper;
        }

        [HttpPost("listings")]
        public async Task<IActionResult> CreateListingCase(ListingCaseRequestDto listingCaseRequestDto)
        {
            var result = await _listingCaseService.CreateListingCase(listingCaseRequestDto);
            return CreatedAtAction(nameof(GetListingCaseById), new { id = result.Id }, result);
        }

        [HttpGet("listings/{id}")]
        public async Task<IActionResult> GetListingCaseById(int id)
        {
            var result = await _listingCaseService.GetListingCaseById(id);
            if(result == null)
            {
                return BadRequest("ListingCase not found...");
            }
            return Ok(result);
        }
        [HttpGet("listings")]
        public async Task<IEnumerable<ListingCaseDto>> GetListingCases()
        {
            return await _listingCaseService.GetListingCases();
        }

        [HttpPut("listings/{id}")]
        public async Task<IActionResult> UpdateListingCaseById(int id, ListingCaseRequestDto listingCaseRequestDto)
        {
            var newListingCase = await _listingCaseService.UpdateListingCaseById(id, listingCaseRequestDto);
            return Ok(newListingCase);
        }

        [HttpDelete("listings/{id}")]
        public async Task<IActionResult> DeleteListingCaseById(int id)
        {
            var deletedListingCase = await _listingCaseService.DeleteListingCaseById(id);
            return Ok(deletedListingCase);
        }

        [HttpPatch("listings/{id}")]
        public async Task<IActionResult> ChangeListingCaseStatusById(int id, PropertyStatus propertyStatus)
        {
            var listingCase = await _listingCaseService.ChangeListingCaseStatusById(id, propertyStatus);
            return Ok(listingCase);
        }
    }
}
