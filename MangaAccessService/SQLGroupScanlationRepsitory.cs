using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLGroupScanlationRepsitory : IGroupScanlationRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLGroupScanlationRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public GroupScanlatingModel Add(GroupScanlatingModel addNewModel)
        {
            context.groupScanlatingModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public GroupScanlatingModel Delete(int id)
        {
            GroupScanlatingModel modelToDelete = context.groupScanlatingModels.Find(id);
            if (modelToDelete != null)
            {
                context.groupScanlatingModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<GroupScanlatingModel> GetAllModels()
        {
            return context.groupScanlatingModels;
        }

        public GroupScanlatingModel GetModel(int id)
        {
            return context.groupScanlatingModels.Find(id);
        }

        public IEnumerable<GroupScanlatingModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.groupScanlatingModels;
            }
            return context.groupScanlatingModels.Where(e => e.GroupName.Contains(searchTerm) || e.website.Contains(searchTerm));
        }

        public GroupScanlatingModel Update(GroupScanlatingModel UpdateModel)
        {
            var currentModel = context.groupScanlatingModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<GroupScanlatingModel> AddAsync(GroupScanlatingModel AddAsync)
        {
            await context.groupScanlatingModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<GroupScanlatingModel> DeleteAsync(int idAsync)
        {
            GroupScanlatingModel modelToDelete = await context.groupScanlatingModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.groupScanlatingModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<GroupScanlatingModel>> GetAllModelAsync()
        {
            return await context.groupScanlatingModels.ToListAsync();
        }

        public async Task<GroupScanlatingModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.groupScanlatingModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<GroupScanlatingModel> UpdateAsync(GroupScanlatingModel UpdateModelAsync)
        {
            var modelUpdate = context.groupScanlatingModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<GroupScanlatingModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.groupScanlatingModels.ToListAsync();
            }
            return await context.groupScanlatingModels.Where(s => s.GroupName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}