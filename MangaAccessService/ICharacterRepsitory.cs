using MangaAccessService.DTO;
using MangaModelService;

namespace MangaAccessService
{
    public interface ICharacterRepsitory
    {
        IEnumerable<Character> Search(string searchTerm);

        IEnumerable<Character> GetAllModels();

        Character GetModel(int id);

        IEnumerable<Character> GetCharactersByIds(List<int> characterIds);

        Character Update(Character UpdateModel);

        Character Add(Character addNewModel);

        Character Delete(int id);

        Task<IEnumerable<Character>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<Character>> GetAllModelAsync();

        Task<Character> GetModelAsync(int idAsync);

        Task<CurrentCharacterDto> GetMangaDtoIncludedAsync(int id);

        Task<Character> UpdateAsync(Character UpdateModelAsync);

        Task<Character> AddAsync(Character AddAsync);

        Task<Character> DeleteAsync(int idAsync);
    }
}