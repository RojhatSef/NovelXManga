using MangaModelService;

namespace MangaAccessService
{
    public class SQLReadingListPageRepsitory : IReadingListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLReadingListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public ReadingList Add(ReadingList addNewModel)
        {
            context.readingLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public ReadingList Delete(int id)
        {
            ReadingList chaptoDelete = context.readingLists.Find(id);
            if (chaptoDelete != null)
            {
                context.readingLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<ReadingList> GetAllModels()
        {
            return context.readingLists;
        }

        public ReadingList GetModel(int id)
        {
            return context.readingLists.Find(id);
        }

        public ReadingList Update(ReadingList UpdateModel)
        {
            var currentModel = context.readingLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}