using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLTagRepository : ITagRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLTagRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public TagModel Add(TagModel addNewModel)
        {
            context.TagModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public TagModel Delete(int id)
        {
            TagModel modelToDelete = context.TagModels.Find(id);
            if (modelToDelete != null)
            {
                context.TagModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<TagModel> GetAllModels()
        {
            return context.TagModels;
        }

        public TagModel GetModel(int id)
        {
            return context.TagModels.Find(id);
        }

        public IEnumerable<TagModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.TagModels;
            }
            return context.TagModels.Where(e => e.TagName.Contains(searchTerm));
        }

        public TagModel Update(TagModel UpdateModel)
        {
            var currentModel = context.TagModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<TagModel> AddAsync(TagModel AddAsync)
        {
            await context.TagModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<TagModel> DeleteAsync(int idAsync)
        {
            TagModel modelToDelete = await context.TagModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.TagModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<TagModel>> GetAllModelAsync()
        {
            return await context.TagModels.ToListAsync();
        }

        public async Task<TagModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.TagModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<TagModel> UpdateAsync(TagModel UpdateModelAsync)
        {
            var modelUpdate = context.TagModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<TagModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.TagModels.ToListAsync();
            }
            return await context.TagModels.Where(s => s.TagName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}