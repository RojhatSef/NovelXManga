using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLArtistRepsitory : IArtistRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLArtistRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public ArtistModel Add(ArtistModel addNewModel)
        {
            context.artistModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public ArtistModel Delete(int id)
        {
            ArtistModel artistToDelte = context.artistModels.Find(id);
            if (artistToDelte != null)
            {
                context.artistModels.Remove(artistToDelte);
                context.SaveChanges();
            }
            return artistToDelte;
        }

        public IEnumerable<ArtistModel> GetAllModels()
        {
            return context.artistModels;
        }

        public ArtistModel GetModel(int id)
        {
            return context.artistModels.Find(id);
        }

        public IEnumerable<ArtistModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.artistModels;
            }
            return context.artistModels.Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm));
        }

        public ArtistModel Update(ArtistModel UpdateModel)
        {
            var currentModel = context.artistModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<ArtistModel> AddAsync(ArtistModel AddAsync)
        {
            await context.artistModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<ArtistModel> DeleteAsync(int idAsync)
        {
            ArtistModel modelToDelete = await context.artistModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.artistModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<ArtistModel>> GetAllModelAsync()
        {
            return await context.artistModels.ToListAsync();
        }

        public async Task<ArtistModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.artistModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<ArtistModel> UpdateAsync(ArtistModel UpdateModelAsync)
        {
            var modelUpdate = context.artistModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<ArtistModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.artistModels.ToListAsync();
            }
            return await context.artistModels.Where(e => e.FirstName.Contains(searchTermAsync) || e.LastName.Contains(searchTermAsync) || e.NameInNative.Contains(searchTermAsync)).ToListAsync();
        }
    }
}