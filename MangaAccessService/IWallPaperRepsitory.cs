using MangaModelService;

namespace MangaAccessService
{
    public interface IWallPaperRepsitory
    {
        IEnumerable<WallPapers> GetAllModels();

        WallPapers GetModel(int id);

        WallPapers Update(WallPapers UpdateModel);

        WallPapers Add(WallPapers addNewModel);

        WallPapers Delete(int id);

        Task<IEnumerable<WallPapers>> GetAllModelAsync();

        Task<WallPapers> GetModelAsync(int idAsync);

        Task<WallPapers> UpdateAsync(WallPapers UpdateModelAsync);

        Task<WallPapers> AddAsync(WallPapers AddAsync);

        Task<WallPapers> DeleteAsync(int idAsync);
    }
}