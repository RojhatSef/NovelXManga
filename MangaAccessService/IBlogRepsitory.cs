using MangaModelService;

namespace MangaAccessService
{
    public interface IBlogRepsitory
    {
        IEnumerable<BlogModel> GetAllModels();

        BlogModel GetModel(int id);

        BlogModel Update(BlogModel UpdateModel);

        BlogModel Add(BlogModel addNewModel);

        BlogModel Delete(int id);
    }
}