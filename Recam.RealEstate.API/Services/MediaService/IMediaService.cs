using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Repositories.MediaRepository;

namespace Recam.RealEstate.API.Services.MediaService
{
    public interface IMediaService
    {
        Task<MediaDto> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto);
        Task<List<MediaDto>> GetAllMediaOfCaseId(int id);
        Task<MediaDto> DeleteMediaById(int id);
    }
}
