using MangaModelService;

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

        public ArtistModel Delete(string id)
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

        public ArtistModel GetModel(string id)
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
    }
}
