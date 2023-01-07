using MangaModelService;

namespace MangaAccessService
{
    public interface ITagRepsitory
    {
        IEnumerable<TagModel> Search(string searchTerm);
        IEnumerable<TagModel> GetAllModels();
        TagModel GetModel(string id);

        TagModel Update(TagModel UpdateModel);

        TagModel Add(TagModel addNewModel);

        TagModel Delete(string id);
    }
}
