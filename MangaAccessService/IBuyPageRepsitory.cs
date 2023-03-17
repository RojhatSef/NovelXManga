using MangaModelService;

namespace MangaAccessService
{
    public interface IBuyPageRepsitory
    {
        IEnumerable<BuyPage> GetAllModels();

        BuyPage GetModel(int id);

        BuyPage Update(BuyPage UpdateModel);

        BuyPage Add(BuyPage addNewModel);

        BuyPage Delete(int id);

        Task<IEnumerable<BuyPage>> GetAllModelAsync();

        Task<BuyPage> GetModelAsync(int idAsync);

        Task<BuyPage> UpdateAsync(BuyPage UpdateModelAsync);

        Task<BuyPage> AddAsync(BuyPage AddAsync);

        Task<BuyPage> DeleteAsync(int idAsync);
    }
}