using MangaModelService;

namespace MangaAccessService
{
    public interface IGenreRepsitory
    {
        IEnumerable<GenresModel> Search(string searchTerm);

        IEnumerable<GenresModel> GetAllModels();

        GenresModel GetModel(int id);

        GenresModel Update(GenresModel UpdateModel);

        GenresModel Add(GenresModel addNewModel);

        GenresModel Delete(int id);

        Task<IEnumerable<GenresModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<GenresModel>> GetAllModelAsync();

        Task<GenresModel> GetModelAsync(int idAsync);

        Task<GenresModel> UpdateAsync(GenresModel UpdateModelAsync);

        Task<GenresModel> AddAsync(GenresModel AddAsync);

        Task<GenresModel> DeleteAsync(int idAsync);
    }
}