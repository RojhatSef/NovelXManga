using MangaModelService;

namespace MangaAccessService
{
    public interface IReviewRepsitory
    {
        IEnumerable<Review> Search(string searchTerm);

        IEnumerable<Review> GetAllModels();

        Review GetModel(int id);

        Review Update(Review UpdateModel);

        Review Add(Review addNewModel);

        Review Delete(int id);
    }
}