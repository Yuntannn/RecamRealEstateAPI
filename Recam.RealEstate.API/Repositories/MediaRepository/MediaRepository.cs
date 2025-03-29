using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.Repositories.MediaRepository
{
    public class MediaRepository : IMediaRepository
    {
        private readonly RecamDbContext _recamDbContext;
        private readonly IMapper _mapper;

        public MediaRepository(RecamDbContext recamDbContext, IMapper mapper)
        {
            _recamDbContext = recamDbContext;
            _mapper = mapper;
        }

        public async Task<MediaDto> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto)
        {
            var media = _mapper.Map<MediaAsset>(mediaRequestDto);
            media.ListingCaseId = id;
            var newMedia = await _recamDbContext.MediaAssets.AddAsync(media);
            await _recamDbContext.SaveChangesAsync();
            return _mapper.Map<MediaDto>(media);
        }

        public async Task<List<MediaDto>> GetAllMediaOfCaseId(int id)
        {
            var media = await _recamDbContext.MediaAssets.Where(m => m.ListingCaseId == id).ToListAsync();
            return _mapper.Map<List<MediaDto>>(media);
        }

        public async Task<MediaDto> DeleteMediaById(int id)
        {
            var media = _recamDbContext.MediaAssets.FirstOrDefault(m => m.Id == id);
            if(media == null)
            {
                return null;
            }
            _recamDbContext.Remove(media);
            await _recamDbContext.SaveChangesAsync();
            return _mapper.Map<MediaDto>(media);
        }
        
    }
}
