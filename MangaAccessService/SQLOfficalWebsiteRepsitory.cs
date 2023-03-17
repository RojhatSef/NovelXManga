using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLOfficalWebsiteRepsitory : IOfficalWebsiteRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLOfficalWebsiteRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public OfficalWebsite Add(OfficalWebsite addNewModel)
        {
            context.OfficalWebsites.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public OfficalWebsite Delete(int id)
        {
            OfficalWebsite modelToDelete = context.OfficalWebsites.Find(id);
            if (modelToDelete != null)
            {
                context.OfficalWebsites.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<OfficalWebsite> GetAllModels()
        {
            return context.OfficalWebsites;
        }

        public OfficalWebsite GetModel(int id)
        {
            return context.OfficalWebsites.Find(id);
        }

        public IEnumerable<OfficalWebsite> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.OfficalWebsites;
            }
            return context.OfficalWebsites.Where(e => e.OfficalWebsiteName.Contains(searchTerm));
        }

        public OfficalWebsite Update(OfficalWebsite UpdateModel)
        {
            var currentModel = context.OfficalWebsites.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<OfficalWebsite> AddAsync(OfficalWebsite AddAsync)
        {
            await context.OfficalWebsites.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<OfficalWebsite> DeleteAsync(int idAsync)
        {
            OfficalWebsite modelToDelete = await context.OfficalWebsites.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.OfficalWebsites.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<OfficalWebsite>> GetAllModelAsync()
        {
            return await context.OfficalWebsites.ToListAsync();
        }

        public async Task<OfficalWebsite> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.OfficalWebsites.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<OfficalWebsite> UpdateAsync(OfficalWebsite UpdateModelAsync)
        {
            var modelUpdate = context.OfficalWebsites.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<OfficalWebsite>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.OfficalWebsites.ToListAsync();
            }
            return await context.OfficalWebsites.Where(s => s.OfficalWebsiteName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}