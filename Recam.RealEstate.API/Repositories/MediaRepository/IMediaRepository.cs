using Recam.RealEstate.API.DTOs;

namespace Recam.RealEstate.API.Repositories.MediaRepository
{
    public interface IMediaRepository
    {
        Task<MediaDto> UploadMediaToCaseId(int id, MediaRequestDto mediaRequestDto);
        Task<List<MediaDto>> GetAllMediaOfCaseId(int id);
        Task<MediaDto> DeleteMediaById(int id);
    }
}
