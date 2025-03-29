using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Repositories.MediaRepository;
using Recam.RealEstate.API.Services.MediaService;

namespace Recam.RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly RecamDbContext _recamDbContext;

        public MediaController(IMediaService mediaService,
                               RecamDbContext recamDbContext)
        {
            _mediaService = mediaService;
            _recamDbContext = recamDbContext;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("listings/{id}")]
        public async Task<IActionResult> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto)
        {
            var existId = await _recamDbContext.ListingCases.AnyAsync(l => l.Id == id);
            if (!existId)
            {
                return NotFound($"ListingCase with Id {id} is not found...");
            }

            var reuslt = await _mediaService.UploadMediaToCaseId(id, mediaRequestDto);
            return Ok(reuslt);
        }

        [HttpGet("listings/{id}")]
        public async Task<IEnumerable<MediaDto>> GetAllMediaOfCaseId(int id)
        {
            var existId = await _recamDbContext.ListingCases.AnyAsync(l => l.Id == id);
            if (!existId)
            {
                return null;
            }
            return await _mediaService.GetAllMediaOfCaseId(id);
        }

        [HttpDelete("listings/{id}")]
        public async Task<IActionResult> DeleteMediaById(int id)
        {
            var existId = await _recamDbContext.MediaAssets.AnyAsync(m => m.Id == id);
            if (!existId)
            {
                return BadRequest("Media Id not found...");
            }
            await _mediaService.DeleteMediaById(id);
            return Ok($"Media with Id {id} has been deleted.");
        }
    }
}
