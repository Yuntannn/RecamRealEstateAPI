using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Repositories.MediaRepository;

namespace Recam.RealEstate.API.Services.MediaService
{
    public class MediaService:IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaDto> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto)
        {
            return await _mediaRepository.UploadMediaToCaseId(id, mediaRequestDto);
        }
        
        public async Task<List<MediaDto>> GetAllMediaOfCaseId(int id)
        {
            return await _mediaRepository.GetAllMediaOfCaseId(id);
        }
        public async Task<MediaDto> DeleteMediaById(int id)
        {
            return await _mediaRepository.DeleteMediaById(id);
        }

    }
}
