using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLPostRepsitory : IPostRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLPostRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public PostModel Add(PostModel addNewModel)
        {
            context.PostModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public PostModel Delete(int id)
        {
            PostModel modelToDelete = context.PostModels.Find(id);
            if (modelToDelete != null)
            {
                context.PostModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<PostModel> GetAllModels()
        {
            return context.PostModels;
        }

        public PostModel GetModel(int id)
        {
            return context.PostModels.Find(id);
        }

        public IEnumerable<PostModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.PostModels;
            }
            return context.PostModels.Where(e => e.Title.Contains(searchTerm));
        }

        public PostModel Update(PostModel UpdateModel)
        {
            var currentModel = context.PostModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<PostModel> AddAsync(PostModel AddAsync)
        {
            await context.PostModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<PostModel> DeleteAsync(int idAsync)
        {
            PostModel modelToDelete = await context.PostModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.PostModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<PostModel>> GetAllModelAsync()
        {
            return await context.PostModels.ToListAsync();
        }

        public async Task<PostModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.PostModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<PostModel> UpdateAsync(PostModel UpdateModelAsync)
        {
            var modelUpdate = context.PostModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<PostModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.PostModels.ToListAsync();
            }
            return await context.PostModels.Where(s => s.Title.Contains(searchTermAsync)).ToListAsync();
        }
    }
}