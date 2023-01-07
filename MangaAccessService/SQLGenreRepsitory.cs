using MangaModelService;

namespace MangaAccessService
{
    public class SQLGenreRepsitory : IGenreRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;
        public SQLGenreRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public GenresModel Add(GenresModel addNewModel)
        {
            context.GenresModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public GenresModel Delete(string id)
        {
            GenresModel modelToDelete = context.GenresModels.Find(id);
            if (modelToDelete != null)
            {
                context.GenresModels.Remove(modelToDelete);
                context.SaveChanges();

            }
            return modelToDelete;
        }

        public IEnumerable<GenresModel> GetAllModels()
        {
            return context.GenresModels;
        }

        public GenresModel GetModel(string id)
        {
            return context.GenresModels.Find(id);
        }

        public IEnumerable<GenresModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.GenresModels;
            }
            return context.GenresModels.Where(e => e.GenreName.Contains(searchTerm));
        }

        public GenresModel Update(GenresModel UpdateModel)
        {
            var currentModel = context.GenresModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}
