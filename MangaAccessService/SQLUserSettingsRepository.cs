using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLUserSettingsRepository : IUserSettingsRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLUserSettingsRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<UserSettings> AddAsync(UserSettings userSettings)
        {
            await context.UserSettings.AddAsync(userSettings);
            await context.SaveChangesAsync();
            return userSettings;
        }

        public async Task<UserSettings> DeleteAsync(string userModelId)
        {
            var settingsToDelete = await context.UserSettings.FindAsync(userModelId);
            if (settingsToDelete != null)
            {
                context.UserSettings.Remove(settingsToDelete);
                await context.SaveChangesAsync();
            }
            return settingsToDelete;
        }

        public async Task<UserSettings> GetAsync(string userModelId)
        {
            return await context.UserSettings
                .Include(us => us.PreferredLanguages)

                .SingleOrDefaultAsync(us => us.UserModelId == userModelId);
        }

        public async Task<IEnumerable<UserSettings>> GetAllAsync()
        {
            return await context.UserSettings
                .Include(us => us.PreferredLanguages)

                .ToListAsync();
        }

        public async Task<UserSettings> UpdateAsync(UserSettings userSettings)
        {
            var settingsToUpdate = await context.UserSettings
                .Include(us => us.PreferredLanguages)

                .SingleOrDefaultAsync(us => us.UserModelId == userSettings.UserModelId);

            if (settingsToUpdate != null)
            {
                // Map all properties that you want to update
                settingsToUpdate.DarkModeEnabled = userSettings.DarkModeEnabled;
                settingsToUpdate.FontSize = userSettings.FontSize;
                settingsToUpdate.ItemsPerPage = userSettings.ItemsPerPage;
                settingsToUpdate.ReadingDirection = userSettings.ReadingDirection;
                settingsToUpdate.ShowMatureContent = userSettings.ShowMatureContent;
                // Further logic to update PreferredLanguages and BlockedUsers as needed

                // The EF Core update method will handle setting the state of the parent entity to modified.
                context.UserSettings.Update(settingsToUpdate);
                await context.SaveChangesAsync();
            }

            return settingsToUpdate;
        }
    }
}