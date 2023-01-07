using MangaModelService;


namespace MangaAccessService
{
    public interface IVoiceRepsitory
    {
        IEnumerable<VoiceActorModel> Search(string searchTerm);
        IEnumerable<VoiceActorModel> GetAllModels();
        VoiceActorModel GetModel(string id);

        VoiceActorModel Update(VoiceActorModel UpdateModel);

        VoiceActorModel Add(VoiceActorModel addNewModel);

        VoiceActorModel Delete(string id);
    }
}
