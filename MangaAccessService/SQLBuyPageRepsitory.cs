using MangaModelService;

namespace MangaAccessService
{
    public class SQLBuyPageRepsitory : IBuyPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLBuyPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public BuyPage Add(BuyPage addNewModel)
        {
            context.BuyPages.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public BuyPage Delete(int id)
        {
            BuyPage modelToDelete = context.BuyPages.Find(id);
            if (modelToDelete != null)
            {
                context.BuyPages.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<BuyPage> GetAllModels()
        {
            return context.BuyPages;
        }

        public BuyPage GetModel(int id)
        {
            return context.BuyPages.Find(id);
        }

        public BuyPage Update(BuyPage UpdateModel)
        {
            var currentModel = context.BuyPages.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}