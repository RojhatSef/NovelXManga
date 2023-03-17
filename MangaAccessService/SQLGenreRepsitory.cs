using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLGenreRepsitory : IGenreRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLGenreRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public GenresModel Add(GenresModel addNewModel)
        {
            context.GenresModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public GenresModel Delete(int id)
        {
            GenresModel modelToDelete = context.GenresModels.Find(id);
            if (modelToDelete != null)
            {
                context.GenresModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<GenresModel> GetAllModels()
        {
            return context.GenresModels;
        }

        public GenresModel GetModel(int id)
        {
            return context.GenresModels.Find(id);
        }

        public IEnumerable<GenresModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.GenresModels;
            }
            return context.GenresModels.Where(e => e.GenreName.Contains(searchTerm));
        }

        public GenresModel Update(GenresModel UpdateModel)
        {
            var currentModel = context.GenresModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<GenresModel> AddAsync(GenresModel AddAsync)
        {
            await context.GenresModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<GenresModel> DeleteAsync(int idAsync)
        {
            GenresModel modelToDelete = await context.GenresModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.GenresModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<GenresModel>> GetAllModelAsync()
        {
            return await context.GenresModels.ToListAsync();
        }

        public async Task<GenresModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.GenresModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<GenresModel> UpdateAsync(GenresModel UpdateModelAsync)
        {
            var modelUpdate = context.GenresModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<GenresModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.GenresModels.ToListAsync();
            }
            return await context.GenresModels.Where(s => s.GenreName.Contains(searchTermAsync)).ToListAsync();
        }
    }
}