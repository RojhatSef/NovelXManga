using MangaModelService;

namespace MangaAccessService
{
    public interface IAuthorRepsitory
    {
        IEnumerable<AuthorModel> Search(string searchTerm);
        IEnumerable<AuthorModel> GetAuthorModels();
        AuthorModel GetAuthor(string id);

        AuthorModel Update(AuthorModel updateAuthorModel);

        AuthorModel Add(AuthorModel newAuthorModel);

        AuthorModel Delete(string id);
    }
}
