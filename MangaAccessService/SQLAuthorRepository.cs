using MangaModelService;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AuthorModel> AddAsync(AuthorModel AddAsync)
        {
            await context.authorModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<AuthorModel> DeleteAsync(int idAsync)
        {
            AuthorModel modelToDelete = await context.authorModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.authorModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<AuthorModel>> GetAllModelAsync()
        {
            return await context.authorModels.ToListAsync();
        }

        public async Task<AuthorModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.authorModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<AuthorModel> UpdateAsync(AuthorModel UpdateModelAsync)
        {
            var modelUpdate = context.authorModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<AuthorModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.authorModels.ToListAsync();
            }
            return await context.authorModels.Where(e => e.FirstName.Contains(searchTermAsync) || e.LastName.Contains(searchTermAsync) || e.NameInNative.Contains(searchTermAsync)).ToListAsync();
        }
    }
}