namespace MangaModelService
{
    public class ArtistModel : UserModel
    {
        public ArtistModel()
        {
            Fullname = (FirstName + " " + LastName);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Fullname { get; set; }


        public string Biography { get; set; }
        public int AmountOfWork { get; set; }

        public DateTime ArtistBorn { get; set; }
        public DateTime? ArtistDeath { get; set; }

        public string? Contact { get; set; }

        //public ICollection<MasterModel> MasterModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }

    }
}
