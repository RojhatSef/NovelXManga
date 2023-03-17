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

        Task<IEnumerable<Review>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<Review>> GetAllModelAsync();

        Task<Review> GetModelAsync(int idAsync);

        Task<Review> UpdateAsync(Review UpdateModelAsync);

        Task<Review> AddAsync(Review AddAsync);

        Task<Review> DeleteAsync(int idAsync);
    }
}