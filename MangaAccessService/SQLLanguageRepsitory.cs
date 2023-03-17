using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLLanguageRepsitory : ILanguageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLLanguageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public Languages Add(Languages addNewModel)
        {
            context.Languages_.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public Languages Delete(int id)
        {
            Languages modelToDelete = context.Languages_.Find(id);
            if (modelToDelete != null)
            {
                context.Languages_.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<Languages> GetAllModels()
        {
            return context.Languages_;
        }

        public Languages GetModel(int id)
        {
            return context.Languages_.Find(id);
        }

        public IEnumerable<Languages> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Languages_;
            }
            return context.Languages_.Where(e => e.LanguageName.Contains(searchTerm));
        }

        public Languages Update(Languages UpdateModel)
        {
            var currentModel = context.Languages_.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<Languages> AddAsync(Languages AddAsync)
        {
            await context.Languages_.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<Languages> DeleteAsync(int idAsync)
        {
            Languages modelToDelete = await context.Languages_.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.Languages_.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<Languages>> GetAllModelAsync()
        {
            return await context.Languages_.ToListAsync();
        }

        public async Task<Languages> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.Languages_.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<Languages> UpdateAsync(Languages UpdateModelAsync)
        {
            var modelUpdate = context.Languages_.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<Languages>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.Languages_.ToListAsync();
            }
            return await context.Languages_.Where(s => s.LanguageName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}