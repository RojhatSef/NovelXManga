using MangaModelService;

namespace MangaAccessService
{
    public class SQLGroupScanlationRepsitory : IGroupScanlationRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLGroupScanlationRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public GroupScanlatingModel Add(GroupScanlatingModel addNewModel)
        {
            context.groupScanlatingModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public GroupScanlatingModel Delete(int id)
        {
            GroupScanlatingModel modelToDelete = context.groupScanlatingModels.Find(id);
            if (modelToDelete != null)
            {
                context.groupScanlatingModels.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<GroupScanlatingModel> GetAllModels()
        {
            return context.groupScanlatingModels;
        }

        public GroupScanlatingModel GetModel(int id)
        {
            return context.groupScanlatingModels.Find(id);
        }

        public IEnumerable<GroupScanlatingModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.groupScanlatingModels;
            }
            return context.groupScanlatingModels.Where(e => e.GroupName.Contains(searchTerm) || e.website.Contains(searchTerm));
        }

        public GroupScanlatingModel Update(GroupScanlatingModel UpdateModel)
        {
            var currentModel = context.groupScanlatingModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}