using MangaModelService;

namespace MangaAccessService
{
    public class SQLTagRepository : ITagRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;
        public SQLTagRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public TagModel Add(TagModel addNewModel)
        {
            context.TagModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public TagModel Delete(string id)
        {
            TagModel modelToDelete = context.TagModels.Find(id);
            if (modelToDelete != null)
            {
                context.TagModels.Remove(modelToDelete);
                context.SaveChanges();

            }
            return modelToDelete;
        }

        public IEnumerable<TagModel> GetAllModels()
        {
            return context.TagModels;
        }

        public TagModel GetModel(string id)
        {
            return context.TagModels.Find(id);
        }

        public IEnumerable<TagModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.TagModels;
            }
            return context.TagModels.Where(e => e.TagName.Contains(searchTerm));
        }

        public TagModel Update(TagModel UpdateModel)
        {
            var currentModel = context.TagModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}
