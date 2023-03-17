using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLBlogRepsitory : IBlogRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLBlogRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public BlogModel Add(BlogModel addNewModel)
        {
            context.blogModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public BlogModel Delete(int id)
        {
            BlogModel BlogDelet = context.blogModels.Find(id);
            if (BlogDelet != null)
            {
                context.blogModels.Remove(BlogDelet);
                context.SaveChanges();
            }
            return BlogDelet;
        }

        public IEnumerable<BlogModel> GetAllModels()
        {
            return context.blogModels;
        }

        public BlogModel GetModel(int id)
        {
            return context.blogModels.Find(id);
        }

        public BlogModel Update(BlogModel UpdateModel)
        {
            var currentModel = context.blogModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<BlogModel> AddAsync(BlogModel AddAsync)
        {
            await context.blogModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<BlogModel> DeleteAsync(int idAsync)
        {
            BlogModel modelToDelete = await context.blogModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.blogModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<BlogModel>> GetAllModelAsync()
        {
            return await context.blogModels.ToListAsync();
        }

        public async Task<BlogModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.blogModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<BlogModel> UpdateAsync(BlogModel UpdateModelAsync)
        {
            var modelUpdate = context.blogModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<BlogModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.blogModels.ToListAsync();
            }
            return await context.blogModels.Where(s => s.mangaName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}