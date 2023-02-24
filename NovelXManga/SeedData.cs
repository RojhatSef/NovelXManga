using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

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

        public Languages JapaneseLanguage { get; set; }
        public Languages SpanishLanguage { get; set; }

        public Languages ChineseLanguage { get; set; }
        public Languages EnglishLanguage { get; set; }
        public Languages KoreanLanguage { get; set; }
        public Languages RussianLanguage { get; set; }
        public Languages FrenchLanguage { get; set; }
        public string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "MangaImage");

        public void Tags_Genres_Languages()
        {
            JapaneseLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "Japanese");
            EnglishLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "English");
            ChineseLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "Chinese");
            SpanishLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "Spanish");
            RussianLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "Russian");
            KoreanLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "Korean");
            FrenchLanguage = context.Languages_.FirstOrDefault(l => l.LanguageName == "French");
        }

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

        private string ProcessUploadedFile(string filename)
        {
            string uniqueFileName = null;
            if (filename != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage");
                string extension = Path.GetExtension(filename);
                uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                File.Copy(Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", "MangaImage", filename), filePath);
            }
            return uniqueFileName;
        }

        public void addLanguages()
        {
            string[] LanguagesInput = { "English", "Japanese", "Spanish", "Chinese", "Russian", "French", "Arabic", "Korean", "Hindi", "Bengali", "Portuguese", "Indonesian", "Urdu", };

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

        #region GenreSeeding

        public void GenreSeed()
        {
            string[] Genres = {  "Action", "Adult", "Adventure", "Comedy", "Drama", "Ecchi", "Fantasy", "Gender Bender", "Historical",
                "Classics", "Comic", "Graphic Novel", "Detective", "Horror", "Josei", "Martial Arts", "Mature", "Mecha", "Mystery", "Psychological", "Romance",
                "School Life", "Seinen", "Shoujo", "Shoujo Ai", "Shounen", "Shounen Ai", "Slice Of Life", "Smut",  "Sports",
            "Supernatural","Thriller", "Tragedy", "Wuxia", "Xianxia", "Xuanhuan", "Yaoi / Gay ", "Yuri / Lesbian", "Ecchi", "Fantasy", "Gender Bender", "Harem", "Historical",
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
                "Cunnilingus", "Curse", "Dark Ambience", "Dark Fantasy",

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
              "Jealousy", "Kidnapping", "Kind Female Lead", "Kind Male Lead", "Knight", "Level System",
                "LGBT Parent", "LGBT Scenes", "Lifestyle Change", "Live-in Lover", "Loneliness", "Loner Protagonist", "Love at First Sight",

           "Love Confession", "Love Interests Who Don't Get Along", "Love Polygon", "Love Triangle", "Love-Hate Relationship",
                "Lust", "Mafia", "Magic", "Magic Schoo", "Magical Creature", "Magical Girl", "Magical Boy",
              "Maid", "Male Demographic with Female Lead", "Male Lead Falls in Love First", "Male Protagonist", "Female Protagonist",
                "Manipulation", "Manly Gay Couple", "Marriage", "Marriage Proposal", "Married Couple", "Masculine Uke", "Masochist",

           "Master-Pet Relationship", "Master-Servant Relationship", "Masturbation", "Mature Child", "Middle School", "Military", "Mind Break",
                "Misunderstanding", "Model", "Misunderstood Protagonist","Middle-earth", "Models", "Modeling",

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

              "Protagonist Strong from the Start", "Psychopath", "Psychological", "Public Sex", "Quirky Character", "Rape",
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

        #region SeedData

        public async Task seedData()
        {
            // ensure we have a database if the user forgetts to update-database

            if (!context.mangaModels.Any())
            {
                await RoleCreating();
                GenreSeed();
                TagsModel();
                addLanguages();
                Tags_Genres_Languages();

                //photopath needs fixing real bad
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage/0f3f5666-7cb9-4713-a779-f1e1546a0d5f");
                // creates a user,
                var user = new UserModel { UserName = "TestUSer", Email = "TestUser@hotmail.com", userPhotoPath = filePath };

                context.SaveChanges();
                AddBerserk();
                CreateNaruto();
                CreateDeathNote();
                CreateHarryPotter();
                CreateFullMetal();
                CreateOnePiece();
                CreateConan();
                AddSailor();
                CreateAttackOnTitan();
                CreateLordOfTheRings();
                CreateSoloLeveling();
                CreateTowerOfGod();
                DonQuiXote();

                RelatedManga();

                var result = await userManager.CreateAsync(user, "Rojhat123!");
                if (result.Succeeded)
                { // adds a role owner to the testobject
                    var resultRole = await userManager.AddToRoleAsync(user, "Owner");
                }
            }
        }

        #endregion SeedData

        public void AddBerserk()
        {
            var Tag1ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Dark Fantasy");
            var Tag2ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Horror");
            var Tag3ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Mature");
            var Gen1ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            MangaModel Berserk = new MangaModel
            {
                MangaName = "Berserk",
                PhotoPath = Path.Combine(ProcessUploadedFile("Berserk.jpeg")),
                ReleaseYear = new DateTime(1989, 8, 25),
                BlogModel = new BlogModel { mangaName = "Berserk" },
                Description = "Berserk follows the character of Guts, a lone mercenary warrior, as he travels a medieval-inspired world of dark fantasy in search of revenge against his former friend and ally, who betrayed him and sacrificed their comrades to demons in order to become one himself.",
                CompletelyTranslated = "No",
                OfficalLanguage = "Japanese",
                ISBN10 = "1593070209",
                ISBN13 = "978-1593070205",
                score = 9.2,
                EndingYear = null,
                StatusInCountryOfOrigin = "Ongoing",
                Type = "Manga",
                OriginalPublisher = "Hakusensha",
                orignalWebtoon = "N/A",
                GroupScanlating = null,
                GroupScanlatingID = null,
                userId = null,
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage> {
        new BuyPage { BuyWebsite = "https://www.amazon.com/Berserk-Vol-1-Kentaro-Miura/dp/1593070209", _Languages = new List<Languages> { EnglishLanguage },  },
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/%E3%83%99%E3%83%AB%E3%82%BB%E3%83%AB%E3%82%AF-1-%E7%95%AA%E5%A4%96%E7%B7%A8-%E3%83%A1%E3%82%AC%E3%83%9F%E3%83%83%E3%82%AF%E3%82%B3%E3%83%9F%E3%83%83%E3%82%AF%E3%82%B9KC-%E3%82%B1%E3%83%B3%E3%82%BF%E3%83%AD%E3%83%BC-%E3%83%9F%E3%83%A5%E3%83%A9/dp/4592135348", _Languages = new List<Languages> { JapaneseLanguage },  },
        new BuyPage { BuyWebsite = "https://www.abebooks.com/products/isbn/9784592135345?cm_sp=bdp-_-9784592135345-_-isbn10", _Languages = new List<Languages> { ChineseLanguage },  },
    },
                AssociatedNames = new List<AssociatedNames>
    {new AssociatedNames { nameString = "Beruseruku" },
new AssociatedNames { nameString = "Berserk: The Prototype" },
},
                OfficalWebsites = new List<OfficalWebsite>
{
new OfficalWebsite { OfficalWebsiteString = "https://berserk.fandom.com/wiki/Berserk_Wiki", OfficalWebsiteName = "Berserk Wiki" },
new OfficalWebsite { OfficalWebsiteString = "https://www.younganimal.com/magazine/lineup.html", OfficalWebsiteName = "Young Animal" },
},
                Authormodels = new List<AuthorModel>
{
new AuthorModel { FirstName = "Kentaro", LastName = "Miura" },
},
                ArtistModels = new List<ArtistModel>
{
new ArtistModel { FirstName = "Kentaro", LastName = "Miura" },
},

                VoiceActors = new List<VoiceActorModel>
{
new VoiceActorModel { FirstName = "Canna", LastName = "Nobutoshi", Gender = "Male" },
new VoiceActorModel { FirstName = "Kenji",LastName="Utsumi", Gender = "Male" },
new VoiceActorModel { FirstName = "Toshiyuki",LastName = "Morikawa",  Gender = "Male" },
new VoiceActorModel { FirstName = "Yuko ", LastName = "Miyamura", Gender="Female" },
},
                GenresModels = new List<GenresModel>
{
new GenresModel { GenreName = "Action" },
new GenresModel { GenreName = "Dark Fantasy" },
new GenresModel { GenreName = "Epic" },
new GenresModel { GenreName = "Horror" },
},
                TagsModels = new List<TagModel>
{
new TagModel { TagName = "Gore" },
new TagModel { TagName = "Tragedy" },
new TagModel { TagName = "Violence" },
},
                Characters = new List<Character>
{
new Character { CharacterName = "Guts", specie="Human",Gender="Male", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Self-taught", Occupation="Mercenary, Swordsman, Leader" },
new Character { CharacterName = "Griffith", specie="Human",Gender="Male", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Founder and leader of the Band of the Hawk" },
new Character { CharacterName = "Casca", specie="Human",Gender="Female", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Commander of the Band of the Hawk" },
new Character { CharacterName = "Zodd the Immortal", specie="Unknown",Gender="Male", Born="Unknown", PlaceOffResidence = "Unknown", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Mercenary" },
new Character { CharacterName = "Puck", specie="Elf",Gender="Male", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Comic relief character" },
new Character { CharacterName = "Farnese de Vandimion", specie="Human",Gender="Female", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Vandimion family", Education="Unknown", Occupation="Leader of the Holy See's Chain Knights" },
new Character { CharacterName = "Serpico", specie="Human",Gender="Male", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Member of the Holy See's Chain Knights" },
new Character { CharacterName = "Ivalera", specie="Elf",Gender="Female", Born="Unknown", PlaceOffResidence = "Midland", World="Berserk", Nationality ="Unknown", Education="Unknown", Occupation="Elf guide and spiritual guide to Schierke" },
},
            };
            context.mangaModels.Add(Berserk);

            context.SaveChanges();
        }

        public void CreateNaruto()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Ninja");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            //first
            DateTime myDate = DateTime.ParseExact("1991-05-08", "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            DateTime started = DateTime.ParseExact("1979-05-08", "yyyy-MM-dd",
                                  System.Globalization.CultureInfo.InvariantCulture);
            MangaModel MangaNaruto = new MangaModel
            {
                MangaName = "Naruto",

                PhotoPath = Path.Combine(ProcessUploadedFile("Naruto.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Naruto" },
                Description = "A Kid who got something stuck in his stomach",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "1569319006",
                ISBN13 = "978-1569319000",
                score = 10,
                relatedSeries = null,
                EndingYear = DateTime.Now,
                StatusInCountryOfOrigin = "11, completed",
                Type = "Manga",
                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Pierrot", Started = started, StudioWebsite = "https://en.pierrot.jp/", Biography = "Pierrot Co., Ltd. (株式会社ぴえろ, Kabushiki-gaisha Piero) is a Japanese animation studio established in May 1979 by Yuji Nunokawa, previously an animator and director for Tatsunoko Production. Its headquarters are located in Mitaka, Tokyo.[2] Pierrot is renowned for several worldwide popular anime series, such as Naruto, Bleach, Yu Yu Hakusho, Black Clover, Boruto: Naruto Next Generations, Tokyo Ghoul, Ghost Stories, Great Teacher Onizuka and Saiyuki. " } },
                OriginalPublisher = "SquareEnix",
                orignalWebtoon = "N/A",
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage> {
                  new BuyPage { BuyWebsite = "https://www.amazon.com/Naruto-Vol-1-Uzumaki/dp/1569319006/ref=sr_1_2?keywords=Naruto+Manga&qid=1673876360&sr=8-2", _Languages = new List<Languages> { EnglishLanguage },  },
                  new BuyPage { BuyWebsite = "https://www.amazon.com/Naruto-1-Japanese-Masashi-Kishimoto/dp/4088728408/ref=sr_1_1?crid=3UV6OU93CQ4G2&keywords=Naruto+Manga+Japanese&qid=1673876516&sprefix=naruto+manga+japane%2Caps%2C191&sr=8-1", _Languages = new List<Languages> { JapaneseLanguage },  },
                  new BuyPage { BuyWebsite = "https://www.abebooks.com/servlet/BookDetailsPL?bi=4678761458&searchurl=kn%3DNaruto%2B1%2B%2528Chinese%2BEdition%2529%26sortby%3D20&cm_sp=snippet-_-srp1-_-title3", _Languages = new List<Languages> { EnglishLanguage },  },
               },

                AssociatedNames = new List<AssociatedNames>
                {
                new AssociatedNames { nameString = "Naluto"  },
                new AssociatedNames { nameString = "Naruto S" } ,
                new AssociatedNames { nameString = "Naruto Shippuden" } ,
                },
                OfficalWebsites = new List<OfficalWebsite>
                {
                new OfficalWebsite { OfficalWebsiteString = "https://naruto-official.com/", OfficalWebsiteName = "Naruto-Official" , Twitter = "https://mobile.twitter.com/naruto_info_en",
                Facebook = "https://www.facebook.com/narutoofficialsns/",
                Line = "https://social-plugins.line.me/lineit/share?url=https://naruto-official.com/en"
                },
                },
                Authormodels = new List<AuthorModel>
                {
                new AuthorModel { FirstName = "Masashi ", LastName = "Kishimoto",  Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. "},
                },
                ArtistModels = new List<ArtistModel>
                {
                new ArtistModel { FirstName = "Masashi ", LastName = "Kishimoto",  Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. "},
                },
                VoiceActors = new List<VoiceActorModel>
                {
                new VoiceActorModel { FirstName = "Nakamura Chie" },
                new VoiceActorModel { FirstName = "Inoue Kazuhiko" },
                new VoiceActorModel { FirstName = "Takeuchi Junko" },
                 new VoiceActorModel { FirstName = "Sugiyama Noriaki" },
                },
                GenresModels = new List<GenresModel>
                {
                    Gen1ToManga1, Gen2ToManga1
                },
                TagsModels = new List<TagModel>
                {
                    Tag1ToManga1,  Tag2ToManga1
                },
                Characters = new List<Character>
                {
                new Character { CharacterName = "Uzumaki Naruto", specie="Human",Gender="Male", Born="N/A", PlaceOffResidence = "Konoha", World="Naruto", Nationality ="Japanese", Education="Genin", Occupation="Hokage" },
                new Character { CharacterName = "Uchiha  Sasuke", specie="Human",Gender="Male", Born="N/A", PlaceOffResidence = "Konoha", World="Naruto", Nationality ="Japanese", Education="Chunin", Occupation="Ninja"},
                new Character { CharacterName = "Hatake Kakashi " ,specie="Human",Gender="Male", Born="N/A", PlaceOffResidence = "Konoha", World="Naruto", Nationality ="Japanese", Education="Anbu", Occupation="Hokage"},
                new Character { CharacterName = "Haruno Sakura" ,specie="Human",Gender="Female", Born="N/A", PlaceOffResidence = "Konoha", World="Naruto", Nationality ="Japanese", Education="Chunin", Occupation="Ninja"},
                },
            };
            context.mangaModels.Add(MangaNaruto);
            context.SaveChanges();
        }

        public void CreateHarryPotter()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Magic");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Fantasy");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");

            //first
            DateTime myDate = DateTime.ParseExact("1997-06-26", "yyyy-MM-dd",
                                         System.Globalization.CultureInfo.InvariantCulture);
            MangaModel MangaHarryPotter = new MangaModel
            {
                MangaName = "Harry Potter",

                PhotoPath = Path.Combine(ProcessUploadedFile("HarryPotter.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Harry Potter" },
                Description = "The story follows the life of an orphan boy, Harry Potter, who discovers he is a wizard and embarks on a journey to learn magic and stop the evil Lord Voldemort.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "English",
                ISBN10 = "1408855674",
                ISBN13 = "978-1408855676",
                score = 9.5,

                EndingYear = DateTime.Now,
                StatusInCountryOfOrigin = "7, completed",
                Type = "Novel",
                OriginalPublisher = "Scholastic",
                orignalWebtoon = "N/A",
                GroupScanlating = null,
                GroupScanlatingID = null,
                userModels = null,
                userId = null,
                AllLanguages = new List<Languages> { EnglishLanguage, JapaneseLanguage, FrenchLanguage },
                BuyPages = new List<BuyPage> {
          new BuyPage { BuyWebsite = "https://www.amazon.com/Harry-Potter-Sorcerers-Stone-Rowling/dp/059035342X/ref=sr_1_2?keywords=Harry+Potter+manga&qid=1673878089&sr=8-2", _Languages = new List<Languages> { EnglishLanguage },  },
          new BuyPage { BuyWebsite = "https://www.amazon.co.jp/-/en/J-K-Rowling/dp/4835421060/ref=sr_1_1?crid=1ML74E33K0V7Q&keywords=harry+potter+manga&qid=1645671799&s=books&sprefix=harry+po%2Cstripbooks%2C242&sr=1-1", _Languages = new List<Languages> { JapaneseLanguage },  },
          new BuyPage { BuyWebsite = "https://www.fnac.com/a8901018/Harry-Potter-Tome-1-Harry-Potter-a-l-ecole-des-sorciers-1-J-K-Rowling-Librio", _Languages = new List<Languages> { FrenchLanguage },  },
       },

                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "ハリー・ポッターと賢者の石" },
            new AssociatedNames { nameString = "Harry Potter and the Philosopher's Stone" } ,
            new AssociatedNames { nameString = "哈利·波特与魔法石" } ,
        },
                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.wizardingworld.com/", OfficalWebsiteName = "Wizarding World" , Twitter = "https://twitter.com/wizardingworld",
            Facebook = "https://www.facebook.com/wizardingworld/",
            Instagram = "https://www.instagram.com/wizardingworld/",
            },
        },
                Authormodels = new List<AuthorModel>
        {
            new AuthorModel { FirstName = "J.K.", LastName = "Rowling",  Biography = "J.K. Rowling is the author of the record-breaking, multi-award-winning Harry Potter novels. "},
        },
                ArtistModels = new List<ArtistModel>
        {
            new ArtistModel { FirstName = "Motohiro", LastName = "Kawashima",  Biography = "Motohiro Kawashima is a manga artist who has worked on many titles including Harry Potter and the Philosopher's Stone." },
        },
                VoiceActors = new List<VoiceActorModel>
        {
            new VoiceActorModel { FirstName = "Rupert", LastName = "Grint" },
            new VoiceActorModel { FirstName = "Emma", LastName = "Watson" },
            new VoiceActorModel { FirstName = "Daniel", LastName = "Radcliffe" },
        },
                GenresModels = new List<GenresModel>
        {
            Gen1ToManga1, Gen2ToManga1
        },
                TagsModels = new List<TagModel>
        {
            Tag1ToManga1, Tag2ToManga1
        },
                Characters = new List<Character>
        {
            new Character { CharacterName = "Harry Potter", specie="Human",Gender="Male", Born="31 July 1980", PlaceOffResidence = "Hogwarts", World="Wizarding World", Nationality ="British", Education="Hogwarts School of Witchcraft and Wizardry", Occupation="Wizard" },
            new Character { CharacterName = "Ron Weasley", specie="Human",Gender="Male", Born="1 March 1980", PlaceOffResidence = "Hogwarts", World="Wizarding World", Nationality ="British", Education="Hogwarts School of Witchcraft and Wizardry", Occupation="Wizard"},
            new Character { CharacterName = "Hermione Granger", specie="Human",Gender="Female", Born="19 September 1979", PlaceOffResidence = "Hogwarts", World="Wizarding World", Nationality ="British", Education="Hogwarts School of Witchcraft and Wizardry", Occupation="Wizard"},
            new Character { CharacterName = "Albus Dumbledore", specie="Human",Gender="Male", Born="1881", PlaceOffResidence = "Hogwarts", World="Wizarding World", Nationality ="British", Education="Hogwarts School of Witchcraft and Wizardry", Occupation="Wizard"},
        },
            };
        }

        public void CreateDeathNote()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Death of Loved One");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Psychological");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Mystery");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Thriller");

            DateTime myDate = DateTime.ParseExact("2003-12-01", "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            MangaModel MangaDeathNote = new MangaModel
            {
                MangaName = "Death Note",
                PhotoPath = Path.Combine(ProcessUploadedFile("DeathNote.jpeg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Death Note" },
                Description = "A young high school student discovers a supernatural notebook that can kill anyone whose name is written in it.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "1421501686",
                ISBN13 = "978-1421501680",
                score = 9.5,

                EndingYear = DateTime.ParseExact("2006-05-15 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                     System.Globalization.CultureInfo.InvariantCulture),
                StatusInCountryOfOrigin = "12, completed",
                Type = "Manga",
                OriginalPublisher = "Shueisha",
                orignalWebtoon = "N/A",
                GroupScanlating = null,
                GroupScanlatingID = null,
                userModels = null,
                userId = null,
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, SpanishLanguage },
                BuyPages = new List<BuyPage> {
          new BuyPage { BuyWebsite = "https://www.amazon.com/Death-Note-Box-Set-Vols/dp/142152581X", _Languages = new List<Languages> { EnglishLanguage },  },
          new BuyPage { BuyWebsite = "https://www.amazon.com/Death-Note-Black-Manga/dp/2505001525", _Languages = new List<Languages> { FrenchLanguage },  },
          new BuyPage { BuyWebsite = "https://www.amazon.co.jp/-/en/Obata-Takeshi-Tsugumi-Ohba/dp/408872836X/ref=sr_1_1?crid=13XLV7IL5PMZ&keywords=death+note+%E3%83%9E%E3%83%B3%E3%82%AC&qid=1645671799&s=books&sprefix=death+%2Cstripbooks%2C261&sr=1-1", _Languages = new List<Languages> { JapaneseLanguage },  },
       },

                AssociatedNames = new List<AssociatedNames>
{
new AssociatedNames { nameString = "デスノート (Japanese)" },
new AssociatedNames { nameString = "Cahier de la Mort (French)" } ,
new AssociatedNames { nameString = "Cuaderno de la Muerte (Spanish)" } ,
},
                OfficalWebsites = new List<OfficalWebsite>
{
new OfficalWebsite { OfficalWebsiteString = "https://www.j-deathnote.com/", OfficalWebsiteName = "Death Note Official Site", Twitter = "https://twitter.com/dnote_official",
Facebook = "https://www.facebook.com/deathnote.official", Instagram = "https://www.instagram.com/deathnote_official/" },
},
                Authormodels = new List<AuthorModel>
{
new AuthorModel { FirstName = "Tsugumi ", LastName = "Ohba", Biography = "Unknown" },
},
                ArtistModels = new List<ArtistModel>
{
new ArtistModel { FirstName = "Takeshi ", LastName = "Obata", Biography = "Graduated from the Graphic Design Program at Nagoya University of Arts" },
},
                VoiceActors = new List<VoiceActorModel>
{
new VoiceActorModel { FirstName = "Miyano Mamoru" },
new VoiceActorModel { FirstName = "Miyuki Sawashiro" },
new VoiceActorModel { FirstName = "Kappei Yamaguchi" },
},
                GenresModels = new List<GenresModel>
{
Gen1ToManga1, Gen2ToManga1
},
                TagsModels = new List<TagModel>
{
Tag1ToManga1, Tag2ToManga1
},
                Characters = new List<Character>
{
new Character { CharacterName = "Light Yagami", specie="Human",Gender="Male", Born="February 28, 1986", PlaceOffResidence = "Japan", World="Death Note", Nationality ="Japanese", Education="Senior High School Student", Occupation="College Student, Criminal Mastermind" },
new Character { CharacterName = "L Lawliet", specie="Human",Gender="Male", Born="October 31, 1979", PlaceOffResidence = "Japan", World="Death Note", Nationality ="English", Education="Whammy's House", Occupation="Detective" },
new Character { CharacterName = "Misa Amane" ,specie="Human",Gender="Female", Born="December 25, 1984", PlaceOffResidence = "Japan", World="Death Note", Nationality ="Japanese", Education="High School Graduate", Occupation="Actress, Model, Kira's supporter" },
},
            };
            context.mangaModels.Add(MangaDeathNote);
            context.SaveChanges();
        }

        public void CreateFullMetal()
        {
            var OpGen1 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Adventure");
            var OPGen2 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Action");
            var OpGen3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Fantasy");
            var tag4 = context.TagModels.FirstOrDefault(e => e.TagName == "Arrogant Male Lead");
            var tag5 = context.TagModels.FirstOrDefault(e => e.TagName == "Insecurity");
            var tag6 = context.TagModels.FirstOrDefault(e => e.TagName == "Revenge");
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
                PhotoPath = Path.Combine(ProcessUploadedFile("FullMetal.jpeg")),
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
              OpGen1, OPGen2, OpGen3
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
            context.mangaModels.Add(fullmetalAlchemist);
            context.SaveChanges();
        }

        public void CreateOnePiece()
        {
            var OpTag = context.TagModels.FirstOrDefault(e => e.TagName == "Pirates");
            var OpTag2 = context.TagModels.FirstOrDefault(e => e.TagName == "Hot-Blooded Male Lead");
            var OpTag3 = context.TagModels.FirstOrDefault(e => e.TagName == "Samurai");
            var OpGen1 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Adventure");
            var OPGen2 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Action");
            var OpGen3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Fantasy");
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
                PhotoPath = Path.Combine(ProcessUploadedFile("OnePiece.jpg")),
                Description = "Follow the adventures of Monkey D. Luffy and the Straw Hat Pirates on their journey to become the King of the Pirates.",
                ReleaseYear = new DateTime(1997, 7, 19),
                EndingYear = null,
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
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
                        new BuyPage { BuyWebsite ="https://www.amazon.com/One-Piece-Vol-Romance-Dawn/dp/1569319014/ref=sr_1_3?keywords=Manga+One+Piece&qid=1675256565&sr=8-3", _Languages = new List<Languages>{  }},
                        new BuyPage {  BuyWebsite = "https://www.amazon.com/One-Piece-Vol-1-Japanese/dp/4088725093", _Languages = new List<Languages>{  } }
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
                        OpTag, OpTag2, OpTag3
                    },

                GenresModels = new List<GenresModel>
                    {
                        OpGen1, OPGen2, OpGen3
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

            context.mangaModels.Add(onePiece);
            context.SaveChanges();
        }

        public void AddSailor()
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "MangaImage");

            var SailorTag7 = context.TagModels.FirstOrDefault(e => e.TagName == "Strong Female Lead");
            var SailorTag8 = context.TagModels.FirstOrDefault(e => e.TagName == "Coming of Age");
            var SailorTag9 = context.TagModels.FirstOrDefault(e => e.TagName == "Family");
            var SailorGen4 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Drama");
            var SailorGen5 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Slice of Life");
            var Sailorgen6 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Comedy");

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
                PhotoPath = Path.Combine(ProcessUploadedFile("SailorMoon.jpg")),
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
        new OfficalWebsite { OfficalWebsiteString = "www.sailormoon.com", OfficalWebsiteName = "SailorMoon.Com",  },
        new OfficalWebsite { OfficalWebsiteString = "sailormoon.jp", OfficalWebsiteName = "SailorMoon.jp", },
    },
                Authormodels = new List<AuthorModel>
    {
        new AuthorModel { FirstName = "Naoko Takeuchi" },
    },
                ArtistModels = new List<ArtistModel>
    {
        new ArtistModel { FirstName = "Naoko Takeuchi" },
    },
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite ="https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419", _Languages = new List<Languages>{ EnglishLanguage }},
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
    },
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
                VoiceActors = new List<VoiceActorModel>
    {
        new VoiceActorModel { FirstName = "Kotono Mitsuishi" },
        new VoiceActorModel { FirstName = "Aya Hisakawa" },
        new VoiceActorModel { FirstName = "Emi Shinohara" },
    },
                GenresModels = new List<GenresModel>
    {
        SailorGen4, SailorGen5, Sailorgen6
    },
                TagsModels = new List<TagModel>
    {
        SailorTag7, SailorTag8, SailorTag9
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
            context.mangaModels.Add(SailorMoon);
            context.SaveChanges();
        }

        public void CreateConan()
        {
            var GenreCon4 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Mystery");
            var GenreCon5 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Detective");
            var tagCon5 = context.TagModels.FirstOrDefault(e => e.TagName == "Secret Identity");
            var tagCon6 = context.TagModels.FirstOrDefault(e => e.TagName == "Murder");
            //first
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
                PhotoPath = Path.Combine(ProcessUploadedFile("Conan.jpeg")),
                Description = "A high school detective who is transformed into a child after being poisoned, solves crimes under a different identity while searching for a cure.",
                ReleaseYear = new DateTime(1994, 1, 2),
                EndingYear = new DateTime(),
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
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
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite ="https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419", _Languages = new List<Languages>{ EnglishLanguage }},
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
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
            context.SaveChanges();
        }

        public void CreateAttackOnTitan()
        {
            var tagAt5 = context.TagModels.FirstOrDefault(e => e.TagName == "Absent Parent");

            var GenreAt2 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Fantasy");
            var GenreAt3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Shounen");
            var GenreAt4 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Mecha");

            var tag4 = context.TagModels.FirstOrDefault(e => e.TagName == "Arrogant Male Lead");
            var tag5 = context.TagModels.FirstOrDefault(e => e.TagName == "Insecurity");
            var tag6 = context.TagModels.FirstOrDefault(e => e.TagName == "Revenge");
            //first
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
                PhotoPath = Path.Combine(ProcessUploadedFile("AttackOnTitan.jpeg")),
                Description = "The story follows Eren Jeager and Mikasa Ackerman as they join the Scout Regiment to fight against the Titans who threaten their city and the rest of humanity.",
                ReleaseYear = new DateTime(2013, 4, 7),
                EndingYear = new DateTime(2013, 9, 29),
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite ="https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419", _Languages = new List<Languages>{ EnglishLanguage }},
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
    },
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
GenreAt2, GenreAt3, GenreAt4
},
                TagsModels = new List<TagModel>
{
tagAt5, tag4,tag5,tag6
},
                Characters = new List<Character>
{
new Character { CharacterName = "Eren Jeager" },
new Character { CharacterName = "Mikasa Ackerman" },
},
            };
            context.mangaModels.Add(AttackOnTitan);
            context.SaveChanges();
        }

        public void CreateLordOfTheRings()
        {
            var tag1 = context.TagModels.FirstOrDefault(t => t.TagName == "Emotionally Strong Male Lead");
            var tag2 = context.TagModels.FirstOrDefault(t => t.TagName == "Sword and Sorcery");
            var genre1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var genre2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");

            MangaModel lotr = new MangaModel
            {
                MangaName = "Lord of the Rings",
                PhotoPath = Path.Combine(ProcessUploadedFile("lotr.jpg")),
                BlogModel = new BlogModel { mangaName = "Lord of the Rings" },
                ReleaseYear = new DateTime(1954, 7, 29),
                Description = "In a sleepy village in the Shire, young hobbit Frodo Baggins finds himself tasked with an immense quest; to destroy the One Ring and thus ensure the destruction of the Dark Lord Sauron. Alongside a fellowship of heroes including wizards, elves and dwarves, he must journey across the land of Middle-earth, facing danger at every turn.",
                CompletelyTranslated = "Yes",
                OfficalLanguage = "English",
                ISBN10 = "0618640150",
                ISBN13 = "978-0618640157",
                score = 9.5,
                EndingYear = null,
                StatusInCountryOfOrigin = "Completed",
                Type = "Novel",
                OriginalPublisher = "Allen & Unwin",
                orignalWebtoon = null,
                GroupScanlating = null,
                GroupScanlatingID = null,
                userModels = null,
                userId = null,
                AllLanguages = new List<Languages> { EnglishLanguage },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Lord-Rings-Trilogy-Boxed-Set/dp/0544445783/ref=sr_1_1?keywords=lord+of+the+rings&qid=1645876628&sr=8-1", _Languages = new List<Languages> { EnglishLanguage } }
        },
                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "LOTR" },
            new AssociatedNames { nameString = "In the Name of the King" }
        },
                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.lordoftherings.net/", OfficalWebsiteName = "Lord of the Rings Official Site", Twitter = "https://twitter.com/lordoftherings", Facebook = "https://www.facebook.com/lordoftheringstrilogy/", Line = null }
        },
                Authormodels = new List<AuthorModel>
        {
            new AuthorModel { FirstName = "J.R.R.", LastName = "Tolkien", Biography = "John Ronald Reuel Tolkien CBE FRSL (3 January 1892 – 2 September 1973) was an English writer, poet, philologist, and academic. He is best known as the author of the high fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion." }
        },
                ArtistModels = new List<ArtistModel>
{
    new ArtistModel { FirstName = "Alan", LastName = "Lee", Biography = "Alan Lee is an English artist and writer. He is best known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings. He won an Academy Award for Best Art Direction for his work on the 2003 film The Lord of the Rings: The Return of the King." },
    new ArtistModel { FirstName = "John", LastName = "Howe", Biography = "John Howe is a Canadian book illustrator, living in Neuchâtel, Switzerland. One year after studying in a college in Oakville, Howe moved to France. He has illustrated many fantasy books, including The Lord of the Rings and The Hobbit by J. R. R. Tolkien, and is one of the conceptual designers employed by Peter Jackson's The Lord of the Rings film trilogy." },
    new ArtistModel { FirstName = "Ted", LastName = "Nasmith", Biography = "Ted Nasmith is a Canadian artist and illustrator. He is best known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings, and has produced several Tolkien-themed calendars and illustrated editions of The Silmarillion, The Lord of the Rings and The Hobbit." },
    new ArtistModel { FirstName = "Roger", LastName = "Garland", Biography = "Roger Garland is an artist and illustrator from the United Kingdom. He is known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings, and has created artwork for various editions of The Hobbit and The Lord of the Rings, as well as other fantasy works." },
    new ArtistModel { FirstName = "Donato", LastName = "Giancola", Biography = "Donato Giancola is an American artist and illustrator, best known for his work in the science fiction and fantasy genres. He has created artwork for a number of book covers, including editions of The Lord of the Rings and The Hobbit, as well as works by other authors in the genre." },
    new ArtistModel { FirstName = "Michael", LastName = "Hague", Biography = "Michael Hague is an American illustrator and author, known for his work in the fantasy genre. He has created artwork for numerous children's books, including editions of The Hobbit and The Lord of the Rings, as well as works by other authors." }
},
                VoiceActors = new List<VoiceActorModel>
{
    new VoiceActorModel { FirstName = "Rob Inglis", CharacterName = "Narrator" },
    new VoiceActorModel { FirstName = "Ian Holm", CharacterName = "Bilbo Baggins" },
    new VoiceActorModel { FirstName = "Andy Serkis", CharacterName = "Gollum" },
    new VoiceActorModel { FirstName = "Christopher Lee", CharacterName = "Saruman" }
},
                GenresModels = new List<GenresModel>
        {
            genre1,
            genre2
        },
                TagsModels = new List<TagModel>
    {
        tag1,
        tag2,
    },
                Characters = new List<Character>
    {
        new Character { CharacterName = "Frodo Baggins", specie = "Hobbit", Gender = "Male", Born = "September 22, T.A. 2968", PlaceOffResidence = "Shire", World = "Middle-earth", Nationality = "Shire", Education = "Private tutor", Occupation = "Ring-bearer" },
        new Character { CharacterName = "Gandalf", specie = "Maiar", Gender = "Male", Born = "Unknown", PlaceOffResidence = "Middle-earth", World = "Middle-earth", Nationality = "Unknown", Education = "Unknown", Occupation = "Wizard" },
        new Character { CharacterName = "Aragorn", specie = "Human", Gender = "Male", Born = "March 1, T.A. 2931", PlaceOffResidence = "Gondor", World = "Middle-earth", Nationality = "Dúnedain", Education = "Ranger training", Occupation = "King of Gondor" },
        new Character { CharacterName = "Legolas", specie = "Elf", Gender = "Male", Born = "Third Age, 87", PlaceOffResidence = "Mirkwood", World = "Middle-earth", Nationality = "Woodland Realm", Education = "Unknown", Occupation = "Prince of Mirkwood" },
    },
            };
            context.mangaModels.Add(lotr);
            context.SaveChanges();
        }

        public void CreateSoloLeveling()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Level System");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Monster");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Weak to Strong");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");
            DateTime started = DateTime.ParseExact("2003-09-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var manga = new MangaModel
            {
                MangaName = "Solo Leveling",
                PhotoPath = Path.Combine(ProcessUploadedFile("SoloLeveling.jpg")),
                ReleaseYear = new DateTime(2018, 3, 19),
                BlogModel = new BlogModel { mangaName = "Solo Leveling" },
                Description = "The story of a world in which an average guy awakens as a hunter and aims to level up by risking his life in dangerous dungeons.",
                CompletelyTranslated = "Ongoing",
                OfficalLanguage = "Korean",
                ISBN10 = "8952776072",
                ISBN13 = "978-8952776078",
                score = 9,
                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Jin Ho Sung", Started = started, StudioWebsite = "N/A", Biography = "Jin Ho Sung is the author and artist of Solo Leveling." } },
                EndingYear = null,
                StatusInCountryOfOrigin = "14, ongoing",
                Type = "Manhwa",
                OriginalPublisher = "D&C Media",
                orignalWebtoon = "https://page.kakao.com/home?seriesId=50870315",

                AllLanguages = new List<Languages> { KoreanLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite = "https://www.amazon.com/Solo-Leveling-Vol-Chu-Gong/dp/1974720273", _Languages = new List<Languages> { EnglishLanguage },  },
        new BuyPage { BuyWebsite = "https://www.kyobobook.co.kr/product/detailViewKor.laf?barcode=9791189381709", _Languages = new List<Languages> { KoreanLanguage },  },
        new BuyPage { BuyWebsite = "https://www.cdjapan.co.jp/product/NEOBK-2299254", _Languages = new List<Languages> { JapaneseLanguage },  },
    },

                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "Na Honjaman Level Up" },
            new AssociatedNames { nameString = "Only I Level Up" },
            new AssociatedNames { nameString = "I Alone Level-Up" },
            new AssociatedNames { nameString = "Solo Leveling" },
            new AssociatedNames { nameString = "Tôi thăng cấp một mình" }
        },

                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://page.kakao.com/home?seriesId=50870315", OfficalWebsiteName = "KakaoPage" },
            new OfficalWebsite { OfficalWebsiteString = "https://page.line.me/uqk0111n", OfficalWebsiteName = "LINE Webtoon" },
            new OfficalWebsite { OfficalWebsiteString = "https://comic.naver.com/webtoon/list.nhn?titleId=758671", OfficalWebsiteName = "Naver Webtoon" }
        },

                GenresModels = new List<GenresModel>
{
    Gen1ToManga1,
    Gen2ToManga1
},
                TagsModels = new List<TagModel>
{
    Tag1ToManga1,
    Tag2ToManga1,
    Tag3ToManga1
},
                Characters = new List<Character>
        {
            new Character { CharacterName = "Sung Jin-Woo",   specie="Human", Gender="Male", Born="March 20, 1997", PlaceOffResidence = "South Korea", World="Solo Leveling", Nationality ="Korean", Education="High School", Occupation="Hunter" },
        new Character { CharacterName = "Cha Hae-In", specie="Human",Gender="Female", Born="January 6, 1998", PlaceOffResidence = "South Korea", World="Solo Leveling", Nationality ="Korean", Education="Hunter Association", Occupation="Hunter"},
        new Character { CharacterName = "Go Gun-Hee", specie="Human",Gender="Male", Born="Unknown", PlaceOffResidence = "South Korea", World="Solo Leveling", Nationality ="Korean", Education="Unknown", Occupation="Chairman of the Hunter Association"},
        new Character { CharacterName = "Jin-Ah Sung", specie="Human",Gender="Female", Born="Unknown", PlaceOffResidence = "South Korea", World="Solo Leveling", Nationality ="Korean", Education="High School", Occupation="Sung Jin-Woo's sister"},
    },

                Authormodels = new List<AuthorModel>
    {
        new AuthorModel { FirstName = "Chu-Gong" },
    },

                ArtistModels = new List<ArtistModel>
    {
        new ArtistModel { FirstName = "Jang Sung-Rak" },
    },

                VoiceActors = new List<VoiceActorModel>(),
            };

            context.mangaModels.Add(manga);
            context.SaveChanges();
        }

        public void CreateTowerOfGod()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Jealousy");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Weak to Strong");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Hunted Protagonist");
            var Tag4ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Near-Death Experience");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Supernatural");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            DateTime myDate = DateTime.ParseExact("2010-06-30", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            MangaModel MangaTowerOfGod = new MangaModel
            {
                MangaName = "Tower of God",
                PhotoPath = Path.Combine(ProcessUploadedFile("TowerOfGod.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Tower of God" },
                Description = "Bam climbs the Tower of God to reach the top and find his friend Rachel, but the Tower is full of deadly tests and other climbers who will do anything to reach the top.",
                CompletelyTranslated = "Ongoing",
                OfficalLanguage = "Korean",
                ISBN10 = "8952745013",
                ISBN13 = "978-8952745018",
                score = 9,

                EndingYear = null,
                StatusInCountryOfOrigin = "3, Ongoing",
                Type = "Manhwa",
                OriginalPublisher = "Naver",
                orignalWebtoon = "https://comic.naver.com/webtoon/list.nhn?titleId=183559",
                GroupScanlating = null,
                GroupScanlatingID = null,
                userModels = null,
                userId = null,
                AllLanguages = new List<Languages> { KoreanLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Tower-God-Vol-1-SIU/dp/8952745013/ref=sr_1_1?crid=3OBHYUE8WT2SB&keywords=tower+of+god+manga&qid=1651934029&sprefix=tower+of+god%2Caps%2C188&sr=8-1", _Languages = new List<Languages> { EnglishLanguage } },
            new BuyPage { BuyWebsite = "http://www.yes24.com/Product/Goods/78819624?scode=032&OzSrank=1", _Languages = new List<Languages> { KoreanLanguage } },
            new BuyPage { BuyWebsite = "https://www.aladin.co.kr/search/wsearchresult.aspx?SearchTarget=All&SearchWord=%EC%9C%A0%EC%A3%BC%ED%98%B8%EB%B0%B0%EC%9A%B0", _Languages = new List<Languages> { KoreanLanguage } }
        },
                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "신의 탑" },
            new AssociatedNames { nameString = "Tower of God" } ,
        },
                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "http://comic.naver.com/webtoon/list.nhn?titleId=183559&weekday=mon", OfficalWebsiteName = "Naver Webtoon" },
            new OfficalWebsite { OfficalWebsiteString = "https://www.webtoons.com/en/fantasy/tower-of-god/list?title_no=95&page=1", OfficalWebsiteName = "Webtoons" },
        },
                Authormodels = new List<AuthorModel>
        {
            new AuthorModel { FirstName = "S.I.", LastName = "UWoo" },
        },
                ArtistModels = new List<ArtistModel>
        {
            new ArtistModel { FirstName = "LEE", LastName = "Jong-hui" },
        },
                VoiceActors = new List<VoiceActorModel>(),
                GenresModels = new List<GenresModel>
        {
            new GenresModel { GenreName = "Action" },
            new GenresModel { GenreName = "Adventure" },
            new GenresModel { GenreName = "Fantasy" },
        },
                TagsModels = new List<TagModel>
        {
            new TagModel { TagName = "Tower Climbing" },
            new TagModel { TagName = "Monsters" },
            new TagModel { TagName = "Magic" },
        },

                Characters = new List<Character>
        {
            new Character { CharacterName = "Twenty-Fifth Baam", specie="Human", Gender="Male", Born="N/A", PlaceOffResidence = "The Tower", World="Tower of God", Nationality ="N/A", Education="N/A", Occupation="Irregular" },
            new Character { CharacterName = "Yuri Jahad", specie="Human", Gender="Female", Born="N/A", PlaceOffResidence = "The Tower", World="Tower of God", Nationality ="N/A", Education="N/A", Occupation="Princess of Jahad" },
            new Character { CharacterName = "Khun Aguero Agnis", specie="Human", Gender="Male", Born="N/A", PlaceOffResidence = "The Tower", World="Tower of God", Nationality ="N/A", Education="N/A", Occupation="Fisherman" },
            new Character { CharacterName = "Rak Wraithraiser", specie="Crocus", Gender="Male", Born="N/A", PlaceOffResidence = "The Tower", World="Tower of God", Nationality ="N/A", Education="N/A", Occupation="Spear Bearer" },
        },
            };

            context.mangaModels.Add(MangaTowerOfGod);
            context.SaveChanges();
        }

        public void DonQuiXote()
        {
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Middle-earth");
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Knight");
            var Gen0ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Classics");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Comedy");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            DateTime myDate = DateTime.ParseExact("1605-01-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime started = DateTime.ParseExact("1605-01-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            MangaModel DonQuixote = new MangaModel
            {
                MangaName = "Don Quixote",
                PhotoPath = Path.Combine(ProcessUploadedFile("Don_Quixote.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Don Quixote" },
                Description = "A man who reads too many chivalric romances and decides to become a knight-errant",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Spanish",
                ISBN10 = "9780142437230",
                ISBN13 = "0142437239",
                score = 9.2,
                relatedSeries = null,
                EndingYear = DateTime.Now,
                StatusInCountryOfOrigin = "Complete",
                Type = "Novel",
                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Pan Pacific International Holdings Corporation", Started = started, StudioWebsite = "N/A", Biography = "N/A" } },
                OriginalPublisher = "Francisco de Robles",
                orignalWebtoon = "N/A",
                AllLanguages = new List<Languages> { SpanishLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage> {
        new BuyPage { BuyWebsite = "https://www.amazon.com/Don-Quixote-Miguel-Cervantes/dp/0142437239", _Languages = new List<Languages> { EnglishLanguage },  },
        new BuyPage { BuyWebsite = "https://www.amazon.es/Don-Quijote-Mancha-Cervantes/dp/8420652364", _Languages = new List<Languages> { SpanishLanguage },  },
    },
                AssociatedNames = new List<AssociatedNames>
    {
        new AssociatedNames { nameString = "El Ingenioso Hidalgo Don Quijote de la Mancha"  },
    },
                OfficalWebsites = new List<OfficalWebsite>
    {
        new OfficalWebsite { OfficalWebsiteString = "N/A", OfficalWebsiteName = "N/A", Twitter = "N/A", Facebook = "N/A", Line = "N/A" },
    },
                Authormodels = new List<AuthorModel>
    {
        new AuthorModel { FirstName = "Miguel de", LastName = "Cervantes",  Biography = "An acclaimed Spanish writer who is widely regarded as one of the greatest writers in the Spanish language and one of the world's pre-eminent novelists." },
    },
                ArtistModels = new List<ArtistModel> {
        new ArtistModel { FirstName = "Gustave", LastName = "Doré", Biography = "A French artist, engraver, illustrator, and sculptor. Doré worked primarily with wood engraving and steel engraving." },
        new ArtistModel { FirstName = "Francisco", LastName = "Rico", Biography = "A Spanish literary critic and academician. He is an expert in C " }
                },
                VoiceActors = new List<VoiceActorModel> {
new VoiceActorModel { FirstName = "John", LastName = "Lescault" },
new VoiceActorModel { FirstName = "Robert", LastName = "Whitmire" },
new VoiceActorModel { FirstName = "George", LastName = "Guidall" },
},
                GenresModels = new List<GenresModel>
{
Gen1ToManga1, Gen2ToManga1, Gen0ToManga1
},
                TagsModels = new List<TagModel>
{
 Tag2ToManga1
},
                Characters = new List<Character>
{
new Character { CharacterName = "Don Quixote", specie = "Human", Gender = "Male", Born = "N/A", PlaceOffResidence = "Spain", World = "Don Quixote", Nationality = "Spanish", Education = "N/A", Occupation = "Knight-errant" },
new Character { CharacterName = "Sancho Panza", specie = "Human", Gender = "Male", Born = "N/A", PlaceOffResidence = "Spain", World = "Don Quixote", Nationality = "Spanish", Education = "N/A", Occupation = "Squire" },
new Character { CharacterName = "Dulcinea del Toboso", specie = "Human", Gender = "Female", Born = "N/A", PlaceOffResidence = "Spain", World = "Don Quixote", Nationality = "Spanish", Education = "N/A", Occupation = "Noblewoman" },
},
            };
            context.mangaModels.Add(DonQuixote);
            context.SaveChanges();
        }
    }
}