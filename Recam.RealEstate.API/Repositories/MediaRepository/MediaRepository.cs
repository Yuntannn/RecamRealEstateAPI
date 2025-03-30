using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;
using System.Security.Claims;

namespace Recam.RealEstate.API.Repositories.MediaRepository
{
    public class MediaRepository : IMediaRepository
    {
        private readonly RecamDbContext _recamDbContext;
        private readonly IMapper _mapper;
        private readonly MongoDbContext _mongoDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MediaRepository(RecamDbContext recamDbContext, 
                               IMapper mapper, 
                               MongoDbContext mongoDbContext,
                               IHttpContextAccessor httpContextAccessor)
        {
            _recamDbContext = recamDbContext;
            _mapper = mapper;
            _mongoDbContext = mongoDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MediaDto> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto)
        {
            var media = _mapper.Map<MediaAsset>(mediaRequestDto);
            media.ListingCaseId = id;
            var newMedia = await _recamDbContext.MediaAssets.AddAsync(media);
            await _recamDbContext.SaveChangesAsync();

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = media.UploadById,
                Action = "Upload New Media",
                Resource = "MediaAssets",
                Description = $"A/An {media.MediaType} Media created successfully.",
                Timestamp = DateTime.Now
            });

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

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = media.UploadById,
                Action = "Delete Media",
                Resource = "MediaAssets",
                Description = $"Media {media.Id} was deleted...",
                Timestamp = DateTime.Now
            });

            return _mapper.Map<MediaDto>(media);
        }
        

        //public async Task<List<MediaDto>> SelectMediaByCaseId(SelectMediaRequestDto selectMediaRequestDto)
        //{
        //    var agentId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var selectedMedias = new List<SelectMedia>();
        //    foreach(var mediaId in selectMediaRequestDto.MediaAssetId)
        //    {
        //        var selectedMedia = new SelectMedia
        //        {
        //            ListingCaseId = selectMediaRequestDto.ListingCaseId,
        //            UploadById = agentId,
        //            MediaAssetId = mediaId,
        //            SelectAt = DateTime.Now
        //        };
        //        selectedMedias.Add(selectedMedia);
        //    }
        //    await _recamDbContext.SelectMedias.AddRangeAsync(selectedMedias);
        //    return _mapper.Map<List<MediaDto>>(selectedMedias);
        //}
    }
}
