using MangaModelService;

namespace MangaAccessService
{
    public interface IVoiceRepsitory
    {
        IEnumerable<VoiceActorModel> Search(string searchTerm);

        IEnumerable<VoiceActorModel> GetAllModels();

        VoiceActorModel GetModel(int id);

        VoiceActorModel Update(VoiceActorModel UpdateModel);

        VoiceActorModel Add(VoiceActorModel addNewModel);

        VoiceActorModel Delete(int id);

        Task<IEnumerable<VoiceActorModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<VoiceActorModel>> GetAllModelAsync();

        Task<VoiceActorModel> GetModelAsync(int idAsync);

        Task<VoiceActorModel> UpdateAsync(VoiceActorModel UpdateModelAsync);

        Task<VoiceActorModel> AddAsync(VoiceActorModel AddAsync);

        Task<VoiceActorModel> DeleteAsync(int idAsync);
    }
}