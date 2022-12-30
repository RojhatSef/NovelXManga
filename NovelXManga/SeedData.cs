
using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;

namespace NovelXManga
{
    public class SeedData
    {
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
        #region CombiningAssociatedNamesToBook
        public void addAssName()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var assName1 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naluto");
            var assName2 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto S");
            var assName3 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto Shippuden");
            manga1.AssociatedNames = new List<AssociatedNames> { assName1, assName2, assName3 };
            context.mangaModels.Update(manga1);
            context.SaveChanges();

        }
        #endregion
        #region AddingassociatedNamesToDatabase
        public void names()
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

            //AssociatedNames associatedNames3 = new AssociatedNames();
            //associatedNames3.nameString = "Black Swordsman";
            //manga2.AssociatedNames = new List<AssociatedNames> { associatedNames3 };

            context.AssociatedNames.Add(associatedNames);
            context.AssociatedNames.Add(associatedNames2);
            context.AssociatedNames.Add(associatedNames3);
            context.SaveChanges();
            addAssName();

        }
        #endregion
        #region addAuthorToManga
        public void addAuthorToManga()
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
            names();
        }
        #endregion
        #region CreatingAuthors
        public void CreatingAuthors()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");


            var NewArtist = new ArtistModel
            {

                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
            ,
                MangaID = manga1.MangaID,
                mangaModel = manga1,
                BirthPlace = "Japan",
                Gender = "Male",
                WorkingAt = "Shueisha",
                AssociatedNames = null
            };
            var newAuthor = new AuthorModel
            {
                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
              ,
                mangaModel = manga1,
                MangaID = manga1.MangaID,
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
                mangaModel = manga1,
                MangaID = manga1.MangaID,
                BirthPlace = "Space",
                Gender = "Void",
                WorkingAt = "Deaf Ears",
                AssociatedNames = null
            };

            context.artistModels.Add(NewArtist);
            context.authorModels.Add(newAuthor);
            context.voiceActorModels.Add(voiceActorModel);

            context.SaveChanges();
            addAuthorToManga();
        }
        #endregion
        #region GenreSeeding
        public void GenreSeed()
        {
            string[] Genres = { "None", "Action", "Adult", "Adult", "Adventure", "Comedy", "Drama", "Ecchi", "Fantasy", "Gender Bender", "Harem", "Historical",
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

        }
        #endregion
        #region SeedData
        public async Task seedData()
        {
            context.Database.EnsureCreated();
            if (!context.mangaModels.Any())
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage/0f3f5666-7cb9-4713-a779-f1e1546a0d5f");
                int rolesCount = roleManager.Roles.Count();
                // creat an array loop our array add all our roles to the database
                string[] roles = { "Admin", "NormalUser", "Owner", "AdminControl", "Updater", "ShadowBanned", "Scanlators", "ForumAdmin", "Author", "Artist", "Publisher", "Marketing", "SearchEngineOS", "FrontEndDeveloper", "SupportGroups", "BackEndDeveloper", "ProjectManager", "Tester" };
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
                DateTime myDate = DateTime.ParseExact("1991-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

                var user = new UserModel { UserName = "TestUSer", Email = "TestUser@hotmail.com", userPhotoPath = filePath };
                MasterModel masterModel = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Naruto",

                        PhotoPath = "https://cdn.mangaupdates.com/image/i140134.png",
                        ReleaseYear = myDate,
                        BlogModel = new BlogModel { mangaName = "Naruto" },
                        OfficialTranslations = "Japanese",
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

                    },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };

                MasterModel masterModel2 = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Berserk",

                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Berserk" },
                        OfficialTranslations = "Japanese",
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
                    },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };

                context.MasterModels.Add(masterModel);
                context.MasterModels.Add(masterModel2);
                GenreSeed();
                TagsModel();
                context.SaveChanges();
                CombineGenreTag();
                RelatedManga();

                var result = await userManager.CreateAsync(user, "Rojhat123!");
                if (result.Succeeded)
                { // adds a role owner to the testobject
                    var resultRole = await userManager.AddToRoleAsync(user, "Owner");
                }
                CreatingAuthors();
            }

        }
        public void CombineGenreTag()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var tags = context.TagModels.FirstOrDefault(t => t.TagName == "Ninja");
            var tagss = context.TagModels.FirstOrDefault(t => t.TagName == "Ninja");
            tags.MangaModels = new List<MangaModel> { manga1 };
            tags.MangaID = manga1.MangaID;
            tagss.MangaModels = new List<MangaModel> { manga2 };
            tagss.MangaID = manga2.MangaID;
            var tags4 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            tags4.MangaModels = new List<MangaModel> { manga1 };
            tags4.MangaID = manga1.MangaID;
            var tags44 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            tags44.MangaModels = new List<MangaModel> { manga2 };
            tags44.MangaID = manga2.MangaID;
            var gen = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            gen.MangaModels = new List<MangaModel> { manga1 };
            gen.MangaID = manga1.MangaID;
            var gens = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            gens.MangaModels = new List<MangaModel> { manga2 };
            gens.MangaID = manga1.MangaID;
            var gen2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            gen2.MangaModels = new List<MangaModel> { manga1 };
            gen2.MangaID = manga1.MangaID;
            var gen22 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            gen22.MangaModels = new List<MangaModel> { manga2 };
            gen22.MangaID = manga1.MangaID;

            manga1.TagsModels = new List<TagModel> { tags, tags4 };
            manga1.GenresModels = new List<GenresModel> { gen, gen2 };
            manga2.TagsModels = new List<TagModel> { tagss, tags44 };
            manga2.GenresModels = new List<GenresModel> { gens, gen2 };

            context.mangaModels.Update(manga1);
            context.mangaModels.Update(manga2);

            context.SaveChanges();
        }
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
        #endregion
        #region TagSeed
        public void TagsModel()
        {


            string[] Genres = { "None", "21st century", "4-koma/Yonkoma", "Abandoned Children", "Absent Parent",

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
                "Emotionally Weak Female Lead", "None", "None", "None", "None", "None",

                 "Emotionally Strong Male Lead", "Emotionally Weak Male Lead", "Enemies Become Friends", "Episodic", "ESP",
                "European Ambience", "Exhibitionism", "Exorcism", "Experiments", "Expressionless Protagonist", "Extended Flashbacks", "Eyepatch",

                  "Family", "Family Drama", "Fantasy World", "Fast Romance", "Father and Daughter", "Fellatio",

                "Female Demographic with Male Lead", "Female Dominance", "Female Fighters", "Female Lead Falls in Love First",
                "Fetishes", "Finding Love Again", "First Kiss", "First Love", "First-Time Intercourse", "Flashbacks", "Food", "Forbidden Love",

           "Foreigners", "Friends Become Enemies", "Friends Become Lovers", "Friends Grow Distant",
                "Friendship", "Full Color", "Futanari", "Future", "Game Elements", "GameLit", "Games", "None",
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
                "Persistent Seme", "Personality Change", "Pervert", "Perverted Female Lead", "Perverted Male Lead", "Photography",

           "Play or Die Situation", "Playboy", "Police", "Politics", "Politics Involving Royalty", "Popular Female Lead",
                "Popular Male Lead", "Possessive Lover", "Post-Secondary School", "Post-Secondary Student", "Post Apocalyptic", "Pregnancy",

                      "Pretend Lovers", "Priest", "Prince", "Princess", "Prostitute", "Prostitution",

              "Protagonist Strong from the Start", "Psychopath", "Public Sex", "Quirky Character", "Rape",
                "Rape Victim", "Regret", "Reader Interactive", "Reincarnated in Another World", "Reincarnation", "Restaurant", "Reunion",

           "Revenge", "Harem", "Rich Family", "Rescue", "Rich Female Lead", "Rich Male Lead", "Rich Kid",
                "Rivalry", "Romantic Subplot", "Roommates", "Royalty", "Robot",
              "Runaway", "Rushed Ending", "Sadist", "Salaryman", "Sadomasochism", "Samurai",
                "Scar", "School Boy", "School Gir", "School Intercourse", "Scientist", "Secret Identity",

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
        }
        #endregion SeedTaskEnd
    }
}
