using MangaModelService;

namespace MangaAccessService
{
    public class SQLLanguageRepsitory : ILanguageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;
        public SQLLanguageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public Languages Add(Languages addNewModel)
        {
            context.Languages_.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public Languages Delete(string id)
        {
            Languages modelToDelete = context.Languages_.Find(id);
            if (modelToDelete != null)
            {
                context.Languages_.Remove(modelToDelete);
                context.SaveChanges();

            }
            return modelToDelete;
        }

        public IEnumerable<Languages> GetAllModels()
        {
            return context.Languages_;
        }

        public Languages GetModel(string id)
        {
            return context.Languages_.Find(id);
        }

        public IEnumerable<Languages> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Languages_;
            }
            return context.Languages_.Where(e => e.LanguageName.Contains(searchTerm) || e.OfficalWebSiteToBuy.Contains(searchTerm));
        }

        public Languages Update(Languages UpdateModel)
        {
            var currentModel = context.Languages_.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}
