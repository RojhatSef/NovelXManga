namespace MangaModelService
{
    public class StudioModel : GroupScanlatingModel
    {


        public string Biography { get; set; }
        public int Works { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }
    }
}
