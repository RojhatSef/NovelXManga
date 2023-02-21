using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga
{
    public class SeedData
    {
        #region properties, constructor and readonly.

        public UserModel User { get; set; }
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MangaNNovelAuthDBContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedData(MangaNNovelAuthDBContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.roleManager = roleManager;
        }

        #endregion properties, constructor and readonly.

        #region CommonSeed

        #region RelatedManga

        public void RelatedManga()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            manga1.RecommendedMangaModels = new List<MangaModel> { manga2 };
            manga2.RecommendedMangaModels = new List<MangaModel> { manga1 };
            context.mangaModels.Update(manga1);
            context.mangaModels.Update(manga2);
            context.SaveChanges();
        }

        #endregion RelatedManga

        #region GenreSeeding

        public void GenreSeed()
        {
            string[] Genres = {  "Action", "Adult", "Adventure", "Comedy", "Drama", "Ecchi", "Fantasy", "Gender Bender", "Harem", "Historical",
                "Classics", "Comic", "Graphic Novel", "Detective", "Horror", "Josei", "Martial Arts", "Mature", "Mecha", "Mystery", "Psychological", "Romance",
                "School Life", "Sci-fi", "Seinen", "Detective", "Shoujo", "Shoujo Ai", "Shounen", "Shounen Ai", "Slice Of Life", "Smut", "Psychological", "Sports",
            "Supernatural", "Tragedy", "Wuxia", "Xianxia", "Xuanhuan", "Yaoi / Gay ", "Yuri / Lesbian", "Ecchi", "Fantasy", "Gender Bender", "Harem", "Historical",
            "Memoir", "True Crime", "Self Help", "Poetry", "Essay","Cookbooks", "Biographies and Autobiographies", "Short Stories",};
            var GenreList = new List<GenresModel>();
            for (int i = 0; i < Genres.Length; i++)
            {
                GenresModel newgenre = new GenresModel();
                newgenre.GenreName = Genres[i];
                GenreList.Add(newgenre);
            }
            context.GenresModels.AddRange(GenreList);
            context.SaveChanges();
        }

        #endregion GenreSeeding

        #region TagSeed

        public void TagsModel()
        {
            string[] Genres = {"21st century", "4-koma/Yonkoma", "Abandoned Children", "Absent Parent",

                "Accident", "Acting", "Actor", "Adapted to Anime", "Adapted to Drama CD", "Adapted to Game", "Adapted to Live Action",

                "Alien", "Age Gap", "All-Boys School", "All-Girls School", "Alternate Reality", "Ambitious Goal/s", "America",

                "Adapted to Manga", "Adapted to Movie", "Adopted Protagonist", "Adoption", "Affectionate Seme",

                "Amnesia", "Anal Intercourse", "Androgynous Male Lead", "Angel", "Animal Characteristics", "Angst",

                "Anilingus", "Animal Sidekick", "Animal Transformation", "Antihero", "Antiheroine", "Apocalypse",

                "Appearance Changes", "Appearance Different from Actual Age", "Appearance Different from Personality", "Aristocrat", "Armed Combat", "Arranged Marriage ", "Arrogant Female Lead",

                "Arrogant Male Lead", "Artist", "Artificial Intelligence", "Assassin", "Attempted Murder",

                "Attempted Rape", "Attempted Suicide", "Atypical Art Style", "Award-Nominated Work", "Award-Winning Work",

                "Based on a Novel", "Based on a Manga", "Based on a Movie", "Based on a Song", "Bathroom Intercourse", "BDSM",

                "Beautiful Female Lead", "Beautiful Male Lead", "Bespectacled Protagonist", "Bisexual Character/s",

                "Best Friends", "Betrayal", "Bickering Love", "Big Breasts",

                "Bishounen", "Blackmail", "Bishoujo", "Blood and Gore",

                "Blushing Male Lead", "Blushing Female Lead", "Body Swap", "Bondage", "Bodyguard", "Borderline H", "Boss-Subordinate Relationship",
                "Breakup", "Bromance", "Brother and Sister", "Brother Complex", "Brother",

                 "Bullying", "Businessman", "Businesswoman", "Butler", "Calm Protagonist", "Cat",
                "Caught in the Act", "Celebrity/ies", "Changed by Love", "Character Growth", "Character Who Bullies the One They Love", "Chasing After Love Interest",

                  "Cheating", "Child Abuse", "Children", "Childhood Friends", "Childhood Love", "Childhood Promise",
                "Childhood Trauma", "Clever Protagonist", "Clubs", "Clumsy Protagonist", "Cohabitation", "Cold Male Lead",

                   "Cold Female Lead", "Collection of Inter-Linked Stories", "Collection of Stories", "Comedic Undertone", "Comic Artist",

                 "Coming of Age",  "Competition", "Conspiracy", "Contract", "Cooking", "Cool Male Lead", "Cool Female Lead",

                 "Cyberpunk", "Cosplay", "Countryside", "Couple Growth", "Cousin", "Coworker Relationship", "Coworker", "Crime", "Cross-dressing",
                "Cunnilingus", "Curse", "Dark Ambience",

                  "Dead Family Member", "Dead Friend", "Dead Lover", "Dead Parent", "Death", "Death of Loved One",
                "Debt", "Deception", "Delinquent", "Demon Lord", "Demon", "Dense When It Comes To Love",

                      "Depression", "Detective", "Determined Protagonist", "Devil", "Disability", "Discrimination",
                "Doctor", "Dog", "Dungeon", "Dormitory", "Double Penetration", "Dragon",

                   "Dream", "Drug", "Drunken Intercourse", "Dubious Consent", "Elves", "Emotionally Strong Female Lead",
                "Emotionally Weak Female Lead",

                 "Emotionally Strong Male Lead", "Emotionally Weak Male Lead", "Enemies Become Friends", "Episodic", "ESP",
                "European Ambience", "Exhibitionism", "Exorcism", "Experiments", "Expressionless Protagonist", "Extended Flashbacks", "Eyepatch",

                  "Family", "Family Drama", "Fantasy World", "Fast Romance", "Father and Daughter", "Fellatio",

                "Female Demographic with Male Lead", "Female Dominance", "Female Fighters", "Female Lead Falls in Love First",
                "Fetishes", "Finding Love Again", "First Kiss", "First Love", "First-Time Intercourse", "Flashbacks", "Food", "Forbidden Love",

           "Foreigners", "Friends Become Enemies", "Friends Become Lovers", "Friends Grow Distant",
                "Friendship", "Full Color", "Futanari", "Future", "Game Elements", "GameLit", "Games",
              "Gang Rape", "Gangs", "Genetically Engineered", "Genius", "Ghosts", "Glasses-Wearing Female Lead",
                "Glasses-Wearing Male Lead", "Glasses-Wearing Uke God", "Goddesses", "Groping", "Group Intercourse", "Guardian Relationship",

           "Guilt", "Gun", "Handjob", "Handsome Male Lead", "Handsome Female Lead", "Hard-Working Protagonist",
                "Height Difference", "Hero", "Hidden Past", "High School", "High School Student", "Homophobia",
              "Honest Protagonist", "Host", "Hostess", "Hot-Blooded Male Lead", "Hot-Blooded Female Lead",
                "Human Experiment", "Human-Nonhuman Relationship", "Hunted Protagonist",
                "Idol", "Immortality", "Important Non-Romantic Relationship", "Incest",

           "Independent Female Lead", "Independent Male Lead", "Inexperienced in Love",
                "Inferiority Complex", "Injury", "Inner Demon", "Innocent Female Lead", "Innocent Male Lead",
                "Insecurity", "Isekai", "Island", "Japan",
              "Jealousy", "Kidnapping", "Kind Female Lead", "Kind Male Lead", "Knight",
                "LGBT Parent", "LGBT Scenes", "Lifestyle Change", "Live-in Lover", "Loneliness", "Loner Protagonist", "Love at First Sight",

           "Love Confession", "Love Interests Who Don't Get Along", "Love Polygon", "Love Triangle", "Love-Hate Relationship",
                "Lust", "Mafia", "Magic", "Magic Schoo", "Magical Creature", "Magical Girl", "Magical Boy",
              "Maid", "Male Demographic with Female Lead", "Male Lead Falls in Love First", "Male Protagonist", "Female Protagonist",
                "Manipulation", "Manly Gay Couple", "Marriage", "Marriage Proposal", "Married Couple", "Masculine Uke", "Masochist",

           "Master-Pet Relationship", "Master-Servant Relationship", "Masturbation", "Mature Child", "Middle School", "Military", "Mind Break",
                "Misunderstanding", "Model", "Misunderstood Protagonist", "Models", "Modeling",

                 "Monster", "Multiple Couples", "Multiple Protagonists", "Murder", "Music", "Music Band",
                "Mythos", "Naive Female Lead", "Naive Male Lead", "Near-Death Experience", "Neighbor", "Netorare",
              "Netor", "Netori", "Ninja", "Nobility", "Nudity", "Obsessive Love", "Odd Situation", "Office Love",
                "Office Worker", "Older Brother", "Older Female Lead", "Older Female Younger Male",

           "Older Male Younger Female", "Older Seme Younger Uke", "Older Sister", "Older Uke Younger Seme",
                "Omegaverse", "Orphan", "Opposites Attract", "Otaku", "Outdoor Intercourse", "Paizuri", "Titty Fuck", "Panchira",
              "Parallel Universe", "Parental Abandonment", "Parody", "Part-Time Job", "Partial Nudity", "Past Plays a Big Role",
                "Persistent Seme", "Personality Change", "Pervert", "Perverted Female Lead", "Perverted Male Lead", "Photography", "Pirates",

           "Play or Die Situation", "Playboy", "Police", "Politics", "Politics Involving Royalty", "Popular Female Lead",
                "Popular Male Lead", "Possessive Lover", "Post-Secondary School", "Post-Secondary Student", "Post Apocalyptic", "Pregnancy",

                      "Pretend Lovers", "Priest", "Prince", "Princess", "Prostitute", "Prostitution",

              "Protagonist Strong from the Start", "Psychopath", "Public Sex", "Quirky Character", "Rape",
                "Rape Victim", "Regret", "Reader Interactive", "Reincarnated in Another World", "Reincarnation", "Restaurant", "Reunion",

           "Revenge", "Harem", "Rich Family", "Rescue", "Rich Female Lead", "Rich Male Lead", "Rich Kid",
                "Rivalry", "Romantic Subplot", "Roommates", "Royalty", "Robot",
              "Runaway", "Rushed Ending", "Sadist", "Salaryman", "Sadomasochism", "Samurai",
                "Scar", "School Boy", "School Gir", "School Intercourse", "Scientist","Science Fiction" , "Secret Identity",

           "Secret Organization", "Secret Relationship", "Secrets", "Seeing Things Other Humans Can't",
                "Old Classmate - Younger Classmate Relationship", "Serial Killer", "Studious Character",
                "Sex Addict", "Sex Friends Become Lovers", "Sex Toy", "Sex", "Sexual Abuse",

             "Sexual Assault", "Sexual Innuendo", "Shameless Female Lead", "Shinigami", "Short Chapter",
                "Short Female Lead", "Short Male Lead", "Showbiz", "Shy Female Lead", "Shy Protagonist", "Sibling", "Single Parent",

           "Sister Complex", "Sister", "Slaves", "Slow Romance", "Small Breasts", "Smart Female Lead", "Smart Male Lead"
           , "Smoking", "Social Hierarchy", "Social Outcast", "Special Ability", "Spirit",
              "Split Personality", "Stalker", "Stepsibling Love", "Stepsiblings", "Stolen Kiss",
                "Straight Seme", "Straight Uke", "Strategic Minds", "Strong Female Lead", "Strong Male Lead",
                "Student Council", "Student-Student Relationship",

           "Student-Teacher Relationship", "Student", "Subtle Romance", "Sudden Appearance", "Suicide", "Super Heroes",
                "Super Powers", "Survival", "Swimsuit", "Sword and Sorcery", "Swordplay", "Swordsman",

                "Swordswoman", "Talented Female Lead", "Talented Male Lead", "Tall Female Lead", "Tall Male Lead",
                "Tattoo", "Teacher", "Teamwork", "Threesome", "Time Skip", "Time Loop", "Time Travel",

             "Tomboy", "Torture", "Tournament", "Tragic Past", "Transfer Student", "Training", "Transformation", "Transgender",
                "Transgender Lead", "Transported to Another World", "LGBTQ", "Transgender Character",

           "Trap", "Traumatic Past", "Travel", "Tsundere", "Tsundere Female Lead", "Tsundere Male Lead",
                "Tutor", "Twins", "Undergarment", "Unexpected Feelings", "Unexpressed Feeling", "University Student",

              "Unorthodox Female Love Interest", "Unorthodox Male Love Interest", "Unpopular Protagonist", "Unrealistic Fighting",
                "Unrequited Love", "Unrequited Love Becomes Requited", "Urination", "Vampire", "Violence", "Virgin",
                "Villainous lead", "Virtual Reality",

           "Voyeurism", "War", "Weak Male Lead", "Weak to Strong", "Weak Female lead", "Web Comic", "Webtoon", "Werewolf",
                "Wishes", "Witch", "Workplace Romance", "World Travel",
                "Writer", "Yandere", "Youkai", "Young Female Lead", "Young Male Lead", "Younger Brother", "Younger Sister",
                "Zombies",
            };
            var GenreList = new List<TagModel>();
            for (int i = 0; i < Genres.Length; i++)
            {
                TagModel newgenre = new TagModel();
                newgenre.TagName = Genres[i];
                GenreList.Add(newgenre);
            }
            context.TagModels.AddRange(GenreList);
            context.SaveChanges();
        }

        #endregion TagSeed

        #region LanguageToOfficalWebsiteToBuy

        public void AddLangToOfficalWebsite()
        {
            #region Languges

            var JapaneseLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Japanese");
            var EnglishLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Japanese");
            var GermanLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "German");
            var FrenchLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "French");
            var RussianLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Russian");
            var PortogueseLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Portuguese");
            var KoreanLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Korean");
            var ArabicLanguage = context.Languages_.FirstOrDefault(e => e.LanguageName == "Arabic");

            #endregion Languges

            var SailorMoon = context.BuyPages.FirstOrDefault(e => e.BuyWebsite == "https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419");
            var SailorMoon2 = context.BuyPages.FirstOrDefault(e => e.BuyWebsite == "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093");

            SailorMoon.Languages = new List<Languages> { EnglishLanguage };
            SailorMoon2.Languages = new List<Languages> { JapaneseLanguage };
            context.Update(SailorMoon);
        }

        #endregion LanguageToOfficalWebsiteToBuy

        public async Task RoleCreating()
        {
            int rolesCount = roleManager.Roles.Count();
            // creat an array loop our array add all our roles to the database
            string[] roles = { "Admin", "NormalUser", "Owner", "AdminControl", "Updater",
                    "ShadowBanned", "Scanlators", "ForumAdmin", "Author", "Artist", "Publisher",
                    "Marketing", "SearchEngineOS", "FrontEndDeveloper", "SupportGroups", "BackEndDeveloper", "ProjectManager", "Tester" };
            if (rolesCount == 0)
            {
                for (int i = 0; i < roles.Length; i++)
                {
                    if (!await roleManager.RoleExistsAsync(roles[i]))
                    {
                        var role = new IdentityRole();
                        role.Name = roles[i];
                        await roleManager.CreateAsync(role);
                    }
                }
            }
            context.SaveChanges();
        }

        #endregion CommonSeed

        #region CreatingAuthors

        public void CreatingAuthors()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");

            var NewArtist = new ArtistModel
            {
                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. "
            ,
                MangaModels = new List<MangaModel> { manga1 },
                BirthPlace = "Japan",
                Gender = "Male",
                WorkingAt = "Shueisha",
                AssociatedNames = null
            };
            var newAuthor = new AuthorModel
            {
                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University."

              ,
                MangaModels = new List<MangaModel> { manga1 },
                BirthPlace = "Japan",
                Gender = "Male",
                WorkingAt = "Shueisha",
                AssociatedNames = null
            };
            var voiceActorModel = new VoiceActorModel
            {
                FirstName = "None",
                LastName = "LastResort",
                Biography = "Silent as death"
              ,
                MangaModels = new List<MangaModel> { manga1 },
                BirthPlace = "Space",
                Gender = "Void",
                WorkingAt = "Deaf Ears",
                AssociatedNames = null
            };

            context.artistModels.Add(NewArtist);
            context.authorModels.Add(newAuthor);
            context.voiceActorModels.Add(voiceActorModel);

            context.SaveChanges();
            NarutoCreatorsToManga();
        }

        #endregion CreatingAuthors

        #region SeedData

        public void CombineFullMetal()
        {
            CombineGenreTagFullMetal();
            //RelatedMangaFullMetal();
            //addLanguagesFullMetal();
            //CombineLangFullmetal();
            //CreatingAuthorsFullMetal();
        }

        public async Task seedData()
        {
            // ensure we have a database if the user forgetts to update-database
            context.Database.EnsureCreated();
            if (!context.mangaModels.Any())
            {
                //photopath needs fixing real bad
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage/0f3f5666-7cb9-4713-a779-f1e1546a0d5f");
                await RoleCreating();
                GenreSeed();
                TagsModel();
                addLanguages();

                #region TagsFor mangas

                var tag = context.TagModels.FirstOrDefault(e => e.TagName == "Pirates");
                var tag2 = context.TagModels.FirstOrDefault(e => e.TagName == "Hot-Blooded Male Lead");
                var tag3 = context.TagModels.FirstOrDefault(e => e.TagName == "Samurai");

                var tag7 = context.TagModels.FirstOrDefault(e => e.TagName == "Strong Female Lead");
                var tag8 = context.TagModels.FirstOrDefault(e => e.TagName == "Coming of Age");
                var tag9 = context.TagModels.FirstOrDefault(e => e.TagName == "Family");
                var Genre4 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Drama");
                var Genre5 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Slice of Life");
                var Genre6 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Comedy");
                var tagAt5 = context.TagModels.FirstOrDefault(e => e.TagName == "Absent Parent");
                var tagAt6 = context.TagModels.FirstOrDefault(e => e.TagName == "Drama");
                var GenreAt2 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Fantasy");
                var GenreAt3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Science Fiction");
                var GenreCon4 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Mystery");
                var GenreCon5 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Detective");
                var tagCon5 = context.TagModels.FirstOrDefault(e => e.TagName == "Secret Identity");
                var tagCon6 = context.TagModels.FirstOrDefault(e => e.TagName == "Murder");
                var Genre = context.GenresModels.FirstOrDefault(e => e.GenreName == "Adventure");
                var Genre2 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Action");
                var Genre3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Fantasy");
                var tag4 = context.TagModels.FirstOrDefault(e => e.TagName == "Arrogant Male Lead");
                var tag5 = context.TagModels.FirstOrDefault(e => e.TagName == "Insecurity");
                var tag6 = context.TagModels.FirstOrDefault(e => e.TagName == "Revenge");

                #endregion TagsFor mangas

                // gives our manga an datetime.
                DateTime myDate = DateTime.ParseExact("1991-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
                // creates a user,
                var user = new UserModel { UserName = "TestUSer", Email = "TestUser@hotmail.com", userPhotoPath = filePath };
                // create a "manga actually" but we use MasterModel to bind our users to a manga.

                MangaModel MangaNaruto = new MangaModel
                {
                    MangaName = "Naruto",

                    PhotoPath = "C:\\Users\\Rojhat\\source\\repos\\NovelXManga\\NovelXManga\\wwwroot\\Images\\AuthorImage\\Rebecca Chambers.png",
                    ReleaseYear = myDate,
                    BlogModel = new BlogModel { mangaName = "Naruto" },
                    Description = "A Kid who got something stuck in his stomach",
                    CompletelyTranslated = "Completed",
                    OfficalLanguage = "Japanese",
                    ISBN10 = "1569319006",
                    ISBN13 = "978-1569319000",
                    score = 10,

                    EndingYear = DateTime.Now,
                    StatusInCountryOfOrigin = "11, completed",
                    Type = "Manga",
                    OriginalPublisher = "SquareEnix",
                    orignalWebtoon = "N/A",
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };
                MangaModel MangaBerserk = new MangaModel
                {
                    MangaName = "Berserk",

                    PhotoPath = "C:\\Users\\Rojhat\\source\\repos\\NovelXManga\\NovelXManga\\wwwroot\\Images\\AuthorImage\\NoPhoto.png",
                    ReleaseYear = DateTime.Now,
                    BlogModel = new BlogModel { mangaName = "Berserk" },
                    Description = "A broken man",
                    CompletelyTranslated = "Ongoing",
                    ISBN10 = "1506727549",
                    ISBN13 = "978-1506717913",
                    score = 10,
                    futureEvents = "Near DeathBall",
                    EndingYear = DateTime.Now,
                    StatusInCountryOfOrigin = "11, completed",
                    Type = "Manga",
                    OriginalPublisher = "SquareEnix",
                    orignalWebtoon = "N/A",
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };

                #region ChatGptData

                var Japanese = context.Languages_.FirstOrDefaultAsync(e => e.LanguageName == "Japanese");
                MangaModel fullmetalAlchemist = new MangaModel
                {
                    MangaName = "Fullmetal Alchemist",
                    BlogModel = new BlogModel { mangaName = "Fullmetal Alchemist" },
                    ISBN10 = "2345678901",
                    ISBN13 = "2345678901234",
                    futureEvents = "None",
                    StatusInCountryOfOrigin = "Completed",
                    CompletelyTranslated = "Yes",
                    OriginalPublisher = "Square Enix",
                    score = 9.0,
                    Type = "Manga",
                    OfficalLanguage = "Japanese",
                    PhotoPath = "fullmetalAlchemist.jpg",
                    Description = "Two brothers use alchemy to revive their deceased mother, leading them on a journey to find the Philosopher's Stone and restore their bodies to their original form.",
                    ReleaseYear = new DateTime(2001, 1, 2),
                    EndingYear = new DateTime(2010, 10, 2),
                    AssociatedNames = new List<AssociatedNames>
                {
                new AssociatedNames { nameString = "FullMetal Alchemist Comics"  },
                new AssociatedNames { nameString = "Full Metal Alchemist Manga" } ,
                },

                    OfficalWebsites = new List<OfficalWebsite>
                {
                new OfficalWebsite { OfficalWebsiteString = "www.fullmetalAlchemist.com", OfficalWebsiteName = "FullmetalAlchemist.Com"},
                new OfficalWebsite { OfficalWebsiteString = "fullMetalAlchemist.jp", OfficalWebsiteName = "FullmetalAlchemist.jp" },
                },
                    Authormodels = new List<AuthorModel>
                {
                new AuthorModel { FirstName = "Hiromu Arakawa" },
                },
                    ArtistModels = new List<ArtistModel>
                {
                new ArtistModel { FirstName = "Hiromu Arakawa" },
                },
                    VoiceActors = new List<VoiceActorModel>
                {
                new VoiceActorModel { FirstName = "Romi Park" },
                new VoiceActorModel { FirstName = "Rie Kugimiya" },
                new VoiceActorModel { FirstName = "Vic Mignogna" },
                },
                    GenresModels = new List<GenresModel>
                {
              Genre, Genre2, Genre3
                },
                    TagsModels = new List<TagModel>
                {
              tag4, tag5, tag6
                },
                    Characters = new List<Character>
                {
                new Character { CharacterName = "Edward Elric" },
                new Character { CharacterName = "Alphonse Elric" },
                new Character { CharacterName = "Winry Rockbell" },
                new Character { CharacterName = "Roy Mustang" },
                new Character { CharacterName = "Riza Hawkeye" },
                new Character { CharacterName = "Maes Hughes" },
                },
                };
                MangaModel onePiece = new MangaModel
                {
                    MangaName = "One Piece",
                    BlogModel = new BlogModel { mangaName = "One Piece" },
                    ISBN10 = "1234567890",
                    ISBN13 = "1234567890123",
                    futureEvents = "None",
                    StatusInCountryOfOrigin = "Ongoing",
                    CompletelyTranslated = "No",
                    OriginalPublisher = "Shueisha",
                    score = 9.5,
                    Type = "Manga",
                    OfficalLanguage = "Japanese",
                    PhotoPath = "onepiece.jpg",
                    Description = "Follow the adventures of Monkey D. Luffy and the Straw Hat Pirates on their journey to become the King of the Pirates.",
                    ReleaseYear = new DateTime(1997, 7, 19),
                    EndingYear = null,
                    AssociatedNames = new List<AssociatedNames>
                    {
                        new AssociatedNames { nameString = "One Piece Comics" },
                        new AssociatedNames { nameString = "One Piece Manga" },
                    },

                    OfficalWebsites = new List<OfficalWebsite>
                    {
                        new OfficalWebsite { OfficalWebsiteString = "www.onepiece.com", OfficalWebsiteName = "onepiece.com", },
                        new OfficalWebsite { OfficalWebsiteString = "onepiece.jp", OfficalWebsiteName = "onepiece.jp", },
                    },

                    Authormodels = new List<AuthorModel>
                    {
                        new AuthorModel { FirstName = "Eiichiro Oda" },
                    },
                    BuyPages = new List<BuyPage>
                    {
                        new BuyPage { BuyWebsite ="https://www.amazon.com/One-Piece-Vol-Romance-Dawn/dp/1569319014/ref=sr_1_3?keywords=Manga+One+Piece&qid=1675256565&sr=8-3"},
                        new BuyPage {  BuyWebsite = "https://www.amazon.com/One-Piece-Vol-1-Japanese/dp/4088725093", _Languages = new List<Languages>{ Japanese } }
                    },
                    ArtistModels = new List<ArtistModel>
                    {
                        new ArtistModel { FirstName = "Eiichiro Oda" },
                    },
                    VoiceActors = new List<VoiceActorModel>
                    {
                        new VoiceActorModel { FirstName = "Mayumi Tanaka" },
                        new VoiceActorModel { FirstName = "Akemi Okamura" },
                        new VoiceActorModel { FirstName = "Kazuya Nakai" },
                    },

                    TagsModels = new List<TagModel>
                    {
                        tag, tag2, tag3
                    },

                    GenresModels = new List<GenresModel>
                    {
                        Genre, Genre2, Genre3
                    },
                    Characters = new List<Character>
                    {
                        new Character { CharacterName = "Monkey D. Luffy" },
                        new Character { CharacterName = "Roronoa Zoro" },
                        new Character { CharacterName = "Nami" },
                        new Character { CharacterName = "Usopp" },
                        new Character { CharacterName = "Sanji" },
                        new Character { CharacterName = "Tony Tony Chopper" },
                        new Character { CharacterName = "Nico Robin" },
                        new Character { CharacterName = "Franky" },
                        new Character { CharacterName = "Brook" },
                    },
                };
                MangaModel AttackOnTitan = new MangaModel
                {
                    MangaName = "Attack on Titan",
                    BlogModel = new BlogModel { mangaName = "Attack on Titan" },
                    ISBN10 = "2345678901",
                    ISBN13 = "2345678901234",
                    futureEvents = "None",
                    StatusInCountryOfOrigin = "Completed",
                    CompletelyTranslated = "Yes",
                    OriginalPublisher = "Production I.G",
                    score = 8.5,
                    Type = "Anime",
                    OfficalLanguage = "Japanese",
                    PhotoPath = "AttackOnTitan.jpg",
                    Description = "The story follows Eren Jeager and Mikasa Ackerman as they join the Scout Regiment to fight against the Titans who threaten their city and the rest of humanity.",
                    ReleaseYear = new DateTime(2013, 4, 7),
                    EndingYear = new DateTime(2013, 9, 29),
                    AssociatedNames = new List<AssociatedNames>
{
new AssociatedNames { nameString = "Attack on Titan TV Show" },
new AssociatedNames { nameString = "Attack on Titan Anime" },
},

                    OfficalWebsites = new List<OfficalWebsite>
{
new OfficalWebsite { OfficalWebsiteString = "www.AttackOnTitan.com", OfficalWebsiteName = "AttackOnTitan.Com" },
new OfficalWebsite { OfficalWebsiteString = "AttackOnTitan.jp", OfficalWebsiteName = "AttackOnTitan.jp" },
},
                    Authormodels = new List<AuthorModel>
{
new AuthorModel { FirstName = "Hajime Isayama" },
},
                    ArtistModels = new List<ArtistModel>
{
new ArtistModel { FirstName = "Hajime Isayama" },
},
                    VoiceActors = new List<VoiceActorModel>
{
new VoiceActorModel { FirstName = "Yuki Kaji" },
new VoiceActorModel { FirstName = "Yui Ishikawa" },
new VoiceActorModel { FirstName = "Marina Inoue" },
},
                    GenresModels = new List<GenresModel>
{
GenreAt2, GenreAt3
},
                    TagsModels = new List<TagModel>
{
tagAt5, tagAt6
},
                    Characters = new List<Character>
{
new Character { CharacterName = "Eren Jeager" },
new Character { CharacterName = "Mikasa Ackerman" },
},
                };
                MangaModel SailorMoon = new MangaModel
                {
                    MangaName = "Sailor Moon",
                    BlogModel = new BlogModel { mangaName = "Sailor Moon" },
                    ISBN10 = "0123456789",
                    ISBN13 = "0123456789012",
                    futureEvents = "None",
                    StatusInCountryOfOrigin = "Completed",
                    CompletelyTranslated = "Yes",
                    OriginalPublisher = "Kodansha",
                    score = 8.0,
                    Type = "Manga",
                    OfficalLanguage = "Japanese",
                    PhotoPath = "sailorMoon.jpg",
                    Description = "A young girl named Usagi transforms into the titular character Sailor Moon, a guardian who fights for love and justice, and battles against evil forces to protect the universe.",
                    ReleaseYear = new DateTime(1991, 3, 10),
                    EndingYear = new DateTime(1997, 2, 1),
                    AssociatedNames = new List<AssociatedNames>
            {
            new AssociatedNames { nameString = "Bishoujo Senshi Sailor Moon" },
            new AssociatedNames { nameString = "Pretty Soldier Sailor Moon" },
            },

                    OfficalWebsites = new List<OfficalWebsite>
            {
            new OfficalWebsite { OfficalWebsiteString = "www.sailormoon.com", OfficalWebsiteName = "SailorMoon.Com",  OfficalWebSiteToBuy = "https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419"},
            new OfficalWebsite { OfficalWebsiteString = "sailormoon.jp", OfficalWebsiteName = "SailorMoon.jp", OfficalWebSiteToBuy = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093"},
            },
                    Authormodels = new List<AuthorModel>
            {
            new AuthorModel { FirstName = "Naoko Takeuchi" },
            },
                    ArtistModels = new List<ArtistModel>
            {
            new ArtistModel { FirstName = "Naoko Takeuchi" },
            },
                    VoiceActors = new List<VoiceActorModel>
            {
            new VoiceActorModel { FirstName = "Kotono Mitsuishi" },
            new VoiceActorModel { FirstName = "Aya Hisakawa" },
            new VoiceActorModel { FirstName = "Emi Shinohara" },
            },
                    GenresModels = new List<GenresModel>
            {
          Genre4, Genre5, Genre6
            },
                    TagsModels = new List<TagModel>
                    {
                        tag7,tag8,tag9
                    },
                    Characters = new List<Character>
                    {
                        new Character { CharacterName = "Sailor Jupter" },
                        new Character { CharacterName = "Sailor Moon" },
                        new Character { CharacterName = "Sailor Mercury" },
                        new Character { CharacterName = "Sailor Mars" },
                        new Character { CharacterName = "Sanji Venus" },
                    },
                };
                MangaModel DetectiveConan = new MangaModel
                {
                    MangaName = "Detective Conan",
                    BlogModel = new BlogModel { mangaName = "Detective Conan" },
                    ISBN10 = "3456789012",
                    ISBN13 = "3456789012345",
                    futureEvents = "None",
                    StatusInCountryOfOrigin = "Ongoing",
                    CompletelyTranslated = "No",
                    OriginalPublisher = "Shogakukan",
                    score = 8.5,
                    Type = "Manga",
                    OfficalLanguage = "Japanese",
                    PhotoPath = "detectiveConan.jpg",
                    Description = "A high school detective who is transformed into a child after being poisoned, solves crimes under a different identity while searching for a cure.",
                    ReleaseYear = new DateTime(1994, 1, 2),
                    EndingYear = new DateTime(),
                    AssociatedNames = new List<AssociatedNames>
{
new AssociatedNames { nameString = "Detective Conan Comics" },
new AssociatedNames { nameString = "Detective Conan Manga" },
},

                    OfficalWebsites = new List<OfficalWebsite>
            {
            new OfficalWebsite { OfficalWebsiteString = "www.detectiveConan.com", OfficalWebsiteName = "DetectiveConan.Com"},
            new OfficalWebsite { OfficalWebsiteString = "detectiveConan.jp", OfficalWebsiteName = "DetectiveConan.jp" },
            },
                    Authormodels = new List<AuthorModel>
            {
            new AuthorModel { FirstName = "Gosho Aoyama" },
            },
                    ArtistModels = new List<ArtistModel>
            {
            new ArtistModel { FirstName = "Gosho Aoyama" },
            },
                    VoiceActors = new List<VoiceActorModel>
            {
            new VoiceActorModel { FirstName = "Minami Takayama" },
            new VoiceActorModel { FirstName = "Wakana Yamazaki" },
            new VoiceActorModel { FirstName = "Rikiya Koyama" },
            },
                    GenresModels = new List<GenresModel>
            {
          GenreCon4, GenreCon5
            },
                    TagsModels = new List<TagModel>
            {
          tagCon5, tagCon6 },
                    Characters = new List<Character>
                {
                new Character { CharacterName = "Ai Haibara" },
                new Character { CharacterName = "Hiroshi Agasa" },
                new Character { CharacterName = "Richard Moore" },
                new Character { CharacterName = "Kogoro Mouri" },
                new Character { CharacterName = "Shinichi Kudo" },
                },
                };

                context.mangaModels.Add(DetectiveConan);
                context.mangaModels.Add(SailorMoon);
                context.mangaModels.Add(onePiece);
                context.mangaModels.Add(fullmetalAlchemist);
                context.mangaModels.Add(onePiece);

                #endregion ChatGptData

                context.mangaModels.Add(MangaNaruto);
                context.mangaModels.Add(MangaBerserk);

                context.SaveChanges();

                CombineBerserkTagsGenres();
                RelatedManga();
                BerserkLanguageToMagnga();
                CombineFullMetal();
                var result = await userManager.CreateAsync(user, "Rojhat123!");
                if (result.Succeeded)
                { // adds a role owner to the testobject
                    var resultRole = await userManager.AddToRoleAsync(user, "Owner");
                }
                CreatingAuthors();
            }
        }

        #endregion SeedData

        public void addLanguages()
        {
            string[] LanguagesInput = { "English", "Japanese", "Spanish", "Chinese", "Russian", "French", "Arabic", "Korean", "Hindi", "Bengali", "Portuguese", "Indonesian", "Urdu", };
            string[] OfficalWeb = { "https://www.amazon.com/Naruto-Vol-1-Uzumaki/dp/1569319006/ref=sr_1_2?keywords=Naruto+Manga&qid=1673876360&sr=8-2",
                "https://www.amazon.com/Naruto-1-Japanese-Masashi-Kishimoto/dp/4088728408/ref=sr_1_1?crid=3UV6OU93CQ4G2&keywords=Naruto+Manga+Japanese&qid=1673876516&sprefix=naruto+manga+japane%2Caps%2C191&sr=8-1",
                "https://www.amazon.com/Naruto-1-Masashi-Kishimoto/dp/8415821816/ref=sr_1_2?crid=14B33YWBQMXRP&keywords=Naruto+Manga+Spanish&qid=1673876488&sprefix=naruto+manga+spanish%2Caps%2C193&sr=8-2",
                "https://www.abebooks.com/servlet/BookDetailsPL?bi=4678761458&searchurl=kn%3DNaruto%2B1%2B%2528Chinese%2BEdition%2529%26sortby%3D20&cm_sp=snippet-_-srp1-_-title3",
            "https://comics.lv/en/product/naruto-box-set-1-volumes-1-27/"};
            var LangList = new List<Languages>();
            for (int i = 0; i < LanguagesInput.Length; i++)
            {
                Languages newLang = new Languages();

                newLang.LanguageName = LanguagesInput[i];
                LangList.Add(newLang);
            }
            context.Languages_.AddRange(LangList);
            context.SaveChanges();
        }

        #region AddingBerserk

        public void CombineBerserkTagsGenres()
        {
            var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var Tag1ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Antihero");
            var Tag2Tomanga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            var GenToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            GenToManga2.MangaModels = new List<MangaModel> { manga2 };
            GenToManga2.MangaID = manga2.MangaID;
            Tag2Tomanga2.MangaModels = new List<MangaModel> { manga2 };
            Tag2Tomanga2.MangaID = manga2.MangaID;
            Tag1ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Tag1ToManga2.MangaID = manga2.MangaID;
            Gen2ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Gen2ToManga2.MangaID = manga2.MangaID;
            GenToManga2.MangaModels = new List<MangaModel> { manga2 };
            GenToManga2.MangaID = manga2.MangaID;

            manga2.TagsModels = new List<TagModel> { Tag1ToManga2, Tag2Tomanga2 };
            manga2.GenresModels = new List<GenresModel> { GenToManga2, Gen2ToManga2 };

            context.TagModels.Update(Tag1ToManga2);
            context.TagModels.Update(Tag2Tomanga2);

            context.GenresModels.Update(GenToManga2);
            context.GenresModels.Update(Gen2ToManga2);

            context.mangaModels.Update(manga2);

            context.SaveChanges();
        }

        public void BerserkRelatedManga()
        {
        }

        public void BerserkLanguageToMagnga()
        {
            var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var Lang3ToManga2 = context.Languages_.Include(e => e.MangaModels).FirstOrDefault(s => s.LanguageName == "Chinese");
            var Lang4ToManga2 = context.Languages_.Include(e => e.MangaModels).FirstOrDefault(s => s.LanguageName == "Russian");
            Lang3ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Lang3ToManga2.MangaID = manga2.MangaID;
            Lang4ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Lang4ToManga2.MangaID = manga2.MangaID;
            manga2.AllLanguages = new List<Languages> { Lang3ToManga2, Lang4ToManga2 };

            context.Languages_.Update(Lang3ToManga2);
            context.Languages_.Update(Lang4ToManga2);

            context.mangaModels.Update(manga2);
            context.SaveChanges();
        }

        #endregion AddingBerserk

        #region AddFullmetal

        public void CombineGenreTagFullMetal()
        {
            var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Fullmetal Alchemist");
            var Tag1ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Science Fiction");
            var Tag2Tomanga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            var Gen1ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var Tag2ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            Tag1ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Tag1ToManga2.MangaID = manga2.MangaID;
            Tag2ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Tag2ToManga2.MangaID = manga2.MangaID;
            Gen1ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Gen1ToManga2.MangaID = manga2.MangaID;
            Gen2ToManga2.MangaModels = new List<MangaModel> { manga2 };
            Gen2ToManga2.MangaID = manga2.MangaID;
            context.TagModels.Update(Tag1ToManga2);
            context.TagModels.Update(Tag2ToManga2);
            context.GenresModels.Update(Gen1ToManga2);
            context.GenresModels.Update(Gen2ToManga2);
            context.SaveChanges();
        }

        #endregion AddFullmetal

        #region AddingNaruto

        public void CombineOfficalWebsites()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var officalWeb = context.OfficalWebsites.FirstOrDefault(e => e.OfficalWebsiteName == "Naruto-Official");

            manga1.OfficalWebsites = new List<OfficalWebsite> { officalWeb };

            context.mangaModels.Update(manga1);
            context.SaveChanges();
            //second
        }

        public void NarutoLanguageToManga()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var Lang1ToManga1 = context.Languages_.Include(e => e.MangaModels).FirstOrDefault(s => s.LanguageName == "English");
            var Lang2ToManga1 = context.Languages_.Include(e => e.MangaModels).FirstOrDefault(s => s.LanguageName == "Japanese");
            Lang1ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Lang1ToManga1.MangaID = manga1.MangaID;
            Lang2ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Lang2ToManga1.MangaID = manga1.MangaID;
            manga1.AllLanguages = new List<Languages> { Lang1ToManga1, Lang2ToManga1 };
            context.Languages_.Update(Lang1ToManga1);
            context.Languages_.Update(Lang2ToManga1);
            context.mangaModels.Update(manga1);
            context.SaveChanges();
        }

        public void NarutoOfficalWebs()
        {
            OfficalWebsite offWeb = new OfficalWebsite();
            offWeb.OfficalWebsiteName = "Naruto-Official";
            offWeb.OfficalWebsiteString = "https://naruto-official.com/en";
            offWeb.Twitter = "https://mobile.twitter.com/naruto_info_en";
            offWeb.Facebook = "https://www.facebook.com/narutoofficialsns/";
            offWeb.Line = "https://social-plugins.line.me/lineit/share?url=https://naruto-official.com/en";

            context.OfficalWebsites.AddRange(offWeb);
            context.SaveChanges();
            //first
        }

        public void AddassociatedNamesToManga()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var assName1 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naluto");
            var assName2 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto S");
            var assName3 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto Shippuden");
            manga1.AssociatedNames = new List<AssociatedNames> { assName1, assName2, assName3 };
            context.mangaModels.Update(manga1);
            context.SaveChanges();
        }

        public void NarutoCreatorsToManga()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var artist = context.artistModels.FirstOrDefault(e => e.FirstName == "Masashi");
            var author = context.authorModels.FirstOrDefault(e => e.FirstName == "Masashi");
            var voice = context.voiceActorModels.FirstOrDefault(e => e.FirstName == "None");
            manga1.Authormodels = new List<AuthorModel> { author };
            manga1.ArtistModels = new List<ArtistModel> { artist };
            manga1.VoiceActors = new List<VoiceActorModel> { voice };
            context.mangaModels.Update(manga1);
            context.SaveChanges();
            NarutoAssociatedNamesToManga();
        }

        public void NarutoAssociatedNamesToManga()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");

            var artist = context.artistModels.FirstOrDefault(e => e.FirstName == "Masashi");
            var author = context.authorModels.FirstOrDefault(e => e.FirstName == "Masashi");
            AssociatedNames associatedNames = new AssociatedNames();
            associatedNames.nameString = "Naluto";
            associatedNames.MangaID = manga1.MangaID;
            associatedNames.mangaModel = manga1;
            associatedNames.ArtistId = artist.ArtistId;
            associatedNames.ArtistModel = artist;
            AssociatedNames associatedNames2 = new AssociatedNames();
            associatedNames2.nameString = "Naruto S";
            associatedNames2.MangaID = manga1.MangaID;
            associatedNames2.mangaModel = manga1;
            associatedNames2.ArtistId = artist.ArtistId;
            associatedNames2.ArtistModel = artist;
            AssociatedNames associatedNames3 = new AssociatedNames();
            associatedNames3.nameString = "Naruto Shippuden";
            associatedNames3.MangaID = manga1.MangaID;
            associatedNames3.mangaModel = manga1;
            associatedNames3.ArtistId = artist.ArtistId;
            associatedNames3.ArtistModel = artist;

            context.AssociatedNames.Add(associatedNames);
            context.AssociatedNames.Add(associatedNames2);
            context.AssociatedNames.Add(associatedNames3);
            context.SaveChanges();
            AddassociatedNamesToManga();
        }

        public void CombineNarutoGenres()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");

            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Ninja");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");

            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            Tag1ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Tag1ToManga1.MangaID = manga1.MangaID;
            Tag2ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Tag2ToManga1.MangaID = manga1.MangaID;
            Gen1ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Gen1ToManga1.MangaID = manga1.MangaID;
            Gen2ToManga1.MangaModels = new List<MangaModel> { manga1 };
            Gen2ToManga1.MangaID = manga1.MangaID;
            manga1.TagsModels = new List<TagModel> { Tag1ToManga1, Tag2ToManga1 };
            manga1.GenresModels = new List<GenresModel> { Gen1ToManga1, Gen2ToManga1 };
            context.TagModels.Update(Tag1ToManga1);
            context.TagModels.Update(Tag2ToManga1);
            context.GenresModels.Update(Gen1ToManga1);
            context.GenresModels.Update(Gen2ToManga1);
            context.mangaModels.Update(manga1);
        }

        public void NarutoLanguagesToBuy()
        {
            var Naruto = context.BuyPages.FirstOrDefault(e => e.BuyWebsite == " ");
        }

        #endregion AddingNaruto
    }
}