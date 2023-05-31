using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLStudioRepsitory : IStudioRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLStudioRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public StudioModel Add(StudioModel addNewModel)
        {
            context.studioModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public StudioModel Delete(int id)
        {
            StudioModel modelToDelete = context.studioModels.Find(id);
            if (modelToDelete != null)
            {
                context.studioModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<StudioModel> GetAllModels()
        {
            return context.studioModels;
        }

        public StudioModel GetModel(int id)
        {
            return context.studioModels.Find(id);
        }

        public IEnumerable<StudioModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.studioModels;
            }
            return context.studioModels.Where(e => e.GroupName.Contains(searchTerm) || e.website.Contains(searchTerm));
        }

        public StudioModel Update(StudioModel UpdateModel)
        {
            var currentModel = context.studioModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<StudioModel> AddAsync(StudioModel AddAsync)
        {
            await context.studioModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<StudioModel> DeleteAsync(int idAsync)
        {
            StudioModel modelToDelete = await context.studioModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.studioModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<StudioModel>> GetAllModelAsync()
        {
            return await context.studioModels.ToListAsync();
        }

        public async Task<StudioModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.studioModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<StudioModel> UpdateAsync(StudioModel UpdateModelAsync)
        {
            var modelUpdate = context.studioModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<StudioModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.studioModels.ToListAsync();
            }
            return await context.studioModels.Where(s => s.GroupName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}