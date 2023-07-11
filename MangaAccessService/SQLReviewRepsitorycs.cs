using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLReviewRepsitorycs : IReviewRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLReviewRepsitorycs(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public Review Add(Review addNewModel)
        {
            context.Reviews.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public Review Delete(int id)
        {
            Review modelToDelete = context.Reviews.Find(id);
            if (modelToDelete != null)
            {
                context.Reviews.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<Review> GetAllModels()
        {
            return context.Reviews.Include(r => r.UserModels);
        }

        public Review GetModel(int id)
        {
            return context.Reviews.Find(id);
        }

        public IEnumerable<Review> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Reviews;
            }
            return context.Reviews.Where(e => e.Title.Contains(searchTerm));
        }

        public Review Update(Review UpdateModel)
        {
            var currentModel = context.Reviews.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<Review> AddAsync(Review AddAsync)
        {
            await context.Reviews.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<Review> DeleteAsync(int idAsync)
        {
            Review modelToDelete = await context.Reviews.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.Reviews.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<Review>> GetAllModelAsync()
        {
            return await context.Reviews.Include(r => r.UserModels).ToListAsync();
        }

        public async Task<Review> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.Reviews.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<Review> UpdateAsync(Review UpdateModelAsync)
        {
            var modelUpdate = context.Reviews.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<Review>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.Reviews.ToListAsync();
            }
            return await context.Reviews.Where(s => s.Title.Contains(searchTermAsync)).ToListAsync();
        }
    }
}