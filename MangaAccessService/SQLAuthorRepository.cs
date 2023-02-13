using MangaModelService;

namespace MangaAccessService
{
    public class SQLAuthorRepository : IAuthorRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLAuthorRepository(MangaNNovelAuthDBContext mangaNNovelAuthDBContext)
        {
            this.context = mangaNNovelAuthDBContext;
        }

        public AuthorModel Add(AuthorModel newAuthorModel)
        {
            context.authorModels.Add(newAuthorModel);
            context.SaveChanges();
            return newAuthorModel;
        }

        public AuthorModel Delete(int id)
        {
            AuthorModel authorToDelete = context.authorModels.Find(id);
            if (authorToDelete != null)
            {
                context.authorModels.Remove(authorToDelete);
                context.SaveChanges();
            }
            return authorToDelete;
        }

        public AuthorModel GetAuthor(int id)
        {
            return context.authorModels.Find(id);
        }

        public IEnumerable<AuthorModel> GetAuthorModels()
        {
            return context.authorModels;
        }

        public IEnumerable<AuthorModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.authorModels;
            }
            return context.authorModels.Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm));
        }

        public AuthorModel Update(AuthorModel updateAuthorModel)
        {
            var assigment = context.authorModels.Attach(updateAuthorModel);
            assigment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updateAuthorModel;
        }
    }
}