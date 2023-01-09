using MangaModelService;

namespace MangaAccessService
{
    public interface IReviewRepsitory
    {
        IEnumerable<Review> Search(string searchTerm);
        IEnumerable<Review> GetAllModels();
        Review GetModel(string id);

        Review Update(Review UpdateModel);

        Review Add(Review addNewModel);

        Review Delete(string id);
    }
}
