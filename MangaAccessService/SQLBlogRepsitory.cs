using MangaModelService;

namespace MangaAccessService
{
    public class SQLBlogRepsitory : IBlogRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;
        public SQLBlogRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }
        public BlogModel Add(BlogModel addNewModel)
        {
            context.blogModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public BlogModel Delete(string id)
        {

            BlogModel BlogDelet = context.blogModels.Find(id);
            if (BlogDelet != null)
            {
                context.blogModels.Remove(BlogDelet);
                context.SaveChanges();
            }
            return BlogDelet;
        }

        public IEnumerable<BlogModel> GetAllModels()
        {
            return context.blogModels;
        }

        public BlogModel GetModel(string id)
        {
            return context.blogModels.Find(id);
        }



        public BlogModel Update(BlogModel UpdateModel)
        {
            var currentModel = context.blogModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}
