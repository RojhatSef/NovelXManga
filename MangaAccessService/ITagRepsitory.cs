using MangaModelService;

namespace MangaAccessService
{
    public interface ITagRepsitory
    {
        IEnumerable<TagModel> Search(string searchTerm);

        IEnumerable<TagModel> GetAllModels();

        TagModel GetModel(int id);

        TagModel Update(TagModel UpdateModel);

        TagModel Add(TagModel addNewModel);

        TagModel Delete(int id);
    }
}