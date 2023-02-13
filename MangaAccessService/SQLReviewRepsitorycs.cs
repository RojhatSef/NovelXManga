using MangaModelService;

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
            return context.Reviews;
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
    }
}