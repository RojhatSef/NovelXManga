using MangaModelService;

namespace MangaAccessService
{
    public class SQLOfficalWebsiteRepsitory : IOfficalWebsiteRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLOfficalWebsiteRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public OfficalWebsite Add(OfficalWebsite addNewModel)
        {
            context.OfficalWebsites.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public OfficalWebsite Delete(int id)
        {
            OfficalWebsite modelToDelete = context.OfficalWebsites.Find(id);
            if (modelToDelete != null)
            {
                context.OfficalWebsites.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<OfficalWebsite> GetAllModels()
        {
            return context.OfficalWebsites;
        }

        public OfficalWebsite GetModel(int id)
        {
            return context.OfficalWebsites.Find(id);
        }

        public IEnumerable<OfficalWebsite> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.OfficalWebsites;
            }
            return context.OfficalWebsites.Where(e => e.WebsiteName.Contains(searchTerm));
        }

        public OfficalWebsite Update(OfficalWebsite UpdateModel)
        {
            var currentModel = context.OfficalWebsites.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}