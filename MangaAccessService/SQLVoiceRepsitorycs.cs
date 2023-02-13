using MangaModelService;

namespace MangaAccessService
{
    public class SQLVoiceRepsitorycs : IVoiceRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLVoiceRepsitorycs(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public VoiceActorModel Add(VoiceActorModel addNewModel)
        {
            context.voiceActorModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public VoiceActorModel Delete(int id)
        {
            VoiceActorModel voiceToDele = context.voiceActorModels.Find(id);
            if (voiceToDele != null)
            {
                context.voiceActorModels.Remove(voiceToDele);
                context.SaveChanges();
            }
            return voiceToDele;
        }

        public IEnumerable<VoiceActorModel> GetAllModels()
        {
            return context.voiceActorModels;
        }

        public VoiceActorModel GetModel(int id)
        {
            return context.voiceActorModels.Find(id);
        }

        public IEnumerable<VoiceActorModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.voiceActorModels;
            }
            return context.voiceActorModels.Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm));
        }

        public VoiceActorModel Update(VoiceActorModel UpdateModel)
        {
            var currentModel = context.voiceActorModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}