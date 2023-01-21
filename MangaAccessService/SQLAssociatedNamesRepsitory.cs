using MangaModelService;

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

        public AssociatedNames Delete(string id)
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

        public AssociatedNames GetModel(string id)
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
            throw new NotImplementedException();
        }
    }
}
