using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLAssociatedNamesRepsitory : IAssociatedNamesRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLAssociatedNamesRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public AssociatedNames Add(AssociatedNames addNewModel)
        {
            context.AssociatedNames.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public AssociatedNames Delete(int id)
        {
            AssociatedNames assoToDelete = context.AssociatedNames.Find(id);
            if (assoToDelete != null)
            {
                context.AssociatedNames.Remove(assoToDelete);
                context.SaveChanges();
            }
            return assoToDelete;
        }

        public IEnumerable<AssociatedNames> GetAllModels()
        {
            return context.AssociatedNames;
        }

        public AssociatedNames GetModel(int id)
        {
            return context.AssociatedNames.Find(id);
        }

        public IEnumerable<AssociatedNames> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.AssociatedNames;
            }
            return context.AssociatedNames.Where(e => e.nameString.Contains(searchTerm));
        }

        public AssociatedNames Update(AssociatedNames UpdateModel)
        {
            var currentModel = context.AssociatedNames.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<AssociatedNames> AddAsync(AssociatedNames AddAsync)
        {
            await context.AssociatedNames.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<AssociatedNames> DeleteAsync(int idAsync)
        {
            AssociatedNames modelToDelete = await context.AssociatedNames.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.AssociatedNames.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<AssociatedNames>> GetAllModelAsync()
        {
            return await context.AssociatedNames.ToListAsync();
        }

        public async Task<AssociatedNames> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.AssociatedNames.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<AssociatedNames> UpdateAsync(AssociatedNames UpdateModelAsync)
        {
            var modelUpdate = context.AssociatedNames.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<AssociatedNames>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.AssociatedNames.ToListAsync();
            }
            return await context.AssociatedNames.Where(s => s.nameString.Contains(searchTermAsync)).ToListAsync();
        }
    }
}