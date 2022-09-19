namespace MangaModelService
{
    public class AuthorModel : UserModel
    {
        public AuthorModel()
        {
            Fullname = (FirstName + " " + LastName);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Fullname { get; set; }


        public string? Biography { get; set; }

        public int? AmountOfWork { get; set; }

        public DateTime AuthorBorn { get; set; }
        public DateTime? AuthorDeath { get; set; }

        public string? Contact { get; set; }

        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
