using MangaModelService;
using Microsoft.EntityFrameworkCore;

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

        public async Task<VoiceActorModel> AddAsync(VoiceActorModel AddAsync)
        {
            await context.voiceActorModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<VoiceActorModel> DeleteAsync(int idAsync)
        {
            VoiceActorModel modelToDelete = await context.voiceActorModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.voiceActorModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<VoiceActorModel>> GetAllModelAsync()
        {
            return await context.voiceActorModels.ToListAsync();
        }

        public async Task<VoiceActorModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.voiceActorModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<VoiceActorModel> UpdateAsync(VoiceActorModel UpdateModelAsync)
        {
            var modelUpdate = context.voiceActorModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<VoiceActorModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.voiceActorModels.ToListAsync();
            }
            return await context.voiceActorModels.Where(e => e.FirstName.Contains(searchTermAsync) || e.LastName.Contains(searchTermAsync) || e.NameInNative.Contains(searchTermAsync)).ToListAsync();
        }
    }
}