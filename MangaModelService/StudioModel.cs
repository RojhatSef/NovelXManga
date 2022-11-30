namespace MangaModelService
{
    public class StudioModel : GroupScanlatingModel
    {


        public string? Biography { get; set; }
        public int? Works { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? DissolutionOfCorporation { get; set; }

        public string StudioWebsite { get; set; }
    }
}
