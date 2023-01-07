using MangaModelService;


namespace MangaAccessService
{
    public interface IChapterModelRepsitory
    {
        IEnumerable<ChapterModel> Search(string searchTerm);
        IEnumerable<ChapterModel> GetAllModels();
        ChapterModel GetModel(string id);

        ChapterModel Update(ChapterModel UpdateModel);

        ChapterModel Add(ChapterModel addNewModel);

        ChapterModel Delete(string id);
    }
}
