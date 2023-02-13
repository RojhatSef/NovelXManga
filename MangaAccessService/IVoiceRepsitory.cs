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
    }
}