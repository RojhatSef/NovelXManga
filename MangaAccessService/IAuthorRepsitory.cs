using MangaModelService;

namespace MangaAccessService
{
    public interface IAuthorRepsitory
    {
        IEnumerable<AuthorModel> Search(string searchTerm);

        IEnumerable<AuthorModel> GetAuthorModels();

        AuthorModel GetAuthor(int id);

        AuthorModel Update(AuthorModel updateAuthorModel);

        AuthorModel Add(AuthorModel newAuthorModel);

        AuthorModel Delete(int id);

        Task<IEnumerable<AuthorModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<AuthorModel>> GetAllModelAsync();

        Task<AuthorModel> GetModelAsync(int idAsync);

        Task<AuthorModel> UpdateAsync(AuthorModel UpdateModelAsync);

        Task<AuthorModel> AddAsync(AuthorModel AddAsync);

        Task<AuthorModel> DeleteAsync(int idAsync);
    }
}