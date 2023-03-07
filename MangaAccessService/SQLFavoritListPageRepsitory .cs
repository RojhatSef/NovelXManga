using MangaModelService;

namespace MangaAccessService
{
    public class SQLFavoritListPageRepsitory : IFavoritListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLFavoritListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public FavoritBookList Add(FavoritBookList addNewModel)
        {
            context.favoritBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public FavoritBookList Delete(int id)
        {
            FavoritBookList chaptoDelete = context.favoritBookLists.Find(id);
            if (chaptoDelete != null)
            {
                context.favoritBookLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<FavoritBookList> GetAllModels()
        {
            return context.favoritBookLists;
        }

        public FavoritBookList GetModel(int id)
        {
            return context.favoritBookLists.Find(id);
        }

        public FavoritBookList Update(FavoritBookList UpdateModel)
        {
            var currentModel = context.favoritBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}