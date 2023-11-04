using MangaModelService;

namespace MangaAccessService
{
    public interface IUserSettingsRepository
    {
        Task<UserSettings> AddAsync(UserSettings userSettings);

        Task<UserSettings> DeleteAsync(string userModelId);

        Task<UserSettings> GetAsync(string userModelId);

        Task<IEnumerable<UserSettings>> GetAllAsync();

        Task<UserSettings> UpdateAsync(UserSettings userSettings);
    }
}