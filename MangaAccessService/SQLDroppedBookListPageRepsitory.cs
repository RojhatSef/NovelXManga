using MangaModelService;

namespace MangaAccessService
{
    public class SQLDroppedBookListPageRepsitory : IDroppedBookListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLDroppedBookListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public DroppedBookList Add(DroppedBookList addNewModel)
        {
            context.droppedBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public DroppedBookList Delete(int id)
        {
            DroppedBookList chaptoDelete = context.droppedBookLists.Find(id);
            if (chaptoDelete != null)
            {
                context.droppedBookLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<DroppedBookList> GetAllModels()
        {
            return context.droppedBookLists;
        }

        public DroppedBookList GetModel(int id)
        {
            return context.droppedBookLists.Find(id);
        }

        public DroppedBookList Update(DroppedBookList UpdateModel)
        {
            var currentModel = context.droppedBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}