using MangaModelService;

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

        public PostModel Delete(string id)
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

        public PostModel GetModel(string id)
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
    }
}
