using MangaModelService;

namespace MangaAccessService
{
    public class SQLCompletedBookListPageRepsitory : ICompletedBookListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLCompletedBookListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public CompletedBookList Add(CompletedBookList addNewModel)
        {
            context.completedBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public CompletedBookList Delete(int id)
        {
            CompletedBookList modelToDelete = context.completedBookLists.Find(id);
            if (modelToDelete != null)
            {
                context.completedBookLists.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<CompletedBookList> GetAllModels()
        {
            return context.completedBookLists;
        }

        public CompletedBookList GetModel(int id)
        {
            return context.completedBookLists.Find(id);
        }

        public CompletedBookList Update(CompletedBookList UpdateModel)
        {
            var currentModel = context.completedBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}