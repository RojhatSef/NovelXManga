namespace MangaModelService
{
    public class AuthorModel : UserModel
    {



        public string Biography { get; set; }

        public int AmountOfWork { get; set; }


        public ICollection<MasterModel> MasterModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
