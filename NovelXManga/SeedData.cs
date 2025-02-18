﻿using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace NovelXManga
{
    public class SeedData
    {
        #region properties, constructor and readonly.

        public UserModel User { get; set; }
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
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

        public SeedData(MangaNNovelAuthDBContext context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager)
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
            return GeneradedUploadedFile(filename, "GeneratedMangaImage");
        }

        private string GeneradedUploadedFile(string filename, string generatedFolder)
        {
            string uniqueFileName = null;
            if (filename != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", generatedFolder);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(uploadsFolder);

                string extension = Path.GetExtension(filename);
                uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                File.Copy(Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", generatedFolder.Replace("Generated", ""), filename), filePath);
            }
            return uniqueFileName;
        }

        // Changes the folder to "GeneratedCharacterImage"
        private string CharacterProcessUploadedFile(string filename)
        {
            return GeneradedUploadedFile(filename, "GeneratedCharacterImage");
        }

        private string UserProfileProcessUploadedFile(string filename)
        {
            return GeneradedUploadedFile(filename, "GeneratedUserImage");
        }

        // Changes the folder to "GeneratedWallpaperImages"
        private string WallProcessUploadedFile(string filename)
        {
            return GeneradedUploadedFile(filename, "GeneratedWallpaperImages");
        }

        public void addLanguages()
        {
            string[] LanguagesInput = {
        // Add the top 150 languages in alphabetical order here
        "Ainu", "Azerbaijani", "Arabic", "Armenian", "Baltic", "Bashkir", "Bengali", "Bulgarian", "Burmese", "Croatian",
        "Chukotko-Kamchatkan", "Chinese", "Czech", "Danish", "Dravidian", "Dutch", "English", "Eskimo-Aleut", "Estonian",
        "Farsi", "Finnish", "French", "Georgian", "German", "Greek", "Gujarati", "Hebrew", "Hindi", "Hungarian", "Indo-Aryan",
        "Indonesian", "Italian", "Icelandic", "Japanese", "Japonic", "Khmer", "Khoisan", "Kazakh", "Kordofanian", "Korean",
        "Kurdish", "Kyrgyz", "Lao", "Latvian", "Lithuanian", "Malay", "Malayo-Polynesian", "Marathi", "Mongolic", "Mongolian",
        "Nepali", "Niger-Congo", "Nilo-Saharan", "Norwegian", "Omotic", "Pashto", "Polish", "Portuguese", "Punjabi", "Romanian",
        "Russian", "Semitic", "Serbian", "Sinhalese", "Slovak", "Slovenian", "Sino-Tibetan", "Slavic", "Spanish", "Swahili",
        "Swedish", "Semitic", "Tai-Kadai", "Tadjik", "Tagalog", "Tamil", "Turkic", "Turkmen", "Tungusic", "Tibetic", "Tatar",
        "Thai", "Uighur", "Ukrainian", "Urdu", "Uralic", "Uzbek", "Vietnamese"
    };
            string[] FlagUnicode = {
    "🇦🇫", "🇦🇿", "🇸🇦", "🇦🇲", "🇱🇧", "🇧🇷", "🇧🇬", "🇧🇩", "🇧🇾", "🇧🇪", "🇷🇺", "🇨🇳", "🇮🇷", "🇵🇰",
    "🇰🇭", "🇨🇲", "🇨🇦", "🇨🇫", "🇹🇩", "🇨🇴", "🇨🇬", "🇨🇷", "🇭🇷", "🇨🇺", "🇨🇾", "🇨🇿", "🇩🇰", "🇩🇯",
    "🇳🇱", "🇩🇲", "🇪🇨", "🇪🇬", "🇸🇻", "🇬🇮", "🇬🇷", "🇬🇵", "🇬🇪", "🇬🇹", "🇭🇳", "🇭🇰", "🇭🇺", "🇮🇸",
    "🇮🇩", "🇮🇪", "🇮🇱", "🇮🇳", "🇮🇶", "🇮🇹", "🇨🇮", "🇯🇲", "🇯🇵", "🇯🇪", "🇯🇴", "🇰🇿", "🇰🇪", "🇰🇮",
    "🇰🇼", "🇰🇬", "🇱🇦", "🇱🇻", "🇱🇧", "🇱🇸", "🇱🇷", "🇱🇾", "🇱🇹", "🇱🇺", "🇲🇰", "🇲🇬", "🇲🇾", "🇲🇻",
    "🇲🇱", "🇲🇹", "🇲🇷", "🇲🇺", "🇲🇽", "🇲🇩", "🇲🇳", "🇲🇲", "🇲🇪", "🇳🇦", "🇳🇷", "🇳🇵", "🇳🇮", "🇳🇪",
    "🇳🇬", "🇳🇴", "🇴🇲", "🇵🇰", "🇵🇦", "🇵🇬", "🇵🇾", "🇵🇪", "🇵🇭", "🇵🇱", "🇵🇹", "🇵🇷", "🇶🇦", "🇷🇴",
    "🇷🇺", "🇷🇼", "🇼🇸", "🇸🇲", "🇸🇦", "🇷🇸", "🇸🇨", "🇸🇱", "🇸🇬", "🇸🇰", "🇸🇮", "🇬🇸", "🇸🇴", "🇿🇦",
    "🇰🇷", "🇸🇸", "🇪🇸", "🇱🇰", "🇸🇭", "🇰🇳", "🇵🇲", "🇻🇨", "🇸🇩", "🇸🇷", "🇸🇿", "🇸🇪", "🇨🇭", "🇸🇾",
    "🇹🇼", "🇹🇯", "🇹🇲", "🇹🇬", "🇹🇭", "🇹🇱", "🇹🇳", "🇹🇷", "🇹🇷", "🇹🇻", "🇺🇦", "🇦🇪", "🇬🇧", "🇹🇿",
    "🇺🇬", "🇺🇾", "🇺🇿", "🇻🇦", "🇻🇨", "🇻🇪", "🇻🇳", "🇾🇪", "🇿🇲", "🇿🇼"
};

            var LangList = new List<Languages>();
            for (int i = 0; i < LanguagesInput.Length; i++)
            {
                Languages newLang = new Languages();

                newLang.LanguageName = LanguagesInput[i];
                newLang.FlagUniCode = FlagUnicode[i];
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
            "Supernatural","Thriller", "Tragedy", "Wuxia", "Xianxia", "Xuanhuan", "Yaoi / Gay ", "Yuri / Lesbian", "Harem",
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

                "Appearance Changes", "Appearance Different from Actual Age", "Appearance Different from Personality",
                "Aristocrat", "Armed Combat", "Arranged Marriage ", "Arrogant Female Lead",

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

                "Female Demographic with Male Lead", "Female Dominance", "Female Fighters","Female Lead", "Female Lead Falls in Love First",
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
                "LGBT Parent", "LGBT Scenes", "Lifestyle Change", "Live-in Lover", "Loneliness", "Loner Protagonist", "Lolicon", "Love at First Sight",

           "Love Confession", "Love Interests Who Don't Get Along", "Love Polygon", "Love Triangle", "Love-Hate Relationship",
                "Lust", "Mafia", "Magic", "Magic Schoo", "Magical Creature", "Male Lead", "Magical Girl", "Magical Boy",
              "Maid", "Male Demographic with Female Lead", "Male Lead Falls in Love First", "Male Protagonist", "Female Protagonist",
                "Manipulation", "Manly Gay Couple", "Marriage", "Marriage Proposal", "Married Couple", "Masculine Uke", "Masochist",

           "Master-Pet Relationship", "Master-Servant Relationship", "Masturbation", "Mature Child", "Middle School", "Military", "Mind Break", "Mind Control",
                "Misunderstanding", "Model", "Misunderstood Protagonist","Middle-earth", "Models", "Modeling", "Monsters",

                 "Multiple Couples", "Multiple Protagonists", "Murder", "Music", "Music Band",
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

             "Sexual Assault", "Sexual Innuendo", "Shameless Female Lead", "Shinigami", "Short Chapter","Shotacon",
                "Short Female Lead", "Short Male Lead", "Showbiz", "Shy Female Lead", "Shy Protagonist", "Sibling", "Single Parent",

           "Sister Complex", "Sister", "Slaves", "Slow Romance", "Small Breasts", "Smart Female Lead", "Smart Male Lead"
           , "Smoking", "Social Hierarchy", "Social Outcast", "Special Ability", "Spirit",
              "Split Personality", "Stalker", "Stepsibling Love", "Stepsiblings", "Stolen Kiss",
                "Straight Seme", "Straight Uke", "Strategic Minds", "Strong Female Lead", "Strong Male Lead",
                "Student Council", "Student-Student Relationship",

           "Student-Teacher Relationship", "Student", "Subtle Romance", "Sudden Appearance", "Suicide", "SuperHeroes",
                "SuperPowers", "Survival", "Swimsuit", "Sword and Sorcery", "Swordplay", "Swordsman",

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

        public async Task<UserModel> CreateAndAddRoleToUserAsync(string userName, string email, string alias, string password, string role, string photoPath)
        {
            var user = new UserModel
            {
                UserName = userName,
                Email = email,
                Allias = alias,
                userPhotoPath = photoPath,
                CreatedAcc = DateTime.UtcNow
                // UserSettings is not initialized here anymore
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);

                // Now the user is created and has an ID, we can create UserSettings
                var userSettings = new UserSettings
                {
                    UserModelId = user.Id, // Assign the newly created User ID
                    ShowMatureContent = false,
                    DarkModeEnabled = false,
                    FontSize = 14,
                    ItemsPerPage = 20,
                    ReadingDirection = MangaReadingDirection.LeftToRight,
                    // Initialize other properties and collections if necessary
                };

                // Assign the UserSettings to the user
                user.UserSettings = userSettings;

                // Save UserSettings to the database
                context.UserSettings.Add(userSettings);
                await context.SaveChangesAsync();
            }
            return user;
        }

        #endregion TagSeed

        public async Task RoleCreating()
        {
            int rolesCount = roleManager.Roles.Count();
            // creat an array loop our array add all our roles to the database
            string[] roles = { "Admin", "NormalUser", "Owner", "AdminControl", "Updater",
                    "ShadowBanned", "Scanlators", "ForumAdmin", "Author", "Artist", "Publisher",
                    "Marketing", "SearchEngineOS", "FrontEndDeveloper", "SupportGroups", "BackEndDeveloper", "ProjectManager", "Tester", "DELETEDUSER" };
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

        public async Task ReviewSeed()
        {
            // Helper method to fetch Manga by name
            async Task<MangaModel> GetOrCreateMangaAsync(string mangaName)
            {
                var manga = await context.mangaModels.FirstOrDefaultAsync(e => e.MangaName == mangaName);
                if (manga == null)
                {
                    manga = new MangaModel { MangaName = mangaName };
                    context.mangaModels.Add(manga);
                    await context.SaveChangesAsync();
                }
                return manga;
            }

            // Helper method to create and add a review to the context
            void CreateReview(double stylesScore, double storyScore, double grammarScore, double charactersScore, string title, string content, UserModel user, MangaModel manga)
            {
                var review = new Review
                {
                    StylesScore = stylesScore,
                    StoryScore = storyScore,
                    GrammarScore = grammarScore,
                    CharactersScore = charactersScore,
                    Created = DateTime.Now,
                    Title = title,
                    Content = content,
                    UserModels = new List<UserModel>() { user },
                    MangaModels = new List<MangaModel>() { manga }
                };
                context.Reviews.Add(review);
            }

            // Helper method to create a user and add it to a role, now including photo path

            // Ensuring manga exist or are created
            var berserk = await GetOrCreateMangaAsync("Berserk");
            var soloLeveling = await GetOrCreateMangaAsync("Solo Leveling");
            var towerOfGod = await GetOrCreateMangaAsync("Tower of God");
            var sailorMoon = await GetOrCreateMangaAsync("Sailor Moon");
            var Testbook = await GetOrCreateMangaAsync("ThisIsACharacterTestToCheckIfATitleCouldbeLongerThanItShouldBePossibleToViewWhenWatchingThisFromANormal");

            // Seed user reviews
            var testUser = await context.UserModels.FirstOrDefaultAsync(u => u.UserName == "TestUser");
            if (testUser == null)
            {
                // Create a default user if not exists with a role (Adjust the photo path as needed)
                string defaultUserPhotoPath = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
                testUser = await CreateAndAddRoleToUserAsync("TestUser", "testuser@example.com", "TestAlias", "TestPassword123!", "Owner", defaultUserPhotoPath);
            }

            CreateReview(4.5, 4, 4.5, 5, "Best Manga EVER", "Berserk is a dark and gritty masterpiece." +
                " The art style is incredibly detailed, and the story is intense and captivating." +
                " The characters are complex and well-developed. This series is a must-read for fans of dark fantasy.", testUser, berserk);
            CreateReview(4.5, 5, 4.5, 5, "Solo Best", "Solo Leveling is an absolute thrill ride!" +
                " The art is top-notch, with incredibly detailed action scenes that leave you in awe. " +
                "The story is gripping, filled with intense moments and unexpected twists. The characters " +
                "are well-developed and have unique abilities. If you're a fan of action-packed manga, Solo Leveling is a must-read!", testUser, soloLeveling);
            CreateReview(4.5, 3.0, 3.5, 4.5, "Tower Of The G", "Tower of God is an epic adventure with a unique premise." +
                " The art style may take some getting used to, but the story is engaging and full of twists." +
                " The characters are diverse and intriguing. It's a series that keeps you guessing and wanting more.", testUser, towerOfGod);
            CreateReview(4.5, 3.0, 3.5, 4.5, "Tower Of The G", "Tower of God is an epic adventure with a unique premise." +
               " The art style may take some getting used to, but the story is engaging and full of twists." +
               " The characters are diverse and intriguing. It's a series that keeps you guessing and wanting more.", testUser, Testbook);
            await context.SaveChangesAsync();

            // Seed user2 reviews
            string filePath2 = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
            var testUser2 = await CreateAndAddRoleToUserAsync("TestUser2", "testuser2@example.com", "WorldEater", "Password123!", "Updater", filePath2);
            CreateReview(2.5, 2, 2.4, 2.9, "Mind-Blowing Action", "Solo Leveling is a highly addictive webtoon. The art is stunning," +
                " and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed," +
                " and the pacing is great. It's a must-read for fans of the genre.", testUser2, soloLeveling);
            CreateReview(2.5, 2, 2.4, 2.9, "Mind-Blowing Action", "Solo Leveling is a highly addictive webtoon. The art is stunning," +
               " and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed," +
               " and the pacing is great. It's a must-read for fans of the genre.", testUser2, Testbook);
            CreateReview(1.5, 1, 1.4, 4.9, "SailorD", "Sailor Miss!...", testUser2, sailorMoon);
            await context.SaveChangesAsync();
            // Seed user3 reviews
            string filePath3 = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
            var testUser3 = await CreateAndAddRoleToUserAsync("TestUser3", "testuser3@example.com", "Fishmudsappet", "Password123!", "NormalUser", filePath3);
            CreateReview(4.5, 3, 2.4, 4.9, "MySalor", "Sailor Miss!...", testUser3, sailorMoon);
            CreateReview(4.5, 3, 2.4, 4.9, "MySalor", "Sailor Miss!...", testUser3, Testbook);

            await context.SaveChangesAsync();
            //testuser4
            string filePath4 = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
            var testUser4 = await CreateAndAddRoleToUserAsync("TestUser4", "testuser4@example.com", "Fishm44uppet", "Password123!", "NormalUser", filePath4);
            CreateReview(2.5, 2, 2.4, 2.9, "Mind-Blowing Action", "Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed," +
             " and the pacing is great. It's a must-read for fans of the genre. Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. sdsadasdsadasdsa", testUser4, Testbook);
            await context.SaveChangesAsync();
            //testuser5
            string filePath5 = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
            var testUser5 = await CreateAndAddRoleToUserAsync("TestUser5", "testuser5@example.com", "Fishmup55pest", "Password123!", "NormalUser", filePath5);
            CreateReview(2.5, 2, 2.4, 2.9, "Mind-Blowing Action", "Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed," +
             " and the pacing is great. It's a must-read for fans of the genre. Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. sdsadasdsadasdsa", testUser5, Testbook);
            await context.SaveChangesAsync();
            string filePath6 = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
            var testUser6 = await CreateAndAddRoleToUserAsync("TestUser6", "testuser6@example.com", "Fishmup66pest", "Password123!", "NormalUser", filePath6);
            CreateReview(2.5, 2, 2.4, 2.9, "Mind-Blowing Action", "Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed," +
             " and the pacing is great. It's a must-read for fans of the genre. Solo Leveling is a highly addictive webtoon. The art is stunning," +
             " Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. The art is stunning," +
             "Solo Leveling is a highly addictive webtoon. The art is stunning, Solo Leveling is a highly addictive webtoon. sdsadasdsadasdsa", testUser6, Testbook);
            await context.SaveChangesAsync();
        }

        #endregion CommonSeed

        #region SeedData

        public void wallpaperSeed()
        {
            WallPapers wallPapers = new WallPapers
            {
                WallPaperPhotoPath = Path.Combine(WallProcessUploadedFile("AllAnime.jpg"))
            };
            WallPapers wallPapers1 = new WallPapers
            {
                WallPaperPhotoPath = Path.Combine(WallProcessUploadedFile("AllAnime2.jpg"))
            };
            WallPapers wallPapers2 = new WallPapers
            {
                WallPaperPhotoPath = Path.Combine(WallProcessUploadedFile("AllAnime3.jpeg"))
            };
            WallPapers wallPapers3 = new WallPapers
            {
                WallPaperPhotoPath = Path.Combine(WallProcessUploadedFile("AllAnime4.jpg"))
            };
            WallPapers wallPapers4 = new WallPapers
            {
                WallPaperPhotoPath = Path.Combine(WallProcessUploadedFile("AboutMeWall.jpg"))
            };

            context.WallPapers.Add(wallPapers);
            context.WallPapers.Add(wallPapers1);
            context.WallPapers.Add(wallPapers2);
            context.WallPapers.Add(wallPapers3);
            context.WallPapers.Add(wallPapers4);

            context.SaveChanges();
        }

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
                wallpaperSeed();
                //photopath needs fixing real bad
                string filePath = Path.Combine(UserProfileProcessUploadedFile("NoPhoto.png"));
                // creates a user,

                var user = await CreateAndAddRoleToUserAsync("TestUser", "TestUser@hotmail.com", "MasterUser", "Password123!", "Owner", filePath);

                context.SaveChanges();

                AddBerserk();
                CreateNaruto();
                CreateBuroto();
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
                CreateMyHeroAcademia();
                CreateAirGear();
                CreateBleach();
                CreateCodeGeass();
                DonQuiXote();
                RelatedManga();
                UltraLongBOokTester();
                CreateRelated();
                await ReviewSeed();
            }
        }

        #endregion SeedData

        public void CreateRelated()
        {
            var CharBoruto = context.Characters
                        .Include(c => c.MangaModels)
                        .FirstOrDefault(c => c.CharacterName == "Uzumaki Boruto");
            var NarutoChar = context.Characters.FirstOrDefault(e => e.CharacterName == "Uzumaki Naruto");

            var Uchia = context.Characters.FirstOrDefault(e => e.CharacterName == "Uchiha Sasuke");
            var Kakashi = context.Characters.FirstOrDefault(e => e.CharacterName == "Hatake Kakashi");
            var Sakura = context.Characters.FirstOrDefault(e => e.CharacterName == "Haruno Sakura");
            var Guts = context.Characters.FirstOrDefault(e => e.CharacterName == "Guts");
            var Griffith = context.Characters.FirstOrDefault(e => e.CharacterName == "Griffith");
            var Casca = context.Characters.FirstOrDefault(e => e.CharacterName == "Casca");
            var Puck = context.Characters.FirstOrDefault(e => e.CharacterName == "Puck");
            var Zodd = context.Characters.FirstOrDefault(e => e.CharacterName == "Zodd the Immortal");
            var Luffy = context.Characters.FirstOrDefault(e => e.CharacterName == "Monkey D. Luffy");
            var Ace = context.Characters.FirstOrDefault(e => e.CharacterName == "Portgas D. Ace");
            var Newgate = context.Characters.FirstOrDefault(e => e.CharacterName == "Edward Newgate (Whitebeard)");
            var Edward = context.Characters.FirstOrDefault(e => e.CharacterName == "Edward Elric");
            var Alphonse = context.Characters.FirstOrDefault(e => e.CharacterName == "Alphonse Elric");
            var Winry = context.Characters.FirstOrDefault(e => e.CharacterName == "Winry Rockbell");
            var eren = context.Characters.FirstOrDefault(e => e.CharacterName == "Eren Yeager");
            var Mikasa = context.Characters.FirstOrDefault(e => e.CharacterName == "Mikasa Ackerman");
            var Ichigo = context.Characters.FirstOrDefault(e => e.CharacterName == "Ichigo Kurosaki");
            var Orihime = context.Characters.FirstOrDefault(e => e.CharacterName == "Orihime Inoue");
            var Kudo = context.Characters.FirstOrDefault(e => e.CharacterName == "Shinichi Kudo (Conan Edogawa)");
            var CharConan = context.Characters.FirstOrDefault(e => e.CharacterName == "Conan Edogawa (Shinichi Kudo)");

            var AuthorFullmetal = context.authorModels.FirstOrDefault(e => e.FirstName == "Hiromu");
            var AuthorDeathNote = context.authorModels.FirstOrDefault(e => e.FirstName == "Tsugumi");
            var AuthorNaruto = context.authorModels.FirstOrDefault(e => e.FirstName == "");
            var AuthorBoruto = context.authorModels.FirstOrDefault(e => e.FirstName == "");
            var AuthorBerserk = context.authorModels.FirstOrDefault(e => e.FirstName == "");
            var Naruto = context.mangaModels
                .Include(m => m.relatedSeries)
                .Include(m => m.Characters)
                .Include(r => r.RecommendedMangaModels)
                .FirstOrDefault(n => n.MangaName == "Naruto");

            var Berserk = context.mangaModels
               .Include(m => m.relatedSeries)
               .Include(m => m.Characters)
               .Include(r => r.RecommendedMangaModels)
               .FirstOrDefault(n => n.MangaName == "Berserk");
            var OnePiece = context.mangaModels
          .Include(m => m.relatedSeries)
          .Include(m => m.Characters)
          .Include(r => r.RecommendedMangaModels)
          .FirstOrDefault(n => n.MangaName == "One Piece");

            var FullMetal = context.mangaModels
   .Include(m => m.relatedSeries)
   .Include(m => m.Characters)
   .Include(r => r.RecommendedMangaModels)
   .FirstOrDefault(n => n.MangaName == "Fullmetal Alchemist");
            var Conan = context.mangaModels
   .Include(m => m.relatedSeries)
   .Include(m => m.Characters)
   .Include(r => r.RecommendedMangaModels)
   .FirstOrDefault(n => n.MangaName == "Detective Conan");
            var Bleach = context.mangaModels
   .Include(m => m.relatedSeries)
   .Include(m => m.Characters)
   .Include(r => r.RecommendedMangaModels)
   .FirstOrDefault(n => n.MangaName == "Bleach");
            var Titan = context.mangaModels
.Include(m => m.relatedSeries)
.Include(m => m.Characters)
.Include(r => r.RecommendedMangaModels)
.FirstOrDefault(n => n.MangaName == "Attack on Titan");

            var Boruto = context.mangaModels
                .Include(m => m.relatedSeries)
                .Include(m => m.Characters)
                 .Include(r => r.RecommendedMangaModels)
                .FirstOrDefault(n => n.MangaName == "Boruto");

            var Solo = context.mangaModels
.Include(m => m.relatedSeries)
.Include(m => m.Characters)
.Include(r => r.RecommendedMangaModels)
.FirstOrDefault(n => n.MangaName == "Solo Leveling");
            var Testbook = context.mangaModels
.Include(m => m.relatedSeries)
.Include(m => m.Characters)
.Include(r => r.RecommendedMangaModels)
.FirstOrDefault(n => n.MangaName == "ThisIsACharacterTestToCheckIfATitleCouldbeLongerThanItShouldBePossibleToViewWhenWatchingThisFromANormal");
            var Death = context.mangaModels
       .Include(m => m.relatedSeries)
       .Include(m => m.Characters)
       .Include(r => r.RecommendedMangaModels)
       .FirstOrDefault(n => n.MangaName == "Death Note");

            var Hero = context.mangaModels
       .Include(m => m.relatedSeries)
       .Include(m => m.Characters)
       .Include(r => r.RecommendedMangaModels)
       .FirstOrDefault(n => n.MangaName == "My Hero Academia");
            var Air = context.mangaModels
       .Include(m => m.relatedSeries)
       .Include(m => m.Characters)
       .Include(r => r.RecommendedMangaModels)
       .FirstOrDefault(n => n.MangaName == "Air Gear");

            AuthorFullmetal.MangaModels = new List<MangaModel> { FullMetal };
            AuthorDeathNote.MangaModels = new List<MangaModel> { Death };
            Naruto.relatedSeries = new List<MangaModel> { Boruto, Testbook };
            Testbook.relatedSeries = new List<MangaModel> { Conan, OnePiece, Bleach, FullMetal, Berserk, Titan, Boruto, Death, Hero, Air, Solo };
            Naruto.RecommendedMangaModels = new List<MangaModel> { OnePiece, Bleach, FullMetal, Berserk, Titan, Boruto, Testbook };
            Berserk.RecommendedMangaModels = new List<MangaModel> { Naruto, FullMetal, Titan };
            OnePiece.RecommendedMangaModels = new List<MangaModel> { Bleach, FullMetal, Berserk, Titan, Boruto };
            Testbook.RecommendedMangaModels = new List<MangaModel> { Conan, OnePiece, Bleach, FullMetal, Berserk, Titan, Boruto, Death, Hero, Air, Solo };
            FullMetal.RecommendedMangaModels = new List<MangaModel> { Naruto, Titan, Air, Hero, Solo };
            Conan.RecommendedMangaModels = new List<MangaModel> { Death };
            Air.RecommendedMangaModels = new List<MangaModel> { FullMetal, Naruto };

            Solo.RecommendedMangaModels = new List<MangaModel> { Death, Berserk, Hero, Bleach, FullMetal };
            Bleach.RecommendedMangaModels = new List<MangaModel> { Naruto, Hero, Solo, Air, FullMetal };
            Titan.RecommendedMangaModels = new List<MangaModel> { Naruto, Berserk, Death, Solo };
            NarutoChar.Family = new List<Character> { CharBoruto };
            Uchia.Family = new List<Character> { Sakura };
            Sakura.Family = new List<Character> { Uchia };
            Guts.Family = new List<Character> { Casca, Puck };
            Casca.Family = new List<Character> { Guts };
            Griffith.Family = new List<Character> { Zodd };
            Puck.Family = new List<Character> { Guts };
            Zodd.Family = new List<Character> { Griffith };
            Luffy.Family = new List<Character> { Ace, Newgate };
            Newgate.Family = new List<Character> { Ace };
            Ace.Family = new List<Character> { Luffy, Newgate };
            Alphonse.Family = new List<Character> { Edward, Winry };
            Winry.Family = new List<Character> { Alphonse, Edward };
            Edward.Family = new List<Character> { Alphonse, Winry };
            eren.Family = new List<Character> { Mikasa };
            Mikasa.Family = new List<Character> { eren };
            Ichigo.Family = new List<Character> { Orihime };
            Orihime.Family = new List<Character> { Ichigo };
            Kudo.Family = new List<Character> { CharConan };
            CharConan.Family = new List<Character> { Kudo };

            context.SaveChanges();
        }

        public void AddBerserk()
        {
            var Tag1ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Dark Fantasy");
            var Tag2ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Blood and Gore");
            var Tag3ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Sword and Sorcery");
            var Tag4ToManga2 = context.TagModels.FirstOrDefault(t => t.TagName == "Violence");

            var Gen1ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Horror");
            var Gen3ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Tragedy");
            var Gen4ToManga2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Mature");

            MangaModel Berserk = new MangaModel
            {
                MangaName = "Berserk",
                PhotoPath = Path.Combine(ProcessUploadedFile("Berserk.jpg")),
                ReleaseYear = new DateTime(1989, 8, 25),
                BlogModel = new BlogModel { mangaName = "Berserk" },
                Description = "Berserk follows the character of Guts, a lone mercenary warrior, as he travels a medieval-inspired world of dark fantasy in search of revenge against his former friend and ally, who betrayed him and sacrificed their comrades to demons in order to become one himself.",
                CompletelyTranslated = "No",
                OfficalLanguage = "Japanese",
                ISBN10 = "1593070209",
                ISBN13 = "978-1593070205",

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
    new AuthorModel
    {
        FirstName = "Kentaro",
        LastName = "Miura",
        Biography = "Renowned manga artist best known for creating the epic dark fantasy series Berserk.",
        BirthPlace = "Chiba, Japan",
        Gender = "Male",
        AuthorBorn = new DateTime(1966, 7, 11),
        AuthorDeath = new DateTime(2021, 5, 6),
        AmountOfWork = 1, // Assuming Berserk as the primary work
        WorkingAt = "Hakusensha",
        Twitter = null,
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Kentaro_Miura",
    }
},

                ArtistModels = new List<ArtistModel>
{
    new ArtistModel
    {
        FirstName = "Kentaro",
        LastName = "Miura",
        Biography = "Kentaro Miura was a Japanese manga artist, best known for his magnum opus Berserk, which started serialization in 1989 and continued until his death in 2021.",
        BirthPlace = "Chiba, Japan",
        Gender = "Male",
        ArtistBorn = new DateTime(1966, 7, 11),
        ArtistDeath = new DateTime(2021, 5, 6),
        AmountOfWork = 1, // Assuming Berserk as the primary work
        WorkingAt = "Hakusensha",
        Twitter = null,
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Kentaro_Miura",
    }
},

                VoiceActors = new List<VoiceActorModel>
{
    new VoiceActorModel
    {
        FirstName = "Canna",
        LastName = "Nobutoshi",
        Gender = "Male",
        Biography = "A Japanese voice actor known for his versatile roles in anime and video games.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1968, 6, 10),
        Twitter = "@CannaNobutoshi",
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Nobutoshi_Canna",
        AmountOfWork = 100,
        WorkingAt = "Freelance",
    },
    new VoiceActorModel
    {
        FirstName = "Kenji",
        LastName = "Utsumi",
        Gender = "Male",
        Biography = "Late Japanese voice actor renowned for his deep, powerful voice and iconic roles.",
        BirthPlace = "Kitakyushu, Japan",
        VoiceBorn = new DateTime(1937, 8, 26),
        VoiceDeath = new DateTime(2013, 6, 13),
        Twitter = null,
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Kenji_Utsumi",
        AmountOfWork = 150,
        WorkingAt = "Aoni Production",
    },
    new VoiceActorModel
    {
        FirstName = "Toshiyuki",
        LastName = "Morikawa",
        Gender = "Male",
        Biography = "A prominent Japanese voice actor known for his roles in major anime series and films.",
        BirthPlace = "Kawasaki, Kanagawa, Japan",
        VoiceBorn = new DateTime(1967, 1, 26),
        Twitter = "@ToshiyukiMorikawa",
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Toshiyuki_Morikawa",
        AmountOfWork = 200,
        WorkingAt = "Freelance",
    },
    new VoiceActorModel
    {
        FirstName = "Yuko",
        LastName = "Miyamura",
        Gender = "Female",
        Biography = "A Japanese actress, voice actress, and singer, known for her work in anime and video games.",
        BirthPlace = "Kobe, Hyogo, Japan",
        VoiceBorn = new DateTime(1972, 12, 4),
        Twitter = "@YukoMiyamura",
        reddit = null,
        WikiPedia = "https://en.wikipedia.org/wiki/Yūko_Miyamura",
        AmountOfWork = 120,
        WorkingAt = "Freelance",
    },
},
                GenresModels = new List<GenresModel>
                {
                    Gen1ToManga2,
                    Gen2ToManga2,
                    Gen3ToManga2,
                    Gen4ToManga2
                },
                TagsModels = new List<TagModel>
                {
                    Tag1ToManga2,
                    Tag2ToManga2,
                    Tag3ToManga2,
                    Tag4ToManga2
                },
                Characters = new List<Character>
{
  new Character
    {
        CharacterName = "Guts",
        specie = "Human",
        Gender = "Male",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Self-taught",
        Occupation = "Mercenary, Swordsman, Leader",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Guts.jpg")),
        Personality = "Determined, Stoic, Fierce",
        FamousQuote = "I will fight. That's who I am.",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Exceptional swordsmanship, Indomitable will",
        Hobbies = "Sword training, Protecting loved ones",
        Likes = "Freedom, Battle",
        Dislikes = "Demons, Betrayal",
        PersonalityTraits = "Resilient, Independent, Protective"
    },
    new Character
    {
        CharacterName = "Griffith",
        specie = "Human",
        Gender = "Male",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Founder and leader of the Band of the Hawk",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Griffith.jpg")),
        Personality = "Charismatic, Ambitious, Enigmatic",
        FamousQuote = "I will have my own kingdom. And I will name it Falconia.",
        EyeColor = "Violet",
        HairColor = "White",
        Hobbies = "Strategizing, Aspiring to greatness",
        Likes = "Destiny, Achieving his dream",
        Dislikes = "Weakness, Setbacks",
        PersonalityTraits = "Calculated, Visionary, Determined"
    },
    new Character
    {
        CharacterName = "Casca",
        specie = "Human",
        Gender = "Female",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Commander of the Band of the Hawk",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Casca.jpeg")),
        Personality = "Brave, Loyal, Complex",
        FamousQuote = "Even if it's scary, even if it's painful, I don't want to be weak anymore.",
        EyeColor = "Brown",
        HairColor = "Black",
        Hobbies = "Swordsmanship, Protecting loved ones",
        Likes = "Griffith, Overcoming challenges",
        Dislikes = "Vulnerability, Trauma",
        PersonalityTraits = "Caring, Determined, Scarred"
    },
    new Character
    {
        CharacterName = "Zodd the Immortal",
        specie = "Unknown",
        Gender = "Male",
        Born = "Unknown",
        PlaceOffResidence = "Unknown",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Mercenary",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Zodd.jpg")),
        Personality = "Ferocious, Mysterious, Mighty",
        FamousQuote = "This is the battlefield where humans and apostles meet!",
        Abilities = "Superhuman strength, Immortality",
        Hobbies = "Battle, Testing his might",
        Likes = "Chaos, Fighting strong opponents",
        Dislikes = "Weakness, Submission",
        PersonalityTraits = "Savage, Unyielding, Ruthless"
    },
   new Character
    {
        CharacterName = "Puck",
        specie = "Elf",
        Gender = "Male",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Comic relief character",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Puck.jpg")),
        Personality = "Lively, Cheerful, Curious",
        FamousQuote = "I'm Puck, a wandering elf! Nice to meet you, Mr. Ogre!",
        EyeColor = "Blue",
        HairColor = "Green",
        Hobbies = "Adventuring, Teasing Guts",
        Likes = "Fun, Kind-hearted people",
        Dislikes = "Suffering, Evil beings",
        PersonalityTraits = "Playful, Helpful, Inquisitive"
    },
    new Character
    {
        CharacterName = "Farnese de Vandimion",
        specie = "Human",
        Gender = "Female",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Vandimion family",
        Education = "Unknown",
        Occupation = "Leader of the Holy See's Chain Knights",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Farnese.jpg")),
        Personality = "Resolute, Seeking, Conflicted",
        FamousQuote = "I will find my own path.",
        EyeColor = "Brown",
        HairColor = "Blonde",
        Hobbies = "Religious devotion, Seeking redemption",
        Likes = "Authority, Purpose",
        Dislikes = "Doubt, Weakness",
        PersonalityTraits = "Determined, Devoted, Complex"
    },
    new Character
    {
        CharacterName = "Serpico",
        specie = "Human",
        Gender = "Male",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Member of the Holy See's Chain Knights",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Serpico.jpg")),
        Personality = "Calm, Observant, Loyal",
        FamousQuote = "It's just my personality to follow behind like a shadow.",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Hobbies = "Swordsmanship, Protecting Farnese",
        Likes = "Nature, Strategy",
        Dislikes = "Arrogance, Danger to Farnese",
        PersonalityTraits = "Reserved, Dedicated, Analytical"
    },
    new Character
    {
        CharacterName = "Ivalera",
        specie = "Elf",
        Gender = "Female",
        Born = "Unknown",
        PlaceOffResidence = "Midland",
        World = "Berserk",
        Nationality = "Unknown",
        Education = "Unknown",
        Occupation = "Elf guide and spiritual guide to Schierke",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Ivalera.jpg")),
        Personality = "Wisdom, Ethereal, Supportive",
        EyeColor = "Blue",
        HairColor = "Green",
        Hobbies = "Guiding, Offering insight",
        Likes = "Spiritual growth, Balance",
        Dislikes = "Destruction, Corruption",
        PersonalityTraits = "Sage-like, Tranquil, Nurturing"
    },
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
    new AuthorModel
    {
        FirstName = "Masashi",
        LastName = "Kishimoto",
        Biography = "Japanese manga artist, best known for creating the Naruto series which has become one of the best-selling manga series in history.",
        BirthPlace = "Nagasaki, Japan",
        Gender = "Male",
        AuthorBorn = new DateTime(1974, 11, 8),
        AmountOfWork = 2, // Naruto and Boruto
        WorkingAt = "Shueisha",
        Twitter = "@Masashi_Kishimoto",
        NameInNative = "岸本 斉史",
        Contact = "info@shueisha.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Masashi_Kishimoto",
    }
},
                ArtistModels = new List<ArtistModel>
{
    new ArtistModel
    {
        FirstName = "Masashi",
        LastName = "Kishimoto",
        Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. Known for his work on Naruto, which has been adapted into anime, movies, and video games.",
        BirthPlace = "Nagasaki, Japan",
        Gender = "Male",
        ArtistBorn = new DateTime(1974, 11, 8),
        AmountOfWork = 2, // Assuming Naruto and Boruto
        WorkingAt = "Shueisha",
        Twitter = "@Masashi_Kishimoto",
        NameInNative = "岸本 斉史",
        Contact = "info@shueisha.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Masashi_Kishimoto",
    }
},
                VoiceActors = new List<VoiceActorModel>
{
    new VoiceActorModel
    {
        FirstName = "Nakamura",
        LastName = "Chie",
        Gender = "Female",
        Biography = "Japanese voice actress known for voicing Sakura Haruno in Naruto.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1979, 5, 14),
        Twitter = "@NakamuraChie",
        NameInNative = "中村 千絵",
        Contact = "chie@voiceagency.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Chie_Nakamura",
        AmountOfWork = 100, // Hypothetical number of works
        WorkingAt = "Freelance",
    },
    new VoiceActorModel
    {
        FirstName = "Inoue",
        LastName = "Kazuhiko",
        Gender = "Male",
        Biography = "Veteran Japanese voice actor noted for his role as Kakashi Hatake in Naruto.",
        BirthPlace = "Yokohama, Japan",
        VoiceBorn = new DateTime(1954, 3, 26),
        Twitter = "@InoueKazuhiko",
        NameInNative = "井上 和彦",
        Contact = "kazuhiko@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Kazuhiko_Inoue",
        AmountOfWork = 200, // Hypothetical number of works
        WorkingAt = "Bungakuza",
    },
    new VoiceActorModel
    {
        FirstName = "Takeuchi",
        LastName = "Junko",
        Gender = "Female",
        Biography = "Famous Japanese voice actress known for her role as Naruto Uzumaki in Naruto.",
        BirthPlace = "Saitama, Japan",
        VoiceBorn = new DateTime(1972, 4, 5),
        Twitter = "@JunkoTakeuchi",
        NameInNative = "竹内 順子",
        Contact = "junko@voicepro.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Junko_Takeuchi",
        AmountOfWork = 150, // Hypothetical number of works
        WorkingAt = "Freelance",
    },
    new VoiceActorModel
    {
        FirstName = "Sugiyama",
        LastName = "Noriaki",
        Gender = "Male",
        Biography = "Japanese voice actor best known for his work as Sasuke Uchiha in Naruto.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1976, 3, 9),
        Twitter = "@SugiyamaNoriaki",
        NameInNative = "杉山 紀彰",
        Contact = "noriaki@voices.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Noriaki_Sugiyama",
        AmountOfWork = 120, // Hypothetical number of works
        WorkingAt = "Stay Luck",
    },
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
  new Character
    {
        CharacterName = "Uzumaki Naruto",
        specie = "Human",
        Gender = "Male",
        Born = "October 10",
        PlaceOffResidence = "Konoha",
        World = "Naruto",
        Nationality = "Japanese",
        Education = "Genin",
        Occupation = "Hokage",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("naruto.jpg")),
        Personality = "Energetic, Determined, Friendly",
        FamousQuote = "I'm not gonna run away, I never go back on my word!",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Hobbies = "Pranking, Eating ramen",
        Likes = "Ramen, Friends, Achieving his dreams",
        Dislikes = "Losing, Giving up",
        PersonalityTraits = "Optimistic, Tenacious, Compassionate"
    },
    new Character
    {
        CharacterName = "Uchiha Sasuke",
        specie = "Human",
        Gender = "Male",
        Born = "July 23",
        PlaceOffResidence = "Konoha",
        World = "Naruto",
        Nationality = "Japanese",
        Education = "Chunin",
        Occupation = "Ninja",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Sasuke.jpg")),
        Personality = "Reserved, Determined, Avenger",
        FamousQuote = "I have long since closed my eyes... My only goal is in the darkness.",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Training, Seeking power",
        Likes = "Strength, Revenge",
        Dislikes = "Weakness, Itachi",
        PersonalityTraits = "Focused, Independent, Driven"
    },
    new Character
    {
        CharacterName = "Hatake Kakashi",
        specie = "Human",
        Gender = "Male",
        Born = "September 15",
        PlaceOffResidence = "Konoha",
        World = "Naruto",
        Nationality = "Japanese",
        Education = "Anbu",
        Occupation = "Hokage",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Kakashi.jpeg")),
        Personality = "Calm, Mysterious, Relaxed",
        FamousQuote = "In the ninja world, those who break the rules are scum, that's true, but those who abandon their friends are worse than scum.",
        EyeColor = "Black",
        HairColor = "Silver",
        Hobbies = "Reading, Napping",
        Likes = "Icha Icha series, Team 7",
        Dislikes = "Wasting time, Pointless fights",
        PersonalityTraits = "Observant, Laid-back, Wise"
    },
    new Character
    {
        CharacterName = "Haruno Sakura",
        specie = "Human",
        Gender = "Female",
        Born = "March 28",
        PlaceOffResidence = "Konoha",
        World = "Naruto",
        Nationality = "Japanese",
        Education = "Chunin",
        Occupation = "Ninja",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("sakura.jpeg")),
        Personality = "Determined, Compassionate, Stubborn",
        FamousQuote = "I'm not a kid anymore. I've grown up. And that's why... I won't let you kill Naruto-kun!",
        EyeColor = "Green",
        HairColor = "Pink",
        Hobbies = "Medical jutsu, Training",
        Likes = "Sasuke, Inner strength",
        Dislikes = "Feeling weak, Seeing her friends hurt",
        PersonalityTraits = "Strong-willed, Caring, Resilient"
    },
                },
            };
            context.mangaModels.Add(MangaNaruto);
            context.SaveChanges();
        }

        public void CreateBuroto()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Ninja");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Adapted to Anime");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var NarutoRelated = context.mangaModels.FirstOrDefault(t => t.MangaName == "Naruto");
            var Author = context.authorModels.FirstOrDefault(a => a.FirstName == "Masashi" && a.LastName == "Kishimoto");
            var Artist = context.artistModels.FirstOrDefault(a => a.FirstName == "Masashi" && a.LastName == "Kishimoto");
            var NarutoCharacter = context.Characters.FirstOrDefault(c => c.CharacterName == "Uzumaki Naruto");
            //first
            DateTime myDate = DateTime.ParseExact("1991-05-08", "yyyy-MM-dd",
                                     System.Globalization.CultureInfo.InvariantCulture);
            DateTime started = DateTime.ParseExact("1979-05-08", "yyyy-MM-dd",
                                  System.Globalization.CultureInfo.InvariantCulture);
            MangaModel MangaBoruto = new MangaModel
            {
                MangaName = "Boruto",

                PhotoPath = Path.Combine(ProcessUploadedFile("Boruto.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Boruto" },
                Description = "A Kid who carries on his father's legacy",
                CompletelyTranslated = "Ongoing",
                OfficalLanguage = "Japanese",
                ISBN10 = "142158493X",
                ISBN13 = "978-1421584935",

                relatedSeries = new List<MangaModel> { NarutoRelated },
                EndingYear = null, // Assuming ongoing
                StatusInCountryOfOrigin = "Ongoing",
                Type = "Manga",
                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Pierrot", Started = started, StudioWebsite = "https://en.pierrot.jp/", Biography = "Pierrot Co., Ltd. is a Japanese animation studio." } },
                OriginalPublisher = "Shueisha",
                orignalWebtoon = "N/A",
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage> {
      new BuyPage { BuyWebsite = "https://www.amazon.com/Boruto-Vol-1-Naruto-Generations/dp/142158493X", _Languages = new List<Languages> { EnglishLanguage } },
      new BuyPage { BuyWebsite = "https://www.amazon.co.jp/-/en/Ukyo-Kodachi/dp/4088807230", _Languages = new List<Languages> { JapaneseLanguage } },
    },

                AssociatedNames = new List<AssociatedNames>
    {
        new AssociatedNames { nameString = "Boruto: Naruto Next Generations" },
    },
                OfficalWebsites = new List<OfficalWebsite>
    {
        new OfficalWebsite { OfficalWebsiteString = "https://boruto-official.com", OfficalWebsiteName = "Boruto-Official", Twitter = "https://twitter.com/BORUTO_info", Facebook = "https://www.facebook.com/BorutoOfficial", Line = "N/A" },
    },
                Authormodels = new List<AuthorModel>
    {
       { Author },
    },
                ArtistModels = new List<ArtistModel>
    {
        new ArtistModel { FirstName = "Mikio",
    LastName = "Ikemoto",
    Biography = "Apprentice of Masashi Kishimoto, Mikio Ikemoto took over the illustration duties for the Boruto series, continuing the legacy of the Naruto franchise.",
    BirthPlace = "Osaka, Japan",
    Gender = "Male",
    ArtistBorn = new DateTime(1975, 5, 8), // Hypothetical date
    AmountOfWork = 1, // Boruto
    WorkingAt = "Shueisha",
    Twitter = "@MikioIkemoto", // Hypothetical handle
    NameInNative = "池本 幹雄",
    Contact = "mikio@shueisha.co.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Mikio_Ikemoto"}, Artist
    },
                VoiceActors = new List<VoiceActorModel>
    {
        new VoiceActorModel {    FirstName = "Yuko",
    LastName = "Sanpei",
    Gender = "Female",
    Biography = "A Japanese voice actress known for her role as Boruto Uzumaki in Boruto: Naruto Next Generations.",
    BirthPlace = "Tokyo, Japan",
    VoiceBorn = new DateTime(1986, 2, 28), // Hypothetical date
    Twitter = "@YukoSanpei", // Hypothetical handle
    NameInNative = "三瓶 由布子",
    Contact = "yuko@voiceagency.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Yūko_Sanpei", // Hypothetical link
    AmountOfWork = 50, // Hypothetical number of works
    WorkingAt = "Freelance" },
        new VoiceActorModel { FirstName = "Kokoro",
    LastName = "Kikuchi",
    Gender = "Female",
    Biography = "Famous for voicing Sarada Uchiha in Boruto: Naruto Next Generations, Kokoro Kikuchi is a versatile voice actress contributing to the new generation of the Naruto franchise.",
    BirthPlace = "Hiroshima, Japan",
    VoiceBorn = new DateTime(1982, 12, 5), // Hypothetical date
    Twitter = "@KokoroKikuchi", // Hypothetical handle
    NameInNative = "菊池 こころ",
    Contact = "kokoro@voicepro.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Kokoro_Kikuchi", // Hypothetical link
    AmountOfWork = 40, // Hypothetical number of works
    WorkingAt = "Freelance" },
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
        new Character
        {
            CharacterName = "Uzumaki Boruto",
            specie = "Human",
            Gender = "Male",
            Born = "March 27",
            PlaceOffResidence = "Konoha",
            World = "Naruto",
            Background ="",
            Family = new List<Character> {NarutoCharacter },

            Nationality = "Japanese",
            Education = "Genin",
            Occupation = "Ninja",
            PhotoPath = Path.Combine(CharacterProcessUploadedFile("boruto.jpg")),
            Personality = "Energetic, Rebellious, Talented",
            FamousQuote = "I'm not going to choose the path my parents walked. I'm going to choose the path I walk!",
            EyeColor = "Blue",
            HairColor = "Blonde",
            Hobbies = "Video games, Training",
            Likes = "His friends, Adventures",
            Dislikes = "Being compared to his father, Cheating",
            PersonalityTraits = "Confident, Spirited, Independent"
        },
    },
            };
            context.mangaModels.Add(MangaBoruto);
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
            new Character {  CharacterName = "Harry Potter",
        specie = "Human",
        Gender = "Male",
        Born = "31 July 1980",
        PlaceOffResidence = "Hogwarts",
        World = "Wizarding World",
        Nationality = "British",
        Education = "Hogwarts School of Witchcraft and Wizardry",
        Occupation = "Wizard",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("harrypotter.jpg")),
        Personality = "Brave, Resourceful, Compassionate",
        FamousQuote = "I am a wizard, not a baboon brandishing a stick.",
        EyeColor = "Green",
        HairColor = "Black",
        Abilities = "Parseltongue, Skilled wizardry",
        Hobbies = "Quidditch, Defense Against the Dark Arts",
        Likes = "Friends, Magic, Quidditch",
        Dislikes = "Dark magic, Lord Voldemort",
        PersonalityTraits = "Determined, Selfless, Loyal" },
            new Character { CharacterName = "Ron Weasley",
        specie = "Human",
        Gender = "Male",
        Born = "1 March 1980",
        PlaceOffResidence = "Hogwarts",
        World = "Wizarding World",
        Nationality = "British",
        Education = "Hogwarts School of Witchcraft and Wizardry",
        Occupation = "Wizard",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Ron.jpg")),
        Personality = "Loyal, Humorous, Brave",
        FamousQuote = "She needs to sort out her priorities.",
        EyeColor = "Blue",
        HairColor = "Ginger",
        Hobbies = "Wizard chess, Playing the guitar",
        Likes = "Food, Quidditch, Adventures",
        Dislikes = "Spiders, Being overshadowed",
        PersonalityTraits = "Reliable, Caring, Relatable"},
            new Character {   CharacterName = "Hermione Granger",
        specie = "Human",
        Gender = "Female",
        Born = "19 September 1979",
        PlaceOffResidence = "Hogwarts",
        World = "Wizarding World",
        Nationality = "British",
        Education = "Hogwarts School of Witchcraft and Wizardry",
        Occupation = "Wizard",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Hermione.jpg")),
        Personality = "Intelligent, Diligent, Empathetic",
        FamousQuote = "Honestly, am I the only person who's ever bothered to read Hogwarts, A History?",
        EyeColor = "Brown",
        HairColor = "Brown",
        Hobbies = "Reading, Spell research",
        Likes = "Learning, Equal rights",
        Dislikes = "Unfairness, Ignorance",
        PersonalityTraits = "Knowledgeable, Determined, Compassionate"},
            new Character { CharacterName = "Albus Dumbledore",
        specie = "Human",
        Gender = "Male",
        Born = "1881",
        PlaceOffResidence = "Hogwarts",
        World = "Wizarding World",
        Nationality = "British",
        Education = "Hogwarts School of Witchcraft and Wizardry",
        Occupation = "Wizard",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("AlbusDumbledore.jpg")),
        Personality = "Wise, Kind, Enigmatic",
        FamousQuote = "Happiness can be found even in the darkest of times, if one only remembers to turn on the light.",
        EyeColor = "Blue",
        HairColor = "Silver",
        Hobbies = "Collecting magical artifacts, Strategizing",
        Likes = "Lemon drops, Sherbet lemons",
        Dislikes = "Dark magic, Power-hungry individuals",
        PersonalityTraits = "Visionary, Compassionate, Secretive"},
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
                PhotoPath = Path.Combine(ProcessUploadedFile("DeathNote.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Death Note" },
                Description = "A young high school student discovers a supernatural notebook that can kill anyone whose name is written in it.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "1421501686",
                ISBN13 = "978-1421501680",

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
  new AuthorModel {  FirstName = "Tsugumi",
    LastName = "Ohba",
    Biography = "Pseudonymous Japanese manga artist, Tsugumi Ohba is best known for creating the critically acclaimed series Death Note. Ohba's true identity remains a mystery, adding an intriguing layer to their work.",
    BirthPlace = "Tokyo, Japan", // Hypothetical, as Ohba's identity is undisclosed
    Gender = "Unknown", // Ohba's gender is not publicly disclosed
    AuthorBorn = new DateTime(1970, 1, 1), // Hypothetical date, true birth date unknown
    AmountOfWork = 3, // Death Note, Bakuman, Platinum End
    WorkingAt = "Shueisha",
    Twitter = "@TsugumiOhba", // Hypothetical handle
    NameInNative = "大場 つぐみ", // Given the pseudonym, this is an assumed translation
    Contact = "ohba@shueisha.co.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Tsugumi_Ohba" },
},
                ArtistModels = new List<ArtistModel>
{
 new ArtistModel { FirstName = "Takeshi", LastName = "Obata", Biography = "Japanese manga artist known for his work on Death Note, Hikaru no Go, and Bakuman", Gender = "Male", BirthPlace = "Niigata, Japan", ArtistBorn = new DateTime(1969, 2, 11) },
},
                VoiceActors = new List<VoiceActorModel>
{
  new VoiceActorModel { FirstName = "Mamoru", LastName = "Miyano", Biography = "Japanese actor, voice actor, and singer", Gender = "Male", BirthPlace = "Saitama, Japan", VoiceBorn = new DateTime(1983, 6, 8), Twitter = "@miyanomamoru_PR" },
    new VoiceActorModel { FirstName = "Miyuki", LastName = "Sawashiro", Biography = "Japanese voice actress and singer", Gender = "Female", BirthPlace = "Tokyo, Japan", VoiceBorn = new DateTime(1985, 6, 2), Twitter = "@sawashiro_staff" },
    new VoiceActorModel { FirstName = "Kappei", LastName = "Yamaguchi", Biography = "Japanese voice actor and actor", Gender = "Male", BirthPlace = "Fukuoka, Japan", VoiceBorn = new DateTime(1965, 5, 23), Twitter = "@ENma_Dororon" },
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
new Character {     CharacterName = "Light Yagami",
    specie = "Human",
    Gender = "Male",
    Born = "February 28, 1986",
    PlaceOffResidence = "Japan",
    World = "Death Note",
    Nationality = "Japanese",
    Education = "Senior High School Student",
    Occupation = "College Student, Criminal Mastermind",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Yagami.jpg")),
    Personality = "Calculative, Charismatic, God Complex",
    FamousQuote = "I am Justice!",
    EyeColor = "Brown",
    HairColor = "Brown",
    Abilities = "Uses Death Note",
    Hobbies = "Strategizing",
    Likes = "Order, Justice",
    Dislikes = "Criminals",
    PersonalityTraits = "Intelligent, Cunning, Manipulative" },
new Character { CharacterName = "L Lawliet",
    specie = "Human",
    Gender = "Male",
    Born = "October 31, 1979",
    PlaceOffResidence = "Japan",
    World = "Death Note",
    Nationality = "English",
    Education = "Whammy's House",
    Occupation = "Detective",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Lawliet.jpeg")),
    Personality = "Analytical, Mysterious, Unconventional",
    FamousQuote = "I am L. Justice is a word that has lost meaning to me.",
    EyeColor = "Black",
    HairColor = "Black",
    Abilities = "Exceptional deductive abilities",
    Hobbies = "Eating sweets, solving puzzles",
    Likes = "Sweets, Challenges",
    Dislikes = "Kira, Losing",
    PersonalityTraits = "Genius, Eccentric, Introverted" },
new Character {
    CharacterName = "Misa Amane",
    specie = "Human",
    Gender = "Female",
    Born = "December 25, 1984",
    PlaceOffResidence = "Japan",
    World = "Death Note",
    Nationality = "Japanese",
    Education = "High School Graduate",
    Occupation = "Actress, Model, Kira's supporter",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Misa.jpg")),
    Personality = "Energetic, Obsessive, Naive",
    FamousQuote = "I'm Misa-Misa, and I love Light Yagami!",
    EyeColor = "Blue",
    HairColor = "Blonde",
    Abilities = "Possesses a Death Note",
    Hobbies = "Stalking Light, Being a fan of Kira",
    Likes = "Light Yagami, Shinigami, Attention",
    Dislikes = "Losing Light's attention, Being ignored",
    PersonalityTraits = "Impulsive, Emotional, Devoted" },
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
                ISBN13 = "234-5678901234",
                futureEvents = "None",
                StatusInCountryOfOrigin = "Completed",
                CompletelyTranslated = "Yes",
                OriginalPublisher = "Square Enix",

                Type = "Manga",
                OfficalLanguage = "Japanese",
                PhotoPath = Path.Combine(ProcessUploadedFile("FullMetal.jpg")),
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
                new AuthorModel { FirstName = "Hiromu", LastName = "Arakawa", Biography = "Known for Fullmetal Alchemist and Silver Spoon.", BirthPlace = "Tokachi, Hokkaido, Japan", AuthorBorn = new DateTime(1973, 5, 8) },
                },
                ArtistModels = new List<ArtistModel>
                {
                new ArtistModel { FirstName = "Hiromu", LastName = "Arakawa", Biography = "Famous for her work on Fullmetal Alchemist.", BirthPlace = "Tokachi, Hokkaido, Japan", ArtistBorn = new DateTime(1973, 5, 8) },
                },
                VoiceActors = new List<VoiceActorModel>
                {
                 new VoiceActorModel { FirstName = "Romi", LastName = "Park", Biography = "Best known for voicing Edward Elric in Fullmetal Alchemist.", BirthPlace = "Edogawa, Tokyo, Japan", VoiceBorn = new DateTime(1972, 1, 22) },
    new VoiceActorModel { FirstName = "Rie", LastName = "Kugimiya", Biography = "Famous for voicing Alphonse Elric in Fullmetal Alchemist and Happy in Fairy Tail.", BirthPlace = "Kumamoto, Japan", VoiceBorn = new DateTime(1979, 5, 30) },
    new VoiceActorModel { FirstName = "Vic", LastName = "Mignogna", Biography = "Known for his English voice work as Edward Elric in Fullmetal Alchemist.", BirthPlace = "Greensburg, Pennsylvania, USA", VoiceBorn = new DateTime(1962, 8, 27) },
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
    new Character {   CharacterName = "Edward Elric",
        specie = "Human",
        Gender = "Male",
        World = "Fullmetal Alchemist",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Edward.jpg")),
        Personality = "Determined, Short-tempered, Compassionate",
        FamousQuote = "A lesson without pain is meaningless. For you cannot gain something without sacrificing something else in return.",
        EyeColor = "Golden",
        HairColor = "Blonde",
        Abilities = "Alchemy, Automail Prosthetics",
        Hobbies = "Alchemy research, Exploring",
        Likes = "Alchemy, His brother Alphonse",
        Dislikes = "Being called short, Losing loved ones",
        PersonalityTraits = "Stubborn, Brave, Protective" },
    new Character { CharacterName = "Alphonse Elric",
        specie = "Human",
        Gender = "Male",
        World = "Fullmetal Alchemist",
        Abilities = "Alchemy, Soul-bound to armor",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Alphonse.jpeg")),
        Personality = "Gentle, Determined, Philosophical",
        FamousQuote = "It's not the face that makes someone a monster. It's the choices they make with their lives.",
        EyeColor = "Gray",
        HairColor = "No hair (due to being a soul-bound armor)",
        Hobbies = "Alchemy studies, Reflecting on existence",
        Likes = "Reading, Seeking knowledge",
        Dislikes = "Being unable to feel sensations",
        PersonalityTraits = "Kind, Thoughtful, Resilient"},
    new Character {   CharacterName = "Winry Rockbell",
        specie = "Human",
        Gender = "Female",
        World = "Fullmetal Alchemist",
        Occupation = "Mechanic",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Winry.jpg")),
        Personality = "Energetic, Caring, Skilled",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Hobbies = "Mechanical work, Improving automail designs",
        Likes = "Automail engineering, Helping her friends",
        Dislikes = "Seeing damaged automail, Losing her loved ones",
        PersonalityTraits = "Practical, Empathetic, Strong-willed" },
    new Character { CharacterName = "Roy Mustang",
        specie = "Human",
        Gender = "Male",
        World = "Fullmetal Alchemist",
        Occupation = "State Alchemist, Military Officer",
        Abilities = "Flame Alchemy",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Roy.jpg")),
        Personality = "Confident, Charismatic, Strategic",
        FamousQuote = "I don't want to rule the world. I just want to see it burn.",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Chess, Reading",
        Likes = "Igniting his gloves, Seeking justice",
        Dislikes = "Injustice, Corruption",
        PersonalityTraits = "Ambitious, Resourceful, Leader" },
    new Character { CharacterName = "Riza Hawkeye",
        specie = "Human",
        Gender = "Female",
        World = "Fullmetal Alchemist",
        Occupation = "Military Officer",
        Abilities = "Marksmanship",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Riza.jpg")),
        Personality = "Calm, Loyal, Focused",
        EyeColor = "Black",
        HairColor = "Blonde",
        Hobbies = "Shooting, Cleaning her firearm",
        Likes = "Roy Mustang, Discipline",
        Dislikes = "Inefficiency, Betrayal",
        PersonalityTraits = "Steadfast, Responsible, Reliable"},
    new Character {CharacterName = "Maes Hughes",
        specie = "Human",
        Gender = "Male",
        World = "Fullmetal Alchemist",
        Occupation = "Military Officer",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Hughes.jpg")),
        Personality = "Friendly, Family-oriented, Enthusiastic",
        FamousQuote = "A soldier's most prized possession is his heart.",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Hobbies = "Showing pictures of his daughter, Investigating",
        Likes = "His family, Friends",
        Dislikes = "Government secrets, Losing his loved ones",
        PersonalityTraits = "Sociable, Protective, Caring" }
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
                ISBN13 = "123-4567890123",
                futureEvents = "None",
                StatusInCountryOfOrigin = "Ongoing",
                CompletelyTranslated = "No",
                OriginalPublisher = "Shueisha",

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
                BuyPages = new List<BuyPage>
                    {
                        new BuyPage { BuyWebsite ="https://www.amazon.com/One-Piece-Vol-Romance-Dawn/dp/1569319014/ref=sr_1_3?keywords=Manga+One+Piece&qid=1675256565&sr=8-3", _Languages = new List<Languages>{ EnglishLanguage  }},
                        new BuyPage {  BuyWebsite = "https://www.amazon.com/One-Piece-Vol-1-Japanese/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
                    },
                Authormodels = new List<AuthorModel>
                    {
                        new AuthorModel {  FirstName = "Eiichiro",
    LastName = "Oda",
    Biography = "Japanese manga artist, best known as the creator of the One Piece series - the best-selling manga in history. Oda's distinct art style and compelling story-telling have garnered a global fanbase.",
    BirthPlace = "Kumamoto, Japan",
    Gender = "Male",
    AuthorBorn = new DateTime(1975, 1, 1),
    AmountOfWork = 1, // One Piece
    WorkingAt = "Shueisha",
    Twitter = "@Eiichiro_Staff", // Official staff account
    NameInNative = "尾田 栄一郎",
    Contact = "oda@onepiece.com", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Eiichiro_Oda" },
                    },

                ArtistModels = new List<ArtistModel>
                    {
                        new ArtistModel { FirstName = "Eiichiro",
    LastName = "Oda",
    Biography = "Creator and artist of One Piece, Eiichiro Oda's unparalleled imagination has crafted one of the longest-running and beloved manga and anime series worldwide.",
    BirthPlace = "Kumamoto, Japan",
    Gender = "Male",
    ArtistBorn = new DateTime(1975, 1, 1),
    AmountOfWork = 1, // One Piece
    WorkingAt = "Shueisha",
    Twitter = "@Eiichiro_Staff", // Official staff account
    NameInNative = "尾田 栄一郎",
    Contact = "oda@onepiece.com", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Eiichiro_Oda" },
                    },
                VoiceActors = new List<VoiceActorModel>
                    {
                        new VoiceActorModel {  FirstName = "Mayumi",
    LastName = "Tanaka",
    Gender = "Female",
    Biography = "Japanese voice actress renowned for her role as Monkey D. Luffy in One Piece. Her energetic voice has brought to life one of the most iconic characters in anime.",
    BirthPlace = "Tokyo, Japan",
    VoiceBorn = new DateTime(1955, 1, 15),
    Twitter = "@MayumiTanaka", // Hypothetical handle
    NameInNative = "田中 真弓",
    Contact = "tanaka@voicepro.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Mayumi_Tanaka",
    AmountOfWork = 100, // Hypothetical number of works
    WorkingAt = "Freelance" },
                        new VoiceActorModel {  FirstName = "Akemi",
    LastName = "Okamura",
    Gender = "Female",
    Biography = "Voice actress known for her portrayal of Nami in One Piece. Okamura's voice perfectly captures Nami's complex character, from her cleverness to her vulnerability.",
    BirthPlace = "Tokyo, Japan",
    VoiceBorn = new DateTime(1969, 3, 12),
    Twitter = "@AkemiOkamura", // Hypothetical handle
    NameInNative = "岡村 明美",
    Contact = "okamura@voiceagency.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Akemi_Okamura",
    AmountOfWork = 80, // Hypothetical number of works
    WorkingAt = "Freelance" },
                        new VoiceActorModel { FirstName = "Kazuya",
    LastName = "Nakai",
    Gender = "Male",
    Biography = "Esteemed for his role as Roronoa Zoro in One Piece, Kazuya Nakai's deep and powerful voice lends a significant depth to the swordsman's character.",
    BirthPlace = "Kobe, Hyogo, Japan",
    VoiceBorn = new DateTime(1967, 11, 25),
    Twitter = "@KazuyaNakai", // Hypothetical handle
    NameInNative = "中井 和哉",
    Contact = "nakai@voices.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Kazuya_Nakai",
    AmountOfWork = 120, // Hypothetical number of works
    WorkingAt = "Freelance" },
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
                        new Character { CharacterName = "Monkey D. Luffy",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Abilities = "Rubber-like physiology, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Luffy.jpg")),
        Personality = "Adventurous, Optimistic, Reckless",
        FamousQuote = "I'm gonna be the Pirate King!",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Eating, Exploring",
        Likes = "Freedom, Adventure, Meat",
        Dislikes = "Being restrained, Friends in danger",
        PersonalityTraits = "Courageous, Impulsive, Loyal"},
                        new Character { CharacterName = "Roronoa Zoro",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Abilities = "Swordsmanship, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Zoro.jpeg")),
        Personality = "Serious, Determined, Loyal",
        FamousQuote = "I don't want to become the world's greatest swordsman to protect you or anyone else. I'm doing it because I want to be the best, period.",
        EyeColor = "Black",
        HairColor = "Green",
        Hobbies = "Sword training, Sleeping",
        Likes = "Swords, Strong opponents",
        Dislikes = "Getting lost, Dishonorable actions",
        PersonalityTraits = "Focused, Strong-willed, Honorable" },
                        new Character {  CharacterName = "Nami",
        specie = "Human",
        Gender = "Female",
        World = "One Piece",
        Occupation = "Navigator",
        Abilities = "Cartography, Weather manipulation (with the Clima-Tact)",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Nami.jpeg")),
        Personality = "Clever, Greedy, Compassionate",
        FamousQuote = "I want money!",
        EyeColor = "Orange",
        HairColor = "Orange",
        Hobbies = "Navigation, Drawing maps",
        Likes = "Tangerines, Money",
        Dislikes = "Thieving pirates, Unfair deals",
        PersonalityTraits = "Resourceful, Opportunistic, Caring"},
                        new Character { CharacterName = "Usopp",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Sniper",
        Abilities = "Sharpshooting, Creativity",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Usopp.jpeg")),
        Personality = "Cowardly, Imaginative, Brave",
        FamousQuote = "I'm not gonna die! Because I'm the brave warrior of the sea!",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Telling tall tales, Inventing",
        Likes = "Bravery, Adventure",
        Dislikes = "Confrontation, Dishonesty",
        PersonalityTraits = "Inventive, Loyal, Overactive imagination" },
                        new Character { CharacterName = "Sanji",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Cook",
        Abilities = "Diable Jambe, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Sanji.jpg")),
        Personality = "Chivalrous, Flirtatious, Determined",
        FamousQuote = "Love-cooked meals suit me best.",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Cooking, Flirting with women",
        Likes = "Women, Cooking, Suits",
        Dislikes = "Injustice, Hurting women",
        PersonalityTraits = "Passionate, Gentlemanly, Competitive" },
                        new Character {  CharacterName = "Tony Tony Chopper",
        specie = "Reindeer",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Doctor",
        Abilities = "Human-Human Fruit transformations, Medical skills",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Chopper.jpg")),
        Personality = "Innocent, Curious, Caring",
        FamousQuote = "I want to become a doctor who can cure any disease!",
        EyeColor = "Black",
        HairColor = "Blue",
        Hobbies = "Reading, Learning about medicine",
        Likes = "Cotton candy, Friends",
        Dislikes = "Being treated like a pet, Bullies",
        PersonalityTraits = "Naive, Optimistic, Kind-hearted" },
                        new Character {CharacterName = "Nico Robin",
        specie = "Human",
        Gender = "Female",
        World = "One Piece",
        Occupation = "Archaeologist",
        Abilities = "Hana-Hana Fruit powers, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Robin.jpg")),
        Personality = "Reserved, Intellectual, Mysterious",
        FamousQuote = "I want to live!",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Reading, Historical research",
        Likes = "Poneglyphs, Knowledge",
        Dislikes = "Isolation, Misunderstanding",
        PersonalityTraits = "Calm, Analytical, Determined" },
                        new Character {  CharacterName = "Franky",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Shipwright",
        Abilities = "Cyborg enhancements, Ship engineering",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Franky.jpeg")),
        Personality = "Eccentric, Loyal, Boisterous",
        FamousQuote = "I'm super, and I'm cyborg!",
        EyeColor = "Blue",
        HairColor = "Bald with a blue crew cut",
        Hobbies = "Building gadgets, Dancing",
        Likes = "Cola, Mechanics",
        Dislikes = "Insults about his appearance, Pirates who harm ships",
        PersonalityTraits = "Energetic, Inventive, Fearless" },new Character
    {
        CharacterName = "Portgas D. Ace",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Pirate",
        Abilities = "Mera-Mera Fruit powers",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Ace.jpg")),
        Personality = "Lively, Hot-headed, Protective",
        FamousQuote = "I don't want to live a thousand years. If I just live through today, that'll be enough.",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Eating, Training",
        Likes = "Freedom, Adventure",
        Dislikes = "Being called weak, Losing loved ones",
        PersonalityTraits = "Passionate, Reckless, Caring"
    },
    new Character
    {
        CharacterName = "Edward Newgate (Whitebeard)",
        specie = "Human",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Pirate Captain",
        Abilities = "Tremor-Tremor Fruit powers, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Whitebeard.jpeg")),
        Personality = "Respected, Caring, Fearless",
        FamousQuote = "When do you think people die? When they are shot by a bullet? No! When they eat a soup made from a poisonous mushroom? No! People die...when they are forgotten!",
        EyeColor = "Black",
        HairColor = "White",
        Hobbies = "Protecting his crew, Napping",
        Likes = "Family, Protecting his loved ones",
        Dislikes = "Injustice, Threats to his family",
        PersonalityTraits = "Wise, Fatherly, Indomitable"
    },
    new Character
    {
        CharacterName = "Boa Hancock",
        specie = "Human",
        Gender = "Female",
        World = "One Piece",
        Occupation = "Pirate Empress, Snake Princess",
        Abilities = "Love-Love Fruit powers, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Hancock.jpeg")),
        Personality = "Proud, Confident, Doting",
        FamousQuote = "A strong heart will always overpower a strong body.",
        EyeColor = "Purple",
        HairColor = "Black",
        Hobbies = "Admiring Luffy, Bathing",
        Likes = "Luffy, Beauty, Freedom",
        Dislikes = "Disrespect, Slavery",
        PersonalityTraits = "Charismatic, Strong-willed, Romantic"
    },
                        new Character { CharacterName = "Brook",
        specie = "Living Skeleton",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Musician",
        Abilities = "Revive-Revive Fruit powers, Swordsmanship",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Brook.jpg")),
        Personality = "Cheerful, Musical, Lively",
        FamousQuote = "Yohohoho!",
        EyeColor = "Empty eye sockets (white light)",
        HairColor = "Black",
        Hobbies = "Playing music, Jokes",
        Likes = "Music, Tea parties",
        Dislikes = "Pudding being wasted, Cold weather",
        PersonalityTraits = "Jovial, Artistic, Optimistic" },
                        new Character { CharacterName = "Jimbei",
        specie = "Fish-Man",
        Gender = "Male",
        World = "One Piece",
        Occupation = "Helmsman",
        Abilities = "Fish-Man Karate, Fish-Man Jujutsu, Haki",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Jimbei.jpeg")),
        Personality = "Calm, Wise, Honorable",
        FamousQuote = "Fish-men and humans are both living in the same world, a world that's equally divided between two kinds of people. But if we join forces, we can overcome the hate, and we can protect the ship that we all live on!",
        EyeColor = "Black",
        HairColor = "Blue",
        Hobbies = "Meditation, Fishing",
        Likes = "Peace, Camaraderie",
        Dislikes = "Prejudice, Aggression",
        PersonalityTraits = "Respectful, Meditative, Just" },
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
                ISBN13 = "012-3456789012",
                futureEvents = "None",
                StatusInCountryOfOrigin = "Completed",
                CompletelyTranslated = "Yes",
                OriginalPublisher = "Kodansha",

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
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite ="https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419", _Languages = new List<Languages>{ EnglishLanguage }},
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
    },
                Authormodels = new List<AuthorModel>
    {
        new AuthorModel {  FirstName = "Naoko",
        LastName = "Takeuchi",
        Biography = "Japanese manga artist best known for creating Sailor Moon, a seminal series that redefined the magical girl genre. Her work has had a significant impact on pop culture worldwide.",
        BirthPlace = "Kofu, Yamanashi, Japan",
        Gender = "Female",
        AuthorBorn = new DateTime(1967, 3, 15),
        AmountOfWork = 1, // Mainly known for Sailor Moon
        WorkingAt = "Kodansha",
        Twitter = null, // Naoko Takeuchi is not known to have a public Twitter account
        NameInNative = "武内 直子",
        Contact = "contact@kodansha.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Naoko_Takeuchi" },
    },
                ArtistModels = new List<ArtistModel>
    {
        new ArtistModel {  FirstName = "Naoko",
        LastName = "Takeuchi",
        Biography = "Noted for her detailed and expressive artwork in Sailor Moon, Naoko Takeuchi has inspired countless artists and fans with her dedication and artistic skill.",
        BirthPlace = "Kofu, Yamanashi, Japan",
        Gender = "Female",
        ArtistBorn = new DateTime(1967, 3, 15),
        AmountOfWork = 1, // Mainly known for Sailor Moon
        WorkingAt = "Kodansha",
        Twitter = null, // Assuming no public Twitter account
        NameInNative = "武内 直子",
        Contact = "contact@kodansha.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Naoko_Takeuchi" },
    },

                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
                VoiceActors = new List<VoiceActorModel>
    {
        new VoiceActorModel {  FirstName = "Kotono",
        LastName = "Mitsuishi",
        Gender = "Female",
        Biography = "A leading Japanese voice actress renowned for her role as Usagi Tsukino in Sailor Moon. Her voice work has earned her critical acclaim and a devoted fanbase.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1967, 12, 8),
        Twitter = "@kotochawanmoon", // Hypothetical handle
        NameInNative = "三石 琴乃",
        Contact = "mitsuishi@voiceacting.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Kotono_Mitsuishi" },
        new VoiceActorModel { FirstName = "Aya",
        LastName = "Hisakawa",
        Gender = "Female",
        Biography = "Esteemed for her voice role as Ami Mizuno in Sailor Moon, Aya Hisakawa's performances have captivated audiences, showcasing her versatility and emotional range.",
        BirthPlace = "Osaka, Japan",
        VoiceBorn = new DateTime(1968, 11, 12),
        Twitter = "@aya_hisakawa", // Hypothetical handle
        NameInNative = "久川 綾",
        Contact = "hisakawa@voiceacting.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Aya_Hisakawa" },
        new VoiceActorModel {  FirstName = "Emi",
        LastName = "Shinohara",
        Gender = "Female",
        Biography = "Known for her role as Makoto Kino in Sailor Moon, Emi Shinohara's voice acting brings depth and strength to her characters, making her a beloved figure in the anime community.",
        BirthPlace = "Nagano, Japan",
        VoiceBorn = new DateTime(1963, 8, 8),
        Twitter = "@emi_shinohara", // Hypothetical handle
        NameInNative = "篠原 恵美",
        Contact = "shinohara@voiceacting.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Emi_Shinohara" },
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
                new Character
    {
        CharacterName = "Sailor Jupiter",
        specie = "Human",
        Gender = "Female",
        World = "Sailor Moon",
        Occupation = "Guardian of Thunder and Courage",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SailorJupiter.jpg")),
        Personality = "Strong-willed, Kind, Loyal",
        FamousQuote = "Supreme Thunder Dragon!",
        EyeColor = "Green",
        HairColor = "Brown",
        Abilities = "Control over thunder and lightning",
        Hobbies = "Cooking, Gardening",
        Likes = "Nature, Physical strength",
        Dislikes = "Cooking failures, Injustice",
        PersonalityTraits = "Resilient, Reliable, Compassionate"
    },
    new Character
    {
        CharacterName = "Sailor Moon",
        specie = "Human",
        Gender = "Female",
        World = "Sailor Moon",
        Occupation = "Guardian of Love and Justice",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SailorMoon.jpg")),
        Personality = "Kind-hearted, Optimistic, Clumsy",
        FamousQuote = "In the name of the Moon, I'll punish you!",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Abilities = "Moon Prism Power, Healing abilities",
        Hobbies = "Eating, Spending time with friends",
        Likes = "Helping others, Romance",
        Dislikes = "Evil, Loneliness",
        PersonalityTraits = "Empathetic, Courageous, Unyielding"
    },
    new Character
    {
        CharacterName = "Sailor Mercury",
        specie = "Human",
        Gender = "Female",
        World = "Sailor Moon",
        Occupation = "Guardian of Wisdom and Water",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SailorMercury.jpg")),
        Personality = "Smart, Shy, Analytical",
        FamousQuote = "Mercury Power, Make Up!",
        EyeColor = "Blue",
        HairColor = "Blue",
        Abilities = "Control over water and ice, Intellect",
        Hobbies = "Studying, Reading",
        Likes = "Learning, Solving problems",
        Dislikes = "Ignorance, Misuse of technology",
        PersonalityTraits = "Intellectual, Caring, Reserved"
    },
    new Character
    {
        CharacterName = "Sailor Mars",
        specie = "Human",
        Gender = "Female",
        World = "Sailor Moon",
        Occupation = "Guardian of Fire and Passion",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SailorMars.jpg")),
        Personality = "Fiery, Intuitive, Spiritual",
        FamousQuote = "Mars Power, Make Up!",
        EyeColor = "Purple",
        HairColor = "Black",
        Abilities = "Control over fire, Spiritual sensitivity",
        Hobbies = "Shrine visits, Meditation",
        Likes = "Spiritual practices, Fortunetelling",
        Dislikes = "Disrespect for tradition, Evil spirits",
        PersonalityTraits = "Passionate, Intense, Mystical"
    },
    new Character
    {
        CharacterName = "Sailor Venus",
        specie = "Human",
        Gender = "Female",
        World = "Sailor Moon",
        Occupation = "Guardian of Love and Beauty",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SailorVenus.jpg")),
        Personality = "Playful, Charismatic, Romantic",
        FamousQuote = "Venus Power, Make Up!",
        EyeColor = "Blue",
        HairColor = "Blonde",
        Abilities = "Control over love-based energy, Combat skills",
        Hobbies = "Idol activities, Dancing",
        Likes = "Popularity, Stardom",
        Dislikes = "Misunderstandings, Betrayal",
        PersonalityTraits = "Charming, Confident, Optimistic"
    },
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
                ISBN13 = "345-6789012345",
                futureEvents = "None",
                StatusInCountryOfOrigin = "Ongoing",
                CompletelyTranslated = "No",
                OriginalPublisher = "Shogakukan",

                Type = "Manga",
                OfficalLanguage = "Japanese",
                PhotoPath = Path.Combine(ProcessUploadedFile("Conan.jpg")),
                Description = "A high school detective who is transformed into a child after being poisoned, solves crimes under a different identity while searching for a cure.",
                ReleaseYear = new DateTime(1994, 1, 2),
                EndingYear = new DateTime(),
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage, FrenchLanguage },
                AssociatedNames = new List<AssociatedNames>
{
new AssociatedNames { nameString = "Detective Conan Comics" },
new AssociatedNames { nameString = "Detective Conan Manga" },
},
                BuyPages = new List<BuyPage>
    {
        new BuyPage { BuyWebsite ="https://www.amazon.com/Sailor-Moon-Kodansha-Comics-Edition/dp/1632366419", _Languages = new List<Languages>{ EnglishLanguage }},
        new BuyPage { BuyWebsite = "https://www.amazon.co.jp/SAILOR-MOON-SAILOR-MOON/dp/4088725093", _Languages = new List<Languages>{ JapaneseLanguage } }
    },
                OfficalWebsites = new List<OfficalWebsite>
            {
            new OfficalWebsite { OfficalWebsiteString = "www.detectiveConan.com", OfficalWebsiteName = "DetectiveConan.Com"},
            new OfficalWebsite { OfficalWebsiteString = "detectiveConan.jp", OfficalWebsiteName = "DetectiveConan.jp" },
            },
                Authormodels = new List<AuthorModel>
            {
            new AuthorModel {  FirstName = "Gosho",
        LastName = "Aoyama",
        Biography = "Japanese manga artist best known for creating Detective Conan (also known as Case Closed). His intricate plots and mystery genres have captivated readers globally.",
        BirthPlace = "Hokuei, Tottori, Japan",
        Gender = "Male",
        AuthorBorn = new DateTime(1963, 6, 21),
        AmountOfWork = 1, // Primarily known for Detective Conan
        WorkingAt = "Shogakukan",
        Twitter = null, // Gosho Aoyama is not known to have a public Twitter account
        NameInNative = "青山 剛昌",
        Contact = "aoyama@shogakukan.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Gosho_Aoyama" },
            },
                ArtistModels = new List<ArtistModel>
            {
            new ArtistModel {  FirstName = "Gosho",
        LastName = "Aoyama",
        Biography = "Creator of Detective Conan, Aoyama's detailed and expressive art style has made the series one of the longest-running and most beloved in manga history.",
        BirthPlace = "Hokuei, Tottori, Japan",
        Gender = "Male",
        ArtistBorn = new DateTime(1963, 6, 21),
        AmountOfWork = 1, // Detective Conan
        WorkingAt = "Shogakukan",
        Twitter = null, // Assuming no public Twitter account
        NameInNative = "青山 剛昌",
        Contact = "aoyama@shogakukan.co.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Gosho_Aoyama"},
            },

                VoiceActors = new List<VoiceActorModel>
            {
            new VoiceActorModel {  FirstName = "Minami",
        LastName = "Takayama",
        Gender = "Female",
        Biography = "Renowned Japanese voice actress and singer, best known for her role as Conan Edogawa in Detective Conan. Her ability to convey Conan's intelligence and bravery has been pivotal to the series' success.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1964, 5, 5),
        Twitter = "@minami_takayama", // Hypothetical handle
        NameInNative = "高山 みなみ",
        Contact = "takayama@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Minami_Takayama" },
            new VoiceActorModel {   FirstName = "Wakana",
        LastName = "Yamazaki",
        Gender = "Female",
        Biography = "A Japanese voice actress known for her role as Ran Mouri in Detective Conan. Her performance adds depth to Ran's character, making her one of the series' most beloved figures.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1965, 3, 21),
        Twitter = "@wakana_yamazaki", // Hypothetical handle
        NameInNative = "山崎 和佳奈",
        Contact = "yamazaki@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Wakana_Yamazaki" },
            new VoiceActorModel {  FirstName = "Rikiya",
        LastName = "Koyama",
        Gender = "Male",
        Biography = "Esteemed Japanese voice actor, well-known for voicing Kogoro Mouri in Detective Conan. His portrayal of Kogoro's complex personality has contributed significantly to the series' intrigue and humor.",
        BirthPlace = "Kyoto, Japan",
        VoiceBorn = new DateTime(1963, 12, 18),
        Twitter = "@rikiya_koyama", // Hypothetical handle
        NameInNative = "小山 力也",
        Contact = "koyama@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Rikiya_Koyama" },
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
               new Character
    {
        CharacterName = "Ai Haibara",
        specie = "Human",
        Gender = "Female",
        World = "Detective Conan",
        Occupation = "Former Black Organization member, Scientist",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("AiHaibara.jpg")),
        Personality = "Reserved, Intelligent, Cautious",
        FamousQuote = "It's not a bad thing to show a little affection.",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Chemistry expertise, Knowledge of the Black Organization",
        Hobbies = "Research, Solving mysteries",
        Likes = "Science, Friends",
        Dislikes = "Black Organization, Betrayal",
        PersonalityTraits = "Analytical, Prudent, Determined"
    },
    new Character
    {
        CharacterName = "Hiroshi Agasa",
        specie = "Human",
        Gender = "Male",
        World = "Detective Conan",
        Occupation = "Inventor, Professor",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("HiroshiAgasa.jpeg")),
        Personality = "Kind, Eccentric, Supportive",
        FamousQuote = "A detective is someone who uncovers the truth. It's not someone who always knows who the criminal is.",
        EyeColor = "Brown",
        HairColor = "Gray",
        Hobbies = "Inventing gadgets, Supporting Conan",
        Likes = "Science, Curiosity",
        Dislikes = "Evil, Unfairness",
        PersonalityTraits = "Inventive, Helpful, Wise"
    },
    new Character
    {
        CharacterName = "Kogoro Mouri",
        specie = "Human",
        Gender = "Male",
        World = "Detective Conan",
        Occupation = "Private Detective, Former Police Officer",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("KogoroMouri.jpg")),
        Personality = "Boisterous, Prideful, Clueless",
        FamousQuote = "The brilliant detective, Kogoro Mouri, is here!",
        EyeColor = "Black",
        HairColor = "Black",
        Hobbies = "Solving cases (with Conan's help), Karaoke",
        Likes = "Solving cases, Reputation",
        Dislikes = "Conan's deductions, Being called an amateur",
        PersonalityTraits = "Confident, Tenacious, Humorous"
    },
    new Character
    {
        CharacterName = "Shinichi Kudo (Conan Edogawa)",
        specie = "Human",
        Gender = "Male",
        World = "Detective Conan",
        Occupation = "High School Student, Amateur Detective",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("ShinichiKudo.jpeg")),
        Personality = "Sharp, Observant, Determined",
        FamousQuote = "There's only one truth!",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Exceptional deduction skills, Martial arts",
        Hobbies = "Solving mysteries, Playing soccer",
        Likes = "Solving puzzles, Justice",
        Dislikes = "Criminals, Injustice",
        PersonalityTraits = "Analytical, Brave, Just"
    }, new Character
    {
        CharacterName = "Conan Edogawa (Shinichi Kudo)",
        specie = "Human",
        Gender = "Male",
        World = "Detective Conan",
        Occupation = "Amateur Detective",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("ConanEdogawa.jpeg")),
        Personality = "Sharp, Observant, Determined",
        FamousQuote = "There's only one truth!",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Exceptional deduction skills, Martial arts",
        Hobbies = "Solving mysteries, Outsmarting culprits",
        Likes = "Solving puzzles, Seeking justice",
        Dislikes = "Criminals, Injustice",
        PersonalityTraits = "Analytical, Brave, Just"
    },
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
                ISBN13 = "234-5678901234",
                futureEvents = "None",
                StatusInCountryOfOrigin = "Completed",
                CompletelyTranslated = "Yes",
                OriginalPublisher = "Production I.G",

                Type = "Anime",
                OfficalLanguage = "Japanese",
                PhotoPath = Path.Combine(ProcessUploadedFile("AttackOnTitan.jpg")),
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
new AuthorModel {  FirstName = "Hajime",
        LastName = "Isayama",
        Biography = "Japanese manga artist best known for creating the Attack on Titan series, which has become one of the best-selling manga series of all time. Isayama's unique storytelling and art style have garnered critical acclaim worldwide.",
        BirthPlace = "Ōyama, Ōita, Japan",
        Gender = "Male",
        AuthorBorn = new DateTime(1986, 8, 29),
        AmountOfWork = 1, // Attack on Titan
        WorkingAt = "Kodansha",
        Twitter = null, // Hajime Isayama is not known to have a public Twitter account
        NameInNative = "諫山 創",
        Contact = "isayama@kodansha.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Hajime_Isayama" },
},
                ArtistModels = new List<ArtistModel>
{
new ArtistModel { FirstName = "Hajime",
        LastName = "Isayama",
        Biography = "Known for his groundbreaking work on Attack on Titan, Hajime Isayama has redefined manga with his innovative storytelling and detailed art, captivating a global audience.",
        BirthPlace = "Ōyama, Ōita, Japan",
        Gender = "Male",
        ArtistBorn = new DateTime(1986, 8, 29),
        AmountOfWork = 1, // Attack on Titan
        WorkingAt = "Kodansha",
        Twitter = null, // Assuming no public Twitter account
        NameInNative = "諫山 創",
        Contact = "isayama@kodansha.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Hajime_Isayama" },
},
                VoiceActors = new List<VoiceActorModel>
{
new VoiceActorModel {  FirstName = "Yuki",
        LastName = "Kaji",
        Gender = "Male",
        Biography = "Prominent Japanese voice actor known for his role as Eren Yeager in Attack on Titan. Kaji's intense and emotional portrayal has contributed greatly to the character's popularity.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1985, 9, 3),
        Twitter = "@YukiKaji", // Hypothetical handle
        NameInNative = "梶 裕貴",
        Contact = "kaji@voiceactor.jp", // Hypothetical contact
        WikiPedia =  "https://en.wikipedia.org/wiki/Yuki_Kaji" },
new VoiceActorModel {  FirstName = "Yui",
        LastName = "Ishikawa",
        Gender = "Female",
        Biography = "Japanese voice actress celebrated for her role as Mikasa Ackerman in Attack on Titan. Ishikawa's performance has been praised for its depth and strength, mirroring Mikasa's character.",
        BirthPlace = "Hyogo, Japan",
        VoiceBorn = new DateTime(1989, 5, 30),
        Twitter = "@YuiIshikawa", // Hypothetical handle
        NameInNative = "石川 由依",
        Contact = "ishikawa@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Yui_Ishikawa"},
new VoiceActorModel {  FirstName = "Marina",
        LastName = "Inoue",
        Gender = "Female",
        Biography = "Known for voicing Armin Arlert in Attack on Titan, Marina Inoue has brought to life one of the series' most intellectually driven characters with her versatile voice acting.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1985, 1, 20),
        Twitter = "@Mari_navi", // Hypothetical handle
        NameInNative = "井上 麻里奈",
        Contact = "inoue@voiceactor.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Marina_Inoue" },
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
 new Character
    {
        CharacterName = "Eren Yeager",
        specie = "Human",
        Gender = "Male",
        World = "Attack on Titan",
        Occupation = "Former Scout Regiment member, Titan Shifter",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("ErenYeager.jpg")),
        Personality = "Determined, Impulsive, Brave",
        FamousQuote = "If you win, you live. If you lose, you die. If you don't fight, you can't win!",
        EyeColor = "Green",
        HairColor = "Brown",
        Abilities = "Titan Shifting, Combat skills",
        Hobbies = "Training, Seeking freedom",
        Likes = "Freedom, Protecting loved ones",
        Dislikes = "Titans, Oppression",
        PersonalityTraits = "Headstrong, Idealistic, Resilient"
    },
    new Character
    {
        CharacterName = "Mikasa Ackerman",
        specie = "Human",
        Gender = "Female",
        World = "Attack on Titan",
        Occupation = "Former Scout Regiment member, Skilled Fighter",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("MikasaAckerman.jpg")),
        Personality = "Protective, Determined, Stoic",
        FamousQuote = "If you think reality is just living comfortably and following your own whims, can you seriously dare to call yourself a soldier?",
        EyeColor = "Black",
        HairColor = "Black",
        Abilities = "Exceptional combat skills, Ackerman lineage",
        Hobbies = "Training, Looking out for Eren",
        Likes = "Eren, Combat",
        Dislikes = "Threats to Eren, Losing loved ones",
        PersonalityTraits = "Loyal, Strong-willed, Skilled"
    },
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
                AssociatedNames = new List<AssociatedNames>
                {
                new AssociatedNames { nameString = "LOTR"  },
                new AssociatedNames { nameString = "In the Name of the King" } ,
                },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Lord-Rings-Trilogy-Boxed-Set/dp/0544445783/ref=sr_1_1?keywords=lord+of+the+rings&qid=1645876628&sr=8-1", _Languages = new List<Languages> { EnglishLanguage } }
        },

                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.lordoftherings.net/", OfficalWebsiteName = "Lord of the Rings Official Site", Twitter = "https://twitter.com/lordoftherings", Facebook = "https://www.facebook.com/lordoftheringstrilogy/", Line = null }
        },
                Authormodels = new List<AuthorModel>
        {
            new AuthorModel {  FirstName = "J.R.R.",
        LastName = "Tolkien",
        Biography = "John Ronald Reuel Tolkien CBE FRSL (3 January 1892 – 2 September 1973) was an English writer, poet, philologist, and academic. He is best known as the author of the high fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.",
        BirthPlace = "Bloemfontein, South Africa",
        Gender = "Male",
        AuthorBorn = new DateTime(1892, 1, 3),
        AuthorDeath = new DateTime(1973, 9, 2),
        AmountOfWork = 3, // The Hobbit, The Lord of the Rings, The Silmarillion
        WorkingAt = "Oxford University",
        Twitter = null, // Tolkien's era predates social media
        NameInNative = "J.R.R. Tolkien",
        Contact = "info@tolkiensociety.org", // Hypothetical contact based on the Tolkien Society
        WikiPedia = "https://en.wikipedia.org/wiki/J._R._R._Tolkien"}
        },
                ArtistModels = new List<ArtistModel>
{
    new ArtistModel
    {
        FirstName = "Alan",
        LastName = "Lee",
        Biography = "Alan Lee is an English artist and writer. He is best known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings. He won an Academy Award for Best Art Direction for his work on the 2003 film The Lord of the Rings: The Return of the King.",
        BirthPlace = "Middlesex, England",
        Gender = "Male",
        ArtistBorn = new DateTime(1947, 8, 20),
        NameInNative = "Alan Lee",
        Contact = "alanlee@middleeartharts.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Alan_Lee_(illustrator)"
    },
    new ArtistModel
    {
        FirstName = "John",
        LastName = "Howe",
        Biography = "John Howe is a Canadian book illustrator, living in Neuchâtel, Switzerland. He has illustrated many fantasy books, including The Lord of the Rings and The Hobbit by J. R. R. Tolkien, and is one of the conceptual designers employed by Peter Jackson's The Lord of the Rings film trilogy.",
        BirthPlace = "Vancouver, Canada",
        Gender = "Male",
        ArtistBorn = new DateTime(1957, 8, 21),
        NameInNative = "John Howe",
        Contact = "johnhowe@fantasyarts.net", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/John_Howe_(illustrator)"
    },
    new ArtistModel
    {
        FirstName = "Ted",
        LastName = "Nasmith",
        Biography = "Ted Nasmith is a Canadian artist and illustrator. He is best known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings, and has produced several Tolkien-themed calendars and illustrated editions of The Silmarillion, The Lord of the Rings, and The Hobbit.",
        BirthPlace = "Godrich, Ontario, Canada",
        Gender = "Male",
        ArtistBorn = new DateTime(1956, 1, 1), // Hypothetical birth date
        NameInNative = "Ted Nasmith",
        Contact = "tednasmith@tolkienarts.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Ted_Nasmith"
    },
    new ArtistModel
    {
        FirstName = "Roger",
        LastName = "Garland",
        Biography = "Roger Garland is an artist and illustrator from the United Kingdom. He is known for his illustration work based on J. R. R. Tolkien's Middle-earth fantasy writings, and has created artwork for various editions of The Hobbit and The Lord of the Rings, as well as other fantasy works.",
        BirthPlace = "United Kingdom",
        Gender = "Male",
        ArtistBorn = new DateTime(1950, 4, 10), // Hypothetical birth date
        NameInNative = "Roger Garland",
        Contact = "rogergarland@fantasyillustrations.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Roger_Garland_(illustrator)" // This is a fabricated link for demonstration
    },
    new ArtistModel
    {
        FirstName = "Donato",
        LastName = "Giancola",
        Biography = "Donato Giancola is an American artist and illustrator, best known for his work in the science fiction and fantasy genres. He has created artwork for a number of book covers, including editions of The Lord of the Rings and The Hobbit, as well as works by other authors in the genre.",
        BirthPlace = "Colchester, Vermont, USA",
        Gender = "Male",
        ArtistBorn = new DateTime(1967, 5, 25),
        NameInNative = "Donato Giancola",
        Contact = "donatogiancola@sciart.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Donato_Giancola"
    },
    new ArtistModel
    {
        FirstName = "Michael",
        LastName = "Hague",
        Biography = "Michael Hague is an American illustrator and author, known for his work in the fantasy genre. He has created artwork for numerous children's books, including editions of The Hobbit and The Lord of the Rings, as well as works by other authors.",
        BirthPlace = "Los Angeles, California, USA",
        Gender = "Male",
        ArtistBorn = new DateTime(1948, 9, 8),
        NameInNative = "Michael Hague",
        Contact = "michaelhague@childrensbooks.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Michael_Hague"
    }
},

                VoiceActors = new List<VoiceActorModel>
{
    new VoiceActorModel
    {
        FirstName = "Rob",
        LastName = "Inglis",
        CharacterName = "Narrator",
        Gender = "Male",
        Biography = "Rob Inglis is known for his unabridged narrations of The Hobbit and The Lord of the Rings for Recorded Books.",
        VoiceBorn = new DateTime(1933, 10, 10), // Hypothetical birth date
        Twitter = "@RobInglisNarrator", // Hypothetical handle
        NameInNative = "Rob Inglis",
        Contact = "robinglis@audiobooks.com", // Hypothetical contact
    },
    new VoiceActorModel
    {
        FirstName = "Ian",
        LastName = "Holm",
        CharacterName = "Bilbo Baggins",
        Gender = "Male",
        Biography = "Ian Holm was a British actor known for his role as Bilbo Baggins in The Lord of the Rings film series.",
        VoiceBorn = new DateTime(1931, 9, 12), // Actual birth date
        VoiceDeath = new DateTime(2020, 6, 19), // Actual death date
        Twitter = null, // Ian Holm's era largely predates social media
        NameInNative = "Ian Holm",
        Contact = "legacy@ianholm.com", // Hypothetical contact
    },
    new VoiceActorModel
    {
        FirstName = "Andy",
        LastName = "Serkis",
        CharacterName = "Gollum",
        Gender = "Male",
        Biography = "Andy Serkis, an English actor and director, gained fame for his performance as Gollum in The Lord of the Rings and The Hobbit film trilogies, utilizing motion capture technology.",
        VoiceBorn = new DateTime(1964, 4, 20),
        Twitter = "@andyserkis",
        NameInNative = "Andy Serkis",
        Contact = "contact@andyserkis.com", // Hypothetical contact
    },
    new VoiceActorModel
    {
        FirstName = "Christopher",
        LastName = "Lee",
        CharacterName = "Saruman",
        Gender = "Male",
        Biography = "Sir Christopher Lee was an English actor, singer, and author known for his role as Saruman in The Lord of the Rings and The Hobbit film trilogies.",
        VoiceBorn = new DateTime(1922, 5, 27),
        VoiceDeath = new DateTime(2015, 6, 7),
        Twitter = null, // Christopher Lee's era largely predates social media
        NameInNative = "Christopher Lee",
        Contact = "legacy@christopherlee.com", // Hypothetical contact
    },
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
   new Character
{
    CharacterName = "Frodo Baggins",
    specie = "Hobbit",
    Gender = "Male",
    Born = "September 22, T.A. 2968",
    PlaceOffResidence = "Shire",
    World = "Middle-earth",
    Nationality = "Shire",
    Education = "Private tutor",
    Occupation = "Ring-bearer",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("FrodoBaggins.jpg")),
    Personality = "Courageous, Resilient, Determined",
    FamousQuote = "I wish it need not have happened in my time.",
    EyeColor = "Blue",
    HairColor = "Brown",
    Abilities = "Ring-bearer resilience, Resourcefulness",
    Hobbies = "Reading, Exploring",
    Likes = "Adventure, Friendship",
    Dislikes = "Sauron, Dark magic",
    PersonalityTraits = "Brave, Selfless, Humble"
},
new Character
{
    CharacterName = "Gandalf",
    specie = "Maiar",
    Gender = "Male",
    Born = "Unknown",
    PlaceOffResidence = "Middle-earth",
    World = "Middle-earth",
    Nationality = "Unknown",
    Education = "Unknown",
    Occupation = "Wizard",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Gandalf.jpeg")),
    Personality = "Wise, Mysterious, Compassionate",
    FamousQuote = "A wizard is never late, nor is he early, he arrives precisely when he means to.",
    EyeColor = "Blue",
    HairColor = "Grey",
    Abilities = "Powerful magic, Wisdom",
    Hobbies = "Advising, Exploring",
    Likes = "Helping others, Solving riddles",
    Dislikes = "Dark forces, Ignorance",
    PersonalityTraits = "Knowledgeable, Caring, Enigmatic"
},
new Character
{
    CharacterName = "Aragorn",
    specie = "Human",
    Gender = "Male",
    Born = "March 1, T.A. 2931",
    PlaceOffResidence = "Gondor",
    World = "Middle-earth",
    Nationality = "Dúnedain",
    Education = "Ranger training",
    Occupation = "King of Gondor",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Aragorn.jpg")),
    Personality = "Noble, Brave, Determined",
    FamousQuote = "I do not love the bright sword for its sharpness, nor the arrow for its swiftness, nor the warrior for his glory. I love only that which they defend.",
    EyeColor = "Grey",
    HairColor = "Dark",
    Abilities = "Swordsmanship, Leadership",
    Hobbies = "Exploring, Studying history",
    Likes = "Protecting, Justice",
    Dislikes = "Sauron, Injustice",
    PersonalityTraits = "Honorable, Compassionate, Regal"
},
new Character
{
    CharacterName = "Legolas",
    specie = "Elf",
    Gender = "Male",
    Born = "Third Age, 87",
    PlaceOffResidence = "Mirkwood",
    World = "Middle-earth",
    Nationality = "Woodland Realm",
    Education = "Unknown",
    Occupation = "Prince of Mirkwood",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Legolas.jpg")),
    Personality = "Graceful, Agile, Optimistic",
    FamousQuote = "Final count, forty-two.",
    EyeColor = "Blue",
    HairColor = "Blonde",
    Abilities = "Archery, Acrobatics",
    Hobbies = "Exploring, Archery contests",
    Likes = "Nature, Fellowship",
    Dislikes = "Orcs, Destruction",
    PersonalityTraits = "Loyal, Light-hearted, Skilled"
},
    },
            };
            context.mangaModels.Add(lotr);
            context.SaveChanges();
        }

        public void UltraLongBOokTester()
        {
            #region Tags

            var tag1 = context.TagModels.FirstOrDefault(t => t.TagName == "Emotionally Strong Male Lead");
            var tag2 = context.TagModels.FirstOrDefault(t => t.TagName == "Sword and Sorcery");
            var tag3 = context.TagModels.FirstOrDefault(t => t.TagName == "Princess");
            var tag4 = context.TagModels.FirstOrDefault(t => t.TagName == "Reincarnated in Another World");
            var tag5 = context.TagModels.FirstOrDefault(t => t.TagName == "Science Fiction");
            var tag6 = context.TagModels.FirstOrDefault(t => t.TagName == "Rich Male Lead");
            var tag7 = context.TagModels.FirstOrDefault(t => t.TagName == "Secret Relationship");
            var tag8 = context.TagModels.FirstOrDefault(t => t.TagName == "Time Travel");
            var tag9 = context.TagModels.FirstOrDefault(t => t.TagName == "Transformation");
            var tag10 = context.TagModels.FirstOrDefault(t => t.TagName == "Tutor");
            var tag11 = context.TagModels.FirstOrDefault(t => t.TagName == "Transfer Student");
            var tag12 = context.TagModels.FirstOrDefault(t => t.TagName == "Sudden Appearance");
            var tag13 = context.TagModels.FirstOrDefault(t => t.TagName == "Spirit");
            var tag14 = context.TagModels.FirstOrDefault(t => t.TagName == "Social Outcast");
            var tag15 = context.TagModels.FirstOrDefault(t => t.TagName == "Time Skip");
            var tag16 = context.TagModels.FirstOrDefault(t => t.TagName == "Teamwork");
            var tag17 = context.TagModels.FirstOrDefault(t => t.TagName == "War");
            var tag18 = context.TagModels.FirstOrDefault(t => t.TagName == "Unrealistic Fighting");
            var tag19 = context.TagModels.FirstOrDefault(t => t.TagName == "Survival");
            var tag20 = context.TagModels.FirstOrDefault(t => t.TagName == "SuperPowers");
            var tag21 = context.TagModels.FirstOrDefault(t => t.TagName == "Robot");
            var tag22 = context.TagModels.FirstOrDefault(t => t.TagName == "Revenge");
            var tag23 = context.TagModels.FirstOrDefault(t => t.TagName == "Pretend Lovers");
            var tag24 = context.TagModels.FirstOrDefault(t => t.TagName == "Politics Involving Royalty");
            var tag25 = context.TagModels.FirstOrDefault(t => t.TagName == "Play or Die Situation");
            var tag26 = context.TagModels.FirstOrDefault(t => t.TagName == "Older Sister");
            var tag27 = context.TagModels.FirstOrDefault(t => t.TagName == "Mythos");

            #endregion Tags

            #region genre

            var genre1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var genre2 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");
            var genre3 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adult");
            var genre4 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var genre5 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Comedy");
            var genre6 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Drama");
            var genre7 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Ecchi");
            var genre8 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Historical");
            var genre9 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Classics");
            var genre10 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Comic");
            var genre11 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Graphic Novel");
            var genre12 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Detective");
            var genre13 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Horror");
            var genre14 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Josei");
            var genre15 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Martial Arts");
            var genre16 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Mature");
            var genre17 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Mecha");
            var genre18 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Psychological");
            var genre19 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Romance");
            var genre20 = context.GenresModels.FirstOrDefault(t => t.GenreName == "School Life");
            var genre21 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Seinen");
            var genre22 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Shoujo");
            var genre23 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Shounen");
            var genre24 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Slice Of Life");
            var genre25 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Smut");
            var genre26 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Supernatural");
            var genre27 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Biographies and Autobiographies");
            var genre28 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Short Stories");
            var genre29 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Wuxia");
            var genre30 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Xuanhuan");

            #endregion genre

            var Artist1 = context.artistModels.FirstOrDefault(a => a.FirstName == "Alan" && a.LastName == "Lee");
            var Artist2 = context.artistModels.FirstOrDefault(a => a.FirstName == "John" && a.LastName == "Howe");
            var Artist3 = context.artistModels.FirstOrDefault(a => a.FirstName == "Ted" && a.LastName == "Nasmith");
            var Artist4 = context.artistModels.FirstOrDefault(a => a.FirstName == "Roger" && a.LastName == "Garland");
            var Artist5 = context.artistModels.FirstOrDefault(a => a.FirstName == "Donato" && a.LastName == "Giancola");
            var Artist6 = context.artistModels.FirstOrDefault(a => a.FirstName == "Michael" && a.LastName == "Hague");
            var Author1 = context.authorModels.FirstOrDefault(at => at.FirstName == "J.R.R.");
            var voiceactor1 = context.voiceActorModels.FirstOrDefault(v => v.FirstName == "Rob");
            var voiceactor2 = context.voiceActorModels.FirstOrDefault(v => v.FirstName == "Ian");
            var voiceactor3 = context.voiceActorModels.FirstOrDefault(v => v.FirstName == "Andy");
            var voiceactor4 = context.voiceActorModels.FirstOrDefault(v => v.FirstName == "Christopher");

            #region chars

            var Character1 = context.Characters.FirstOrDefault(c => c.CharacterName == "Puck");
            var Character2 = context.Characters.FirstOrDefault(c => c.CharacterName == "Frodo Baggins");
            var Character3 = context.Characters.FirstOrDefault(c => c.CharacterName == "Roronoa Zoro");
            var Character4 = context.Characters.FirstOrDefault(c => c.CharacterName == "Guts");
            var Character5 = context.Characters.FirstOrDefault(c => c.CharacterName == "Griffith");
            var Character6 = context.Characters.FirstOrDefault(c => c.CharacterName == "Casca");
            var Character7 = context.Characters.FirstOrDefault(c => c.CharacterName == "Zodd the Immortal");
            var Character8 = context.Characters.FirstOrDefault(c => c.CharacterName == "Farnese de Vandimion");
            var Character9 = context.Characters.FirstOrDefault(c => c.CharacterName == "Serpico");
            var Character10 = context.Characters.FirstOrDefault(c => c.CharacterName == "Ivalera");
            var Character11 = context.Characters.FirstOrDefault(c => c.CharacterName == "Usopp");
            var Character12 = context.Characters.FirstOrDefault(c => c.CharacterName == "Sanji");
            var Character13 = context.Characters.FirstOrDefault(c => c.CharacterName == "Tony Tony Chopper");
            var Character14 = context.Characters.FirstOrDefault(c => c.CharacterName == "Nico Robin");
            var Character15 = context.Characters.FirstOrDefault(c => c.CharacterName == "Franky");
            var Character16 = context.Characters.FirstOrDefault(c => c.CharacterName == "Portgas D. Ace");
            var Character17 = context.Characters.FirstOrDefault(c => c.CharacterName == "Edward Newgate (Whitebeard)");
            var Character18 = context.Characters.FirstOrDefault(c => c.CharacterName == "Boa Hancock");
            var Character19 = context.Characters.FirstOrDefault(c => c.CharacterName == "Brook");
            var Character20 = context.Characters.FirstOrDefault(c => c.CharacterName == "Izuku Midoriya");
            var Character21 = context.Characters.FirstOrDefault(c => c.CharacterName == "Rukia Kuchiki");
            var Character22 = context.Characters.FirstOrDefault(c => c.CharacterName == "Orihime Inoue");
            var Character23 = context.Characters.FirstOrDefault(c => c.CharacterName == "Uryuu Ishida");
            var Character24 = context.Characters.FirstOrDefault(c => c.CharacterName == "Katsuki Bakugo");
            var Character25 = context.Characters.FirstOrDefault(c => c.CharacterName == "All Might");
            var Character26 = context.Characters.FirstOrDefault(c => c.CharacterName == "Gandalf");
            var Character27 = context.Characters.FirstOrDefault(c => c.CharacterName == "Aragorn");
            var Character28 = context.Characters.FirstOrDefault(c => c.CharacterName == "Legolas");

            #endregion chars

            MangaModel lotr = new MangaModel
            {
                MangaName = "ThisIsACharacterTestToCheckIfATitleCouldbeLongerThanItShouldBePossibleToViewWhenWatchingThisFromANormal",
                PhotoPath = Path.Combine(ProcessUploadedFile("TestTest.png")),
                BlogModel = new BlogModel { mangaName = "ThisIsACharacterTestToCheckIfATitleCouldbeLongerThanItShouldBePossibleToViewWhenWatchingThisFromANormal" },
                ReleaseYear = new DateTime(1954, 7, 29),
                Description = "In a sleepy village in the Shire, young hobbit Frodo Baggins finds himself tasked with an immense quest; to destroy the One Ring and thus ensure the destruction of the Dark Lord Sauron. Alongside a fellowship of heroes including wizards, elves and dwarves, he must journey across the land of Middle-earth, facing danger at every turn.",
                CompletelyTranslated = "Yes",
                OfficalLanguage = "English",
                ISBN10 = "0618640150",
                ISBN13 = "978-0618640157",

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
             new AssociatedNames { nameString = "TestData"  },
                new AssociatedNames { nameString = "Fix Data for characters" } ,
        },
                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.lordoftherings.net/", OfficalWebsiteName = "Lord of the Rings Official Site", Twitter = "https://twitter.com/lordoftherings", Facebook = "https://www.facebook.com/lordoftheringstrilogy/", Line = null }
        },
                Authormodels = new List<AuthorModel>
        {
           Author1
        },
                ArtistModels = new List<ArtistModel>
{
                    Artist1, Artist2, Artist3, Artist4, Artist5, Artist6
},
                VoiceActors = new List<VoiceActorModel>
{
 voiceactor1, voiceactor2, voiceactor3, voiceactor4
},
                GenresModels = new List<GenresModel>
        {
           genre1, genre2, genre3, genre4, genre5, genre6, genre7, genre8, genre9, genre10, genre11, genre12, genre13, genre14, genre15, genre16, genre17, genre18, genre19, genre20, genre21, genre22, genre23, genre24, genre25, genre26, genre27, genre28, genre29, genre30
        },
                TagsModels = new List<TagModel>
    {
        tag1, tag2, tag3, tag4, tag5, tag6, tag7, tag8, tag9, tag10, tag11, tag12, tag13, tag14, tag15, tag16, tag17, tag18, tag19, tag20, tag21, tag22, tag23, tag24, tag25, tag26, tag27
    },
                Characters = new List<Character>
    {
    Character1, Character2,Character3,Character4,
   Character5,Character6
   ,Character7, Character8,Character9, Character10,Character11, Character12,Character13, Character14,  Character15, Character16, Character17, Character18, Character19, Character20, Character21,
   Character21, Character22, Character23, Character24, Character25, Character26, Character27, Character28
    },
            };
            context.mangaModels.Add(lotr);
            context.SaveChanges();
        }

        public void CreateSoloLeveling()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Level System");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Monsters");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Weak to Strong");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Shounen");
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
    Gen2ToManga1,
    Gen3ToManga1
},
                TagsModels = new List<TagModel>
{
    Tag1ToManga1,
    Tag2ToManga1,
    Tag3ToManga1
},
                Characters = new List<Character>
        {
  new Character
    {
        CharacterName = "Sung Jin-Woo",
        specie = "Human",
        Gender = "Male",
        Born = "March 20, 1997",
        PlaceOffResidence = "South Korea",
        World = "Solo Leveling",
        Nationality = "Korean",
        Education = "High School",
        Occupation = "Hunter",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SungJin.jpg")),
        Personality = "Determined, Strategic, Brave",
        FamousQuote = "I alone will become stronger!",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Shadow Monarch powers, Combat skills",
        Hobbies = "Training, Exploring dungeons",
        Likes = "Growth, Protecting others",
        Dislikes = "Monsters, Weakness",
        PersonalityTraits = "Tenacious, Calculated, Resilient"
    },
    new Character
    {
        CharacterName = "Cha Hae-In",
        specie = "Human",
        Gender = "Female",
        Born = "January 6, 1998",
        PlaceOffResidence = "South Korea",
        World = "Solo Leveling",
        Nationality = "Korean",
        Education = "Hunter Association",
        Occupation = "Hunter",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("ChaHaeIn.jpeg")),
        Personality = "Skilled, Focused, Independent",
        FamousQuote = "Challenges make me stronger!",
        EyeColor = "Blue",
        HairColor = "Black",
        Abilities = "Combat skills, Magic sensing",
        Hobbies = "Training, Reading",
        Likes = "Challenges, Personal growth",
        Dislikes = "Arrogance, Injustice",
        PersonalityTraits = "Dedicated, Strong-willed, Determined"
    },
 new Character
{
    CharacterName = "Go Gun-Hee",
    specie = "Human",
    Gender = "Male",
    Born = "Unknown",
    PlaceOffResidence = "South Korea",
    World = "Solo Leveling",
    Nationality = "Korean",
    Occupation = "Chairman of the Hunter Association",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("GoGunHee.jpeg")),
    Personality = "Wise, Charismatic, Visionary",
    EyeColor = "Brown",
    HairColor = "Grey",
    Likes = "Hunter community, Progress",
    Dislikes = "Threats to humanity, Corruption",
    PersonalityTraits = "Strategic, Influential, Caring"
},
    new Character
    {
        CharacterName = "Jin-Ah Sung",
        specie = "Human",
        Gender = "Female",
        Born = "Unknown",
        PlaceOffResidence = "South Korea",
        World = "Solo Leveling",
        Nationality = "Korean",
        Education = "High School",
        Occupation = "Sung Jin-Woo's sister",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("JinAhSung.jpg")),
        Personality = "Supportive, Caring, Strong",
        EyeColor = "Brown",
        HairColor = "Black",
        Likes = "Family, Photography",
        Dislikes = "Danger, Separation",
        PersonalityTraits = "Protective, Nurturing, Resilient"
    },
    },

                Authormodels = new List<AuthorModel>
    {
        new AuthorModel
    {
        FirstName = "Chu-Gong",
        Biography = "South Korean web novel author, best known for writing Solo Leveling, a highly popular web novel that has been adapted into a webtoon. Chu-Gong's story combines action, adventure, and fantasy in a unique world where hunters gain power to battle monsters.",
        BirthPlace = "South Korea",
        Gender = "Male",
        AuthorBorn = new DateTime(1980, 1, 1), // Hypothetical birth date
        AmountOfWork = 1, // Mainly known for Solo Leveling
        WorkingAt = "D&C Media",
        Twitter = null, // Assuming no public Twitter account
        NameInNative = "추공",
        Contact = "chugong@solo.com", // Hypothetical contact
        WikiPedia = null, // Assuming no dedicated Wikipedia page
    },
    },

                ArtistModels = new List<ArtistModel>
    {
        new ArtistModel
    {
        FirstName = "Jang",
        LastName = "Sung-Rak",
        Biography = "Known as Dubu, Jang Sung-Rak is the CEO of Redice Studio and the artist behind the Solo Leveling webtoon. His artwork brings to life the intricate details of the Solo Leveling world, captivating fans with dynamic action scenes and vivid character portrayals.",
        BirthPlace = "South Korea",
        Gender = "Male",
        ArtistBorn = new DateTime(1975, 6, 20), // Hypothetical birth date
        AmountOfWork = 1, // Solo Leveling
        WorkingAt = "Redice Studio",
        Twitter = "@Dubu_Redice", // Hypothetical handle
        NameInNative = "장성락 (Dubu)",
        Contact = "jangsung@solo.com", // Hypothetical contact
        WikiPedia = null, // Assuming no dedicated Wikipedia page
    },
    },

                VoiceActors = new List<VoiceActorModel> {  new VoiceActorModel
    {
        FirstName = "Taito",
        LastName = "Ban",
        Gender = "Male",
        Biography = "A Japanese voice actor known for voicing Sung Jin-Woo in the anime adaptation of Solo Leveling. His performance brings to life the depth and complexity of the protagonist.",
        BirthPlace = "Tokyo, Japan", // Hypothetical birthplace, as specific details might not be widely known
        VoiceBorn = new DateTime(1986, 4, 25), // Hypothetical birth date
        Twitter = "@TaitoBan", // Hypothetical handle, assuming availability
        NameInNative = "伴 太斗", // Standard representation
        Contact = "ban@voiceagency.jp", // Hypothetical contact
        WikiPedia = "https://myanimelist.net/people/48849/Taito_Ban", // Reference to a profile
        CharacterName = "Sung Jin-Woo"
    },
    new VoiceActorModel
    {
        FirstName = "Reina",
        LastName = "Ueda",
        Gender = "Female",
        Biography = "Japanese voice actress Reina Ueda is known for her role as Cha Hae-In in Solo Leveling. Her voice acting adds a rich layer of personality to the character, enhancing the story's immersive experience.",
        BirthPlace = "Toyama, Japan",
        VoiceBorn = new DateTime(1994, 1, 17),
        Twitter = "@ReinaUeda_Staff", // Hypothetical handle
        NameInNative = "上田 麗奈",
        Contact = "ueda@voiceagency.jp", // Hypothetical contact
        WikiPedia = "https://myanimelist.net/people/18593/Reina_Ueda",
        CharacterName = "Cha Hae-In"
    }, },
            };

            context.mangaModels.Add(manga);
            context.SaveChanges();
        }

        public void CreateTowerOfGod()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Monsters");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Weak to Strong");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Hunted Protagonist");
            var Tag4ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Near-Death Experience");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Fantasy");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");
            var GenreAt3 = context.GenresModels.FirstOrDefault(e => e.GenreName == "Shounen");
            DateTime myDate = DateTime.ParseExact("2010-06-30", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            MangaModel MangaTowerOfGod = new MangaModel
            {
                MangaName = "Tower of God",
                PhotoPath = Path.Combine(ProcessUploadedFile("TowerOfGod.jpeg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "Tower of God" },
                Description = "Bam climbs the Tower of God to reach the top and find his friend Rachel, but the Tower is full of deadly tests and other climbers who will do anything to reach the top.",
                CompletelyTranslated = "Ongoing",
                OfficalLanguage = "Korean",
                ISBN10 = "8952745013",
                ISBN13 = "978-8952745018",

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
          new AuthorModel
    {
        FirstName = "S.I.",
        LastName = "Uoo", // Assuming a typo in the initial entry, correcting to SIU's known pen name
        Biography = "Known by his pen name SIU (Slave In Utero), he is the author and artist behind the popular webtoon Tower of God. The series has garnered attention for its intricate plot and detailed world-building.",
        BirthPlace = "South Korea", // Specific birthplace details are not publicly disclosed
        Gender = "Male",
        AuthorBorn = new DateTime(1985, 11, 29), // Hypothetical birth date
        AmountOfWork = 1, // Tower of God
        WorkingAt = "Naver Webtoon",
        Twitter = "@SIU_Manwha", // Hypothetical handle, as specific social media details are scarce
        NameInNative = "S.I.Uoo",
        Contact = "siu@naverwebtoon.kr", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Tower_of_God",
    },
        },
                ArtistModels = new List<ArtistModel>
        {
             new ArtistModel
    {
        FirstName = "LEE",
        LastName = "Jong-hui", // Correcting to reflect SIU's real name
        Biography = "LEE Jong-hui, known by his pen name SIU, is both the author and artist of Tower of God. His work on the series showcases a unique art style that has contributed to its success across the globe.",
        BirthPlace = "South Korea", // As above, specific details are not widely disclosed
        Gender = "Male",
        ArtistBorn = new DateTime(1985, 11, 29), // Matching the author entry
        AmountOfWork = 1, // Reflecting his work on Tower of God
        WorkingAt = "Naver Webtoon",
        Twitter = "@SIU_Manwha", // Assuming the same hypothetical handle
        NameInNative = "이종휘",
        Contact = "leejonghui@naverwebtoon.kr", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Tower_of_God",
    },
        },
                VoiceActors = new List<VoiceActorModel> { new VoiceActorModel {FirstName = "Taichi",
    LastName = "Ichikawa",
    Gender = "Male",
    Biography = "Japanese voice actor known for his role as Twenty-Fifth Bam in the anime adaptation of Tower of God. His performance captures the innocence and determination of Bam, resonating well with fans of the series.",
    BirthPlace = "Tokyo, Japan",
    VoiceBorn = new DateTime(1992, 3, 29),
    Twitter = "@TaichiIchikawa", // Hypothetical handle
    NameInNative = "市川 太一",
    Contact = "ichikawa@voiceagency.jp", // Hypothetical contact
    WikiPedia = "https://en.wikipedia.org/wiki/Taichi_Ichikawa", // Assuming a page exists
    CharacterName = "Twenty-Fifth Bam" }, },
                GenresModels = new List<GenresModel>
        {
          Gen1ToManga1,
           Gen2ToManga1,Gen3ToManga1,
            GenreAt3
        },
                TagsModels = new List<TagModel>
        {
           Tag1ToManga1,
            Tag2ToManga1,
           Tag3ToManga1,
        },

                Characters = new List<Character>
        {
new Character {
    CharacterName = "Twenty-Fifth Baam",
    specie="Human",
    Gender="Male",
    Born="N/A",
    PlaceOffResidence = "The Tower",
    World="Tower of God",
    Nationality ="N/A",
    Education="N/A",
    Occupation="Irregular",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Baam.jpeg")),
    Personality = "Curious, Determined, Loyal",
    FamousQuote = "I want to follow my own path, not someone else's.",
    EyeColor = "Blue",
    HairColor = "Black",
    Abilities = "Shinsu manipulation, Wave controller",
    Hobbies = "Exploring the Tower, Protecting friends",
    Likes = "Rachel (initially), Friendship",
    Dislikes = "Being powerless, Betrayal",
    PersonalityTraits = "Adaptable, Resilient, Kind-hearted"
},
new Character {
    CharacterName = "Yuri Jahad",
    specie="Human",
    Gender="Female",
    Born="N/A",
    PlaceOffResidence = "The Tower",
    World="Tower of God",
    Nationality ="N/A",
    Education="N/A",
    Occupation="Princess of Jahad",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("YuriTOG.jpg")),
    Personality = "Brash, Adventurous, Charismatic",
    FamousQuote = "I'll do anything to make the Tower lively and fun!",
    EyeColor = "Red",
    HairColor = "Black",
    Abilities = "Shinsu manipulation, Fisherman",
    Hobbies = "Exploring, Battling strong opponents",
    Likes = "Freedom, Adventure",
    Dislikes = "Being controlled, Boredom",
    PersonalityTraits = "Bold, Outgoing, Resolute"
},
new Character {
    CharacterName = "Khun Aguero Agnis",
    specie="Human",
    Gender="Male",
    Born="N/A",
    PlaceOffResidence = "The Tower",
    World="Tower of God",
    Nationality ="N/A",
    Education="N/A",
    Occupation="Fisherman",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Khun.jpg")),
    Personality = "Cunning, Strategic, Manipulative",
    FamousQuote = "To achieve one's goals, one must do whatever it takes.",
    EyeColor = "Blue",
    HairColor = "Blonde",
    Abilities = "Shinsu manipulation, Light Bearer",
    Hobbies = "Planning, Outwitting opponents",
    Likes = "Knowledge, Power",
    Dislikes = "Being underestimated, Failure",
    PersonalityTraits = "Intelligent, Calculating, Resourceful"
},
new Character {
    CharacterName = "Rak Wraithraiser",
    specie="Crocus",
    Gender="Male",
    Born="N/A",
    PlaceOffResidence = "The Tower",
    World="Tower of God",
    Nationality ="N/A",
    Education="N/A",
    Occupation="Spear Bearer",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Rak.jpg")),
    Personality = "Direct, Fierce, Loyal",
    FamousQuote = "I am not 'gator', I am 'Rak Wraithraiser'!",
    EyeColor = "Yellow",
    HairColor = "Green",
    Abilities = "Shinsu manipulation, Spear Bearer",
    Hobbies = "Hunting, Challenging opponents",
    Likes = "Strength, Battle",
    Dislikes = "Being called 'gator', Weakness",
    PersonalityTraits = "Brave, Unyielding, Proud"
},
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
                ISBN13 = "978-0142437230",
                ISBN10 = "0142437239",

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
        new AuthorModel
    {
        FirstName = "Miguel de",
        LastName = "Cervantes",
        Biography = "An acclaimed Spanish writer who is widely regarded as one of the greatest writers in the Spanish language and one of the world's pre-eminent novelists. His masterpiece, Don Quixote, is considered a founding work of Western literature.",
        BirthPlace = "Alcalá de Henares, Spain",
        Gender = "Male",
        AuthorBorn = new DateTime(1547, 9, 29),
        AuthorDeath = new DateTime(1616, 4, 22),
        AmountOfWork = 2, // Don Quixote and various other works
        WorkingAt = "Spanish Royal Court", // Hypothetically speaking, given the era
        Twitter = null,
        NameInNative = "Miguel de Cervantes Saavedra",
        Contact = "info@literaturaespanola.es", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Miguel_de_Cervantes",
    },
    },
                ArtistModels = new List<ArtistModel> {
     new ArtistModel
    {
        FirstName = "Gustave",
        LastName = "Doré",
        Biography = "A French artist, engraver, illustrator, and sculptor. Doré worked primarily with wood engraving and steel engraving. He is renowned for his detailed illustrations that bring literary works to life, including the epic Don Quixote.",
        BirthPlace = "Strasbourg, France",
        Gender = "Male",
        ArtistBorn = new DateTime(1832, 1, 6),
        ArtistDeath = new DateTime(1883, 1, 23),
        NameInNative = "Paul Gustave Louis Christophe Doré",
        Contact = "info@doregallery.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Gustave_Dor%C3%A9",
    },
    new ArtistModel
    {
        FirstName = "Francisco",
        LastName = "Rico",
        Biography = "A Spanish literary critic and academician. He is an expert in Medieval and Golden Age Spanish Literature, with a focus on the works of Miguel de Cervantes. Rico has contributed extensively to the study and annotation of Don Quixote.",
        BirthPlace = "Barcelona, Spain", // Hypothetical, based on academic affiliation
        Gender = "Male",
        ArtistBorn = new DateTime(1942, 11, 27), // Hypothetical birth date
        NameInNative = "Francisco Rico Manrique",
        Contact = "frico@universidad.es", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Francisco_Rico", // Assuming a page exists
    }
                },
                VoiceActors = new List<VoiceActorModel> {
  new VoiceActorModel
    {
        FirstName = "John",
        LastName = "Lescault",
        Gender = "Male",
        Biography = "An accomplished voice actor known for his rich narrative style, John Lescault has lent his voice to a variety of audiobooks and dramatic readings. His ability to convey deep, nuanced emotions makes him a sought-after narrator for classic literature.",
        BirthPlace = "Washington, D.C., USA", // Hypothetical
        VoiceBorn = new DateTime(1960, 4, 15), // Hypothetical birth date
        Twitter = "@JohnLescault", // Hypothetical handle
        NameInNative = "John Lescault",
        Contact = "jlescault@voiceactorsguild.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/John_Lescault", // Assuming a page exists
    },
    new VoiceActorModel
    {
        FirstName = "Robert",
        LastName = "Whitmire",
        Gender = "Male",
        Biography = "Robert Whitmire, with his dynamic range and expressive depth, has become a prominent voice in the world of audiobook narration. His interpretations of classic and contemporary works alike resonate with listeners around the globe.",
        BirthPlace = "Los Angeles, California, USA", // Hypothetical
        VoiceBorn = new DateTime(1965, 7, 20), // Hypothetical birth date
        Twitter = "@RobertWhitmire", // Hypothetical handle
        NameInNative = "Robert Whitmire",
        Contact = "rwhitmire@voiceactorsguild.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Robert_Whitmire", // Assuming a page exists
    },
    new VoiceActorModel
    {
        FirstName = "George",
        LastName = "Guidall",
        Gender = "Male",
        Biography = "George Guidall is an award-winning narrator with hundreds of audiobooks to his credit. Renowned for his storytelling prowess, Guidall's voice has brought to life some of literature's most enduring works, making him a legend in the audiobook industry.",
        BirthPlace = "New York City, New York, USA", // Real data, as Guidall is a known narrator
        VoiceBorn = new DateTime(1938, 6, 7), // Real birth date
        Twitter = "@GeorgeGuidall", // Hypothetical handle
        NameInNative = "George Guidall",
        Contact = "gguidall@audiobooknarrators.com", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/George_Guidall", // Real Wikipedia page
    },
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
new Character
    {
        CharacterName = "Don Quixote",
        specie = "Human",
        Gender = "Male",
        Born = "N/A",
        PlaceOffResidence = "Spain",
        World = "Don Quixote",
        Nationality = "Spanish",
        Education = "N/A",
        Occupation = "Knight-errant",
        Background = "Formerly a nobleman named Alonso Quixano, he becomes delusional and believes himself to be the legendary knight Don Quixote.",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("DonQuixote.jpg")),
        Personality = "Idealistic, Delusional, Chivalrous",
        FamousQuote = "For I am Don Quixote of La Mancha, the Knight of the Sorrowful Countenance!",
        EyeColor = "Brown",
        HairColor = "Grey",
        Abilities = "Fighting windmills, Rescuing damsels",
        Hobbies = "Reading tales of chivalry, Questing",
        Likes = "Chivalry, Adventure",
        Dislikes = "Reality, Mundane life",
        PersonalityTraits = "Courageous, Determined, Eccentric"
    },
    new Character
    {
        CharacterName = "Sancho Panza",
        specie = "Human",
        Gender = "Male",
        Born = "N/A",
        PlaceOffResidence = "Spain",
        World = "Don Quixote",
        Nationality = "Spanish",
        Education = "N/A",
        Occupation = "Squire",
        Background = "A simple and practical farmer who becomes Don Quixote's loyal squire.",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("SanchoPanza.jpg")),
        Personality = "Practical, Loyal, Superstitious",
        FamousQuote = "May God give you good luck, Sir Quixote, and I wish I had a good bough to hit you with.",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Tending to animals, Following orders",
        Hobbies = "Eating, Talking",
        Likes = "Food, Simplicity",
        Dislikes = "Adventures, Risky situations",
        PersonalityTraits = "Down-to-earth, Humble, Trustworthy"
    },
    new Character
    {
        CharacterName = "Dulcinea del Toboso",
        specie = "Human",
        Gender = "Female",
        Born = "N/A",
        PlaceOffResidence = "Spain",
        World = "Don Quixote",
        Nationality = "Spanish",
        Education = "N/A",
        Occupation = "Noblewoman",
        Background = "A beautiful woman whom Don Quixote imagines to be a noble lady, though she is merely a peasant woman named Aldonza Lorenzo.",
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Dulcinea.jpg")),
        Personality = "Illusory, Imaginary, Enigmatic",
        FamousQuote = "Her beauty superhuman, for in her they were all, though she was only one.",
        EyeColor = "Unknown",
        HairColor = "Unknown",
        Abilities = "None (imaginary character)",
        Hobbies = "None (imaginary character)",
        Likes = "None (imaginary character)",
        Dislikes = "None (imaginary character)",
        PersonalityTraits = "Romantic, Idealized, Unattainable"
    },
},
            };
            context.mangaModels.Add(DonQuixote);
            context.SaveChanges();
        }

        public void CreateMyHeroAcademia()
        {
            var Tag1ToManga = context.TagModels.FirstOrDefault(t => t.TagName == "SuperHeroes");
            var Tag2ToManga = context.TagModels.FirstOrDefault(t => t.TagName == "Weak to Strong");
            var Gen1ToManga = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga = context.GenresModels.FirstOrDefault(t => t.GenreName == "Shounen");
            var Gen3ToManga = context.GenresModels.FirstOrDefault(t => t.GenreName == "Adventure");

            DateTime myDate = DateTime.ParseExact("2014-07-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime started = DateTime.ParseExact("2014-07-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            MangaModel MyHeroAcademia = new MangaModel
            {
                MangaName = "My Hero Academia",
                PhotoPath = Path.Combine(ProcessUploadedFile("MHA.jpg")),
                ReleaseYear = myDate,
                BlogModel = new BlogModel { mangaName = "My Hero Academia" },
                Description = "In a world where nearly every human has some form of superpower, or 'quirk', aspiring heroes join U.A. High School to learn how to become a superhero.",
                CompletelyTranslated = "Ongoing",
                OfficalLanguage = "Japanese",
                ISBN13 = "978-4088802640",
                ISBN10 = "4088802646",
                relatedSeries = null,
                EndingYear = DateTime.Now,
                StatusInCountryOfOrigin = "Ongoing",
                Type = "Manga",
                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Bones Inc.", Started = started, StudioWebsite = "https://bones.co.jp/", Biography = "N/A" } },
                OriginalPublisher = "Shueisha",
                orignalWebtoon = "N/A",
                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage> {
            new BuyPage { BuyWebsite = "https://www.amazon.com/My-Hero-Academia-Vol-1/dp/1421582694", _Languages = new List<Languages> { EnglishLanguage }  },
            new BuyPage { BuyWebsite = "https://www.amazon.co.jp/僕のヒーローアカデミア-1-ジャンプコミックス-堀越-耕平/dp/4088802646", _Languages = new List<Languages> { JapaneseLanguage }  },
        },
                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "Boku no Hero Academia"  },
        },
                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://heroaca.com/", OfficalWebsiteName = "Official Site", Twitter = "N/A", Facebook = "N/A", Line = "N/A" },
        },
                Authormodels = new List<AuthorModel>
        {
            new AuthorModel {    FirstName = "Kohei",
        LastName = "Horikoshi",
        Biography = "A Japanese manga artist known for creating the My Hero Academia series, which has become a worldwide phenomenon for its unique take on the superhero genre, blending Japanese manga aesthetics with Western superhero motifs.",
        BirthPlace = "Aichi Prefecture, Japan",
        Gender = "Male",
        AuthorBorn = new DateTime(1986, 11, 20),
        AmountOfWork = 1, // My Hero Academia
        WorkingAt = "Shueisha",
        Twitter = "@horikoshiko",
        NameInNative = "堀越 耕平",
        Contact = "info@shueisha.co.jp", // Hypothetical contact based on publisher
        WikiPedia = "https://en.wikipedia.org/wiki/K%C5%8Dhei_Horikoshi"},
        },
                ArtistModels = new List<ArtistModel> {
            new ArtistModel {  FirstName = "Kohei",
        LastName = "Horikoshi",
        Biography = "Renowned for his work on My Hero Academia, Kohei Horikoshi has garnered acclaim for his dynamic art style and compelling character designs, contributing significantly to the manga's success both in Japan and internationally.",
        BirthPlace = "Aichi Prefecture, Japan",
        Gender = "Male",
        ArtistBorn = new DateTime(1986, 11, 20),
        AmountOfWork = 1, // Reiterating My Hero Academia
        WorkingAt = "Shueisha",
        Twitter = "@horikoshiko",
        NameInNative = "堀越 耕平",
        Contact = "info@shueisha.co.jp", // Assuming similar contact details
        WikiPedia = "https://en.wikipedia.org/wiki/K%C5%8Dhei_Horikoshi" }
        },
                VoiceActors = new List<VoiceActorModel> {
              new VoiceActorModel
    {
        FirstName = "Daiki",
        LastName = "Yamashita",
        Gender = "Male",
        Biography = "Daiki Yamashita is celebrated for his role as Izuku Midoriya in My Hero Academia. His ability to convey the character's heartfelt determination and vulnerability has made Midoriya one of the most beloved protagonists in recent anime.",
        BirthPlace = "Shizuoka Prefecture, Japan",
        VoiceBorn = new DateTime(1989, 9, 7),
        Twitter = "@D_Yamashita48",
        NameInNative = "山下 大輝",
        Contact = "yamashita@voiceagency.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Daiki_Yamashita",
        CharacterName = "Izuku Midoriya"
    },
    new VoiceActorModel
    {
        FirstName = "Kenta",
        LastName = "Miyake",
        Gender = "Male",
        Biography = "Kenta Miyake is known for his role as All Might in My Hero Academia, bringing to life the iconic symbol of peace with his deep, powerful voice that's both inspiring and comforting.",
        BirthPlace = "Okaya, Nagano, Japan",
        VoiceBorn = new DateTime(1977, 8, 23),
        Twitter = "@MiyakeKenta",
        NameInNative = "三宅 健太",
        Contact = "miyake@voiceagency.jp", // Hypothetical contact
        WikiPedia = "https://en.wikipedia.org/wiki/Kenta_Miyake",
        CharacterName = "All Might"
    },
            new VoiceActorModel {  FirstName = "Nobuhiko",
        LastName = "Okamoto",
        Gender = "Male",
        Biography = "Nobuhiko Okamoto is acclaimed for his versatile voice acting skills, notably for his role as Katsuki Bakugo in My Hero Academia. His portrayal adds depth to Bakugo's fiery, complex personality.",
        BirthPlace = "Tokyo, Japan",
        VoiceBorn = new DateTime(1986, 10, 24),
        Twitter = "@nobuhiko_okamoto",
        NameInNative = "岡本 信彦",
        Contact = "okamoto@voiceagency.jp", // Hypothetical contact
         WikiPedia = "https://en.wikipedia.org/wiki/Nobuhiko_Okamoto",
        CharacterName = "Katsuki Bakugou" },
        },
                GenresModels = new List<GenresModel>
        {
            Gen1ToManga, Gen2ToManga, Gen3ToManga
        },
                TagsModels = new List<TagModel>
        {
            Tag1ToManga, Tag2ToManga
        },
                Characters = new List<Character>
        {
            new Character
            {
                CharacterName = "Izuku Midoriya",
                specie = "Human",
                Gender = "Male",
                Born = "July 15",
                PlaceOffResidence = "Japan",
                World = "My Hero Academia",
                Nationality = "Japanese",
                Education = "U.A. High School",
                Occupation = "Student",
                Background = "A boy born without superpowers in a world where they are common, but still dreams of becoming a hero.",
                PhotoPath = Path.Combine(CharacterProcessUploadedFile("IzukuMidoriya.jpg")),
                Personality = "Kind, Determined, Brave",
                FamousQuote = "When you have to save someone, they're usually in a scary situation. A true hero saves not only their lives but also their spirits.",
                EyeColor = "Green",
                HairColor = "Green",
                Abilities = "One For All",
                Hobbies = "Studying Heroes",
                Likes = "Heroes, Quirks",
                Dislikes = "Villains, Injustice",
                PersonalityTraits = "Optimistic, Intelligent, Humble"
            },
            new Character
{
    CharacterName = "Katsuki Bakugo",
    specie = "Human",
    Gender = "Male",
    Born = "April 20",
    PlaceOffResidence = "Japan",
    World = "My Hero Academia",
    Nationality = "Japanese",
    Education = "U.A. High School",
    Occupation = "Student",
    Background = "A student in Class 1-A at U.A. High School, training to become a Pro Hero. He has a Quirk named Explosion.",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Katsuki-Bakugou.jpg")),
    Personality = "Arrogant, Short-tempered, Confident",
    FamousQuote = "I just wanna win the way I wanna win. Is that so wrong?",
    EyeColor = "Red",
    HairColor = "Blond",
    Abilities = "Explosion",
    Hobbies = "None",
    Likes = "Victory, Himself",
    Dislikes = "Losing, Weakness",
    PersonalityTraits = "Competitive, Aggressive, Driven"
},
new Character
{
    CharacterName = "All Might",
    specie = "Human",
    Gender = "Male",
    Born = "June 10",
    PlaceOffResidence = "Japan",
    World = "My Hero Academia",
    Nationality = "Japanese",
    Education = "U.A. High School",
    Occupation = "Teacher, Former Pro Hero",
    Background = "The former No. 1 Pro Hero who bore the title of the world's Symbol of Peace. He teaches Foundational Hero Studies at U.A. High School.",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("AllMight.jpg")),
    Personality = "Charismatic, Optimistic, Caring",
    FamousQuote = "I am here!",
    EyeColor = "Blue",
    HairColor = "Blond",
    Abilities = "One For All",
    Hobbies = "Training Heroes",
    Likes = "Peace, Justice",
    Dislikes = "Villains, Injustice",
    PersonalityTraits = "Inspirational, Courageous, Kind-hearted"
}
        },
            };
            context.mangaModels.Add(MyHeroAcademia);
            context.SaveChanges();
        }

        public void CreateCodeGeass()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Mind Control");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Betrayal");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Politics");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Mecha");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Drama");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Thriller");
            DateTime started = DateTime.ParseExact("2006-10-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var manga = new MangaModel
            {
                MangaName = "Code Geass: Lelouch of the Rebellion",
                PhotoPath = Path.Combine(ProcessUploadedFile("CodeGeass.jpg")),
                ReleaseYear = new DateTime(2006, 10, 5),
                BlogModel = new BlogModel { mangaName = "Code Geass: Lelouch of the Rebellion" },
                Description = "In an alternate reality, Lelouch vi Britannia gains the power of Geass, which allows him to control minds. With this power, he seeks to destroy the Britannian Empire and create a better world.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "4047139281",
                ISBN13 = "978-4047139287",

                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Sunrise", Started = started, StudioWebsite = "http://www.sunrise-inc.co.jp/" } },
                EndingYear = new DateTime(2008, 7, 28),
                StatusInCountryOfOrigin = "50, completed",
                Type = "Manga",
                OriginalPublisher = "Kadokawa Shoten",
                orignalWebtoon = "N/A",

                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Code-Geass-Lelouch-Rebellion-Vol/dp/4047139281", _Languages = new List<Languages> { EnglishLanguage },  },
            new BuyPage { BuyWebsite = "https://www.kadokawa.co.jp/product/200708000175/", _Languages = new List<Languages> { JapaneseLanguage },  },
        },

                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "Code Geass: Hangyaku no Lelouch" },
            new AssociatedNames { nameString = "Code Geass: Lelouch of the Rebellion" },
            new AssociatedNames { nameString = "Code Geass: Rebellion of Lelouch" },
            new AssociatedNames { nameString = "Code Geass: 反叛的鲁鲁修" }
        },

                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.geass.jp/", OfficalWebsiteName = "Official Website" },
        },

                GenresModels = new List<GenresModel>
        {
            Gen1ToManga1,
            Gen2ToManga1,
            Gen3ToManga1
        },
                TagsModels = new List<TagModel>
        {
            Tag1ToManga1,
            Tag2ToManga1,
            Tag3ToManga1
        },
                Characters = new List<Character>
        {
 new Character
    {
        CharacterName = "Lelouch vi Britannia",
        specie = "Human",
        Gender = "Male",
        Born = "December 5, 2000",
        PlaceOffResidence = "Holy Britannian Empire",
        World = "Code Geass",
        Nationality = "Britannian",
        Occupation = "Exiled Prince, Leader of the Black Knights",
        Height = 178,
        Weight = 68,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Lelouch.jpeg")),
        Personality = "Strategic, Charismatic, Complex",
        FamousQuote = "The only ones who should kill are those who are prepared to be killed.",
        EyeColor = "Violet",
        HairColor = "Black",
        Abilities = "Tactical genius, Geass power",
        Hobbies = "Chess, Planning",
        Likes = "Justice, Nunnally",
        Dislikes = "Tyranny, Loss",
        PersonalityTraits = "Ambitious, Calculating, Protective"
    },
    new Character
    {
        CharacterName = "C.C.",
        specie = "Human",
        Gender = "Female",
        Born = "Unknown",
        PlaceOffResidence = "World of C",
        World = "Code Geass",
        Nationality = "Unknown",
        Occupation = "Witch, Contractor of the Power of Geass",
        Height = 162,
        Weight = 52,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("CC.jpg")),
        Personality = "Mysterious, Enigmatic, Playful",
        FamousQuote = "The power of kings will wither away, but I'll be waiting for you at the future's end.",
        EyeColor = "Amber",
        HairColor = "Green",
        Abilities = "Power of Geass, Immortality",
        Hobbies = "Eating Pizza, Observing",
        Likes = "Freedom, Witnessing human drama",
        Dislikes = "Immortality, Loneliness",
        PersonalityTraits = "Reserved, Nonchalant, Empathetic"
    },
    new Character
    {
        CharacterName = "Suzaku Kururugi",
        specie = "Human",
        Gender = "Male",
        Born = "September 10, 2000",
        PlaceOffResidence = "Holy Britannian Empire",
        World = "Code Geass",
        Nationality = "Britannian",
        Occupation = "Knight of the Round, Member of the Black Knights",
        Height = 178,
        Weight = 67,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Suzaku.jpg")),
        Personality = "Idealistic, Just, Conflicted",
        FamousQuote = "I'm not wrong. I can't be wrong.",
        EyeColor = "Brown",
        HairColor = "Black",
        Abilities = "Advanced combat skills, Lancelot pilot",
        Hobbies = "Kendo, Seeking justice",
        Likes = "Justice, Euphemia",
        Dislikes = "Violence, Injustice",
        PersonalityTraits = "Honorable, Driven, Self-sacrificing"
    },
    new Character
    {
        CharacterName = "Kallen Kozuki",
        specie = "Human",
        Gender = "Female",
        Born = "March 29, 2000",
        PlaceOffResidence = "Area 11",
        World = "Code Geass",
        Nationality = "Japanese",
        Occupation = "Member of the Black Knights",
        Height = 170,
        Weight = 58,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Kallen.jpg")),
        Personality = "Brave, Rebellious, Compassionate",
        FamousQuote = "You're wrong! It's not the world that's messed up; it's those of us in it!",
        EyeColor = "Blue",
        HairColor = "Red",
        Abilities = "Knightmare piloting, Combat skills",
        Hobbies = "Motorcycle racing, Cooking",
        Likes = "Freedom, Her friends",
        Dislikes = "Britannian oppression, Injustice",
        PersonalityTraits = "Daring, Empathetic, Tenacious"
    },
        },

                Authormodels = new List<AuthorModel>(),
                ArtistModels = new List<ArtistModel>(),
                VoiceActors = new List<VoiceActorModel>(),
            };

            context.mangaModels.Add(manga);
            context.SaveChanges();
        }

        public void CreateBleach()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Fantasy World");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Absent Parent");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Death");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Supernatural");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Shounen");
            DateTime started = DateTime.ParseExact("2001-08-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var manga = new MangaModel
            {
                MangaName = "Bleach",
                PhotoPath = Path.Combine(ProcessUploadedFile("Bleach.jpg")),
                ReleaseYear = new DateTime(2001, 8, 7),
                BlogModel = new BlogModel { mangaName = "Bleach" },
                Description = "Ichigo Kurosaki becomes a Soul Reaper, a protector of the living world against evil spirits and guiding the souls of the deceased to the afterlife.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "4088730172",
                ISBN13 = "978-4088730175",

                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Studio Pierrot", Started = started, StudioWebsite = "https://www.pierrot.jp/" } },
                EndingYear = new DateTime(2016, 8, 22),
                StatusInCountryOfOrigin = "74, completed",
                Type = "Manga",
                OriginalPublisher = "Shueisha",
                orignalWebtoon = "N/A",

                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Bleach-Box-Set-Vols-1-21/dp/1421526107", _Languages = new List<Languages> { EnglishLanguage },  },
            new BuyPage { BuyWebsite = "https://books.shueisha.co.jp/CGI/search/syousai_put.cgi?isbn_cd=4-08-873017-2", _Languages = new List<Languages> { JapaneseLanguage },  },
        },

                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "BLEACH-ブリーチ-" },
            new AssociatedNames { nameString = "블리치" },
        },

                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.j-bleach.com/", OfficalWebsiteName = "Official Website" },
        },

                GenresModels = new List<GenresModel>
        {
            Gen1ToManga1,
            Gen2ToManga1,
            Gen3ToManga1
        },
                TagsModels = new List<TagModel>
        {
            Tag1ToManga1,
            Tag2ToManga1,
            Tag3ToManga1
        },
                Characters = new List<Character>
        {
new Character
    {
        CharacterName = "Ichigo Kurosaki",
        specie = "Human",
        Gender = "Male",
        Born = "July 15, 1989",
        PlaceOffResidence = "Karakura Town",
        World = "Bleach",
        Nationality = "Japanese",
        Occupation = "Soul Reaper",
        Height = 174,
        Weight = 61,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Ichigo.jpeg")),
        Personality = "Brave, Determined, Protective",
        FamousQuote = "If you wanna know who I am, I'm the guy who's gonna kill you!",
        EyeColor = "Brown",
        HairColor = "Orange",
        Abilities = "Zanpakuto, Hollow powers",
        Hobbies = "Protecting, Fighting",
        Likes = "Family, Friends",
        Dislikes = "Injustice, Cruelty",
        PersonalityTraits = "Fearless, Loyal, Strong-willed"
    },
    new Character
    {
        CharacterName = "Rukia Kuchiki",
        specie = "Shinigami",
        Gender = "Female",
        Born = "January 14",
        PlaceOffResidence = "Soul Society",
        World = "Bleach",
        Nationality = "Japanese",
        Occupation = "Soul Reaper",
        Height = 144,
        Weight = 42,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Rukia.jpg")),
        Personality = "Serious, Caring, Skilled",
        FamousQuote = "If you were to turn into a snake tomorrow, and began devouring humans, and from the same mouth you started devouring humans, you cried out to me 'I love you', would I still be able to say 'I love you' the same way I do today?",
        EyeColor = "Violet",
        HairColor = "Black",
        Abilities = "Kidou, Zanpakuto",
        Hobbies = "Training, Reading",
        Likes = "Teaching, Helping",
        Dislikes = "Soul Society's rules, Injustice",
        PersonalityTraits = "Responsible, Stern, Dedicated"
    },
    new Character
    {
        CharacterName = "Orihime Inoue",
        specie = "Human",
        Gender = "Female",
        Born = "September 3",
        PlaceOffResidence = "Karakura Town",
        World = "Bleach",
        Nationality = "Japanese",
        Occupation = "High School Student",
        Height = 157,
        Weight = 45,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Orihime.jpeg")),
        Personality = "Kind-hearted, Naive, Optimistic",
        FamousQuote = "I chose this. I chose all of this. This time, I want to protect Ichigo.",
        EyeColor = "Brown",
        HairColor = "Orange",
        Abilities = "Santen Kesshun, Shun Shun Rikka",
        Hobbies = "Cooking, Drawing",
        Likes = "Friends, Peace",
        Dislikes = "Conflict, Sadness",
        PersonalityTraits = "Empathetic, Cheerful, Supportive"
    },
    new Character
    {
        CharacterName = "Uryuu Ishida",
        specie = "Human",
        Gender = "Male",
        Born = "November 6",
        PlaceOffResidence = "Karakura Town",
        World = "Bleach",
        Nationality = "Japanese",
        Occupation = "High School Student, Quincy",
        Height = 182,
        Weight = 68,
        PhotoPath = Path.Combine(CharacterProcessUploadedFile("Uryuu.jpeg")),
        Personality = "Proud, Analytical, Stern",
        FamousQuote = "I don't have to win. But I have to fight.",
        EyeColor = "Blue",
        HairColor = "Blue",
        Abilities = "Spiritual energy manipulation, Quincy techniques",
        Hobbies = "Sewing, Studying",
        Likes = "Independence, Justice",
        Dislikes = "Hollows, Arrogance",
        PersonalityTraits = "Determined, Intelligent, Self-reliant"
    },
        },

                Authormodels = new List<AuthorModel>(),
                ArtistModels = new List<ArtistModel>(),
                VoiceActors = new List<VoiceActorModel>(),
            };

            context.mangaModels.Add(manga);
            context.SaveChanges();
        }

        public void CreateAirGear()
        {
            var Tag1ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Absent Parent");
            var Tag2ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Training");
            var Tag3ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Neighbor");
            var Tag4ToManga1 = context.TagModels.FirstOrDefault(t => t.TagName == "Male lead");
            var Gen1ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Sports");
            var Gen2ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Action");
            var Gen3ToManga1 = context.GenresModels.FirstOrDefault(t => t.GenreName == "Comedy");

            DateTime started = DateTime.ParseExact("2002-11-18", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var manga = new MangaModel
            {
                MangaName = "Air Gear",
                PhotoPath = Path.Combine(ProcessUploadedFile("AirGear.jpg")),
                ReleaseYear = new DateTime(2002, 11, 18),
                BlogModel = new BlogModel { mangaName = "Air Gear" },
                Description = "A group of young students participates in an underground form of inline skating called Air Treck, using high-tech rollerblades to perform gravity-defying stunts and engage in intense battles.",
                CompletelyTranslated = "Completed",
                OfficalLanguage = "Japanese",
                ISBN10 = "4063192731",
                ISBN13 = "978-4063192734",

                StudioModels = new List<StudioModel> { new StudioModel { GroupName = "Toei Animation", Started = started, StudioWebsite = "https://www.toei-anim.co.jp/" } },
                EndingYear = new DateTime(2012, 5, 23),
                StatusInCountryOfOrigin = "37, completed",
                Type = "Manga",
                OriginalPublisher = "Kodansha",
                orignalWebtoon = "N/A",

                AllLanguages = new List<Languages> { JapaneseLanguage, EnglishLanguage },
                BuyPages = new List<BuyPage>
        {
            new BuyPage { BuyWebsite = "https://www.amazon.com/Air-Gear-Vol-1-Oh-Great/dp/0345491896", _Languages = new List<Languages> { EnglishLanguage },  },
            new BuyPage { BuyWebsite = "https://books.shueisha.co.jp/CGI/search/syousai_put.cgi?isbn_cd=4-06-319273-1", _Languages = new List<Languages> { JapaneseLanguage },  },
        },

                AssociatedNames = new List<AssociatedNames>
        {
            new AssociatedNames { nameString = "エア・ギア" },
        },

                OfficalWebsites = new List<OfficalWebsite>
        {
            new OfficalWebsite { OfficalWebsiteString = "https://www.kc.kodansha.co.jp/product?item=0000049145", OfficalWebsiteName = "Kodansha Comics" },
        },

                GenresModels = new List<GenresModel>
        {
            Gen1ToManga1,
            Gen2ToManga1,
            Gen3ToManga1,
        },
                TagsModels = new List<TagModel>
        {
            Tag1ToManga1,
            Tag2ToManga1,
            Tag3ToManga1,
            Tag4ToManga1
        },
                Characters = new List<Character>
        {
          new Character
{
    CharacterName = "Itsuki Minami",
    specie = "Human",
    Gender = "Male",
    PlaceOffResidence = "East Tokyo",
    World = "Air Gear",
    Nationality = "Japanese",
    Occupation = "Storm Rider, Leader of Team Kogarasumaru",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Itsuki.jpg")),
    Personality = "Reckless, Brave, Determined",
    FamousQuote = "I'm not flying! I'm falling with style!",
    EyeColor = "Brown",
    HairColor = "Brown",
    Abilities = "Wind Regalia, AT Rider",
    Hobbies = "Skating, Fighting",
    Likes = "Air Trecks, His friends, Challenges",
    Dislikes = "Authority, Losing",
    PersonalityTraits = "Fearless, Protective, Confident"
},

new Character
{
    CharacterName = "Simca",
    specie = "Human",
    Gender = "Female",
    PlaceOffResidence = "Tokyo",
    World = "Air Gear",
    Nationality = "Japanese",
    Occupation = "Tuner, Former leader of Sleeping Forest",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Simca.jpg")),
    Personality = "Free-spirited, Playful, Mysterious",
    FamousQuote = "It's the sky that calls out to me.",
    EyeColor = "Green",
    HairColor = "Pink",
    Abilities = "Great AT Rider, Tuner",
    Hobbies = "Skating, Singing",
    Likes = "Freedom, Itsuki",
    Dislikes = "Restrictions, Betrayal",
    PersonalityTraits = "Charming, Supportive, Cunning"
},

new Character
{
    CharacterName = "Kazu",
    specie = "Human",
    Gender = "Male",
    PlaceOffResidence = "East Tokyo",
    World = "Air Gear",
    Nationality = "Japanese",
    Occupation = "Storm Rider, Member of Team Kogarasumaru",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Kazu.jpg")),
    Personality = "Loyal, Courageous, Kind-hearted",
    FamousQuote = "As long as I can fly, I'll keep pushing forward.",
    EyeColor = "Brown",
    HairColor = "Blonde",
    Abilities = "Flame Regalia, AT Rider",
    Hobbies = "Skating",
    Likes = "Air Trecks, Friendship, Challenges",
    Dislikes = "Injustice, Losing",
    PersonalityTraits = "Reliable, Brave, Determined"
},

new Character
{
    CharacterName = "Agito/Akito",
    specie = "Human",
    Gender = "Male",
    PlaceOffResidence = "Tokyo",
    World = "Air Gear",
    Nationality = "Japanese",
    Occupation = "Storm Rider, Fang King",
    PhotoPath = Path.Combine(CharacterProcessUploadedFile("Agito_Akito.jpg")),
    Personality = "Aggressive (Agito) / Kind-hearted (Akito)",
    FamousQuote = "Shut up, or I'll eat you alive!",
    EyeColor = "Red",
    HairColor = "Blue",
    Abilities = "Fang Regalia, Switching Personalities",
    Hobbies = "Skating, Fighting (Agito) / Cooking (Akito)",
    Likes = "Dominance (Agito) / Ikki and friends (Akito)",
    Dislikes = "Weakness (Agito) / Conflict (Akito)",
    PersonalityTraits = "Violent, Fierce (Agito) / Gentle, Caring (Akito)"
}
        },

                Authormodels = new List<AuthorModel>(),
                ArtistModels = new List<ArtistModel>(),
                VoiceActors = new List<VoiceActorModel>(),
            };

            context.mangaModels.Add(manga);
            context.SaveChanges();
        }
    }
}

//public async Task ReviewSeed()
//{
//    // Creating or finding a manga

//    var user = await context.UserModels.FirstOrDefaultAsync(u => u.UserName == "TestUser");
//    var manga = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
//    var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Solo Leveling");
//    var manga3 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Tower of God");
//    var manga4 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Sailor Moon");
//    if (user != null)
//    {
//        // Find or create manga

//        // Creating a review
//        var review = new Review
//        {
//            StylesScore = 4.5,
//            StoryScore = 4,
//            GrammarScore = 4.5,
//            CharactersScore = 5,
//            Title = "Best Manga EVER",
//            Content = "Berserk is a dark and gritty masterpiece. The art style is incredibly detailed, and the story is intense and captivating. The characters are complex and well-developed. This series is a must-read for fans of dark fantasy."
//        };
//        review.UserModels = new List<UserModel>() { user };
//        review.MangaModels = new List<MangaModel>() { manga };

//        var review2 = new Review
//        {
//            StylesScore = 4.5,
//            StoryScore = 5,
//            GrammarScore = 4.5,
//            CharactersScore = 5,
//            Created = DateTime.Now,
//            Title = "Solo Best",
//            Content = "Solo Leveling is a highly addictive webtoon. The art is stunning, and the story keeps you hooked with its action-packed sequences and intriguing world-building. The characters are well-designed, and the pacing is great. It's a must-read for fans of the genre."
//        };
//        review2.UserModels = new List<UserModel>() { user };
//        review2.MangaModels = new List<MangaModel>() { manga2 };

//        var review3 = new Review
//        {
//            StylesScore = 4.5,
//            StoryScore = 3.0,
//            GrammarScore = 3.5,
//            Created = DateTime.Now,
//            CharactersScore = 4.5,
//            Title = "Tower Of The G",
//            Content = "Tower of God is an epic adventure with a unique premise. The art style may take some getting used to, but the story is engaging and full of twists. The characters are diverse and intriguing. It's a series that keeps you guessing and wanting more."
//        };
//        review3.UserModels = new List<UserModel>() { user };
//        review3.MangaModels = new List<MangaModel>() { manga3 };
//        context.Reviews.Add(review);
//        context.Reviews.Add(review2);
//        context.Reviews.Add(review3);
//        await context.SaveChangesAsync();
//    }
//    else
//    {
//        throw new Exception("Test user not found.");
//    }
//    var user2 = new UserModel { UserName = "TestUser2", Email = "TestUser2@hotmail.com", Allias = "WorldEater" };
//    var result2 = await userManager.CreateAsync(user2, "Rojhat123!");
//    if (result2.Succeeded)
//    {
//        var resultRole = await userManager.AddToRoleAsync(user2, "Updater");
//    }
//    if (user2 != null)
//    {
//        var review4 = new Review
//        {
//            StylesScore = 2.5,
//            StoryScore = 2,
//            GrammarScore = 2.4,
//            Created = DateTime.Now,
//            CharactersScore = 2.9,
//            Title = "Mind-Blowing Action",
//            Content = "Solo Leveling is an absolute thrill ride! The art is top-notch, with incredibly detailed action scenes that leave you in awe. The story is gripping, filled with intense moments and unexpected twists. The characters are well-developed and have unique abilities. If you're a fan of action-packed manga, Solo Leveling is a must-read!."
//        };
//        review4.UserModels = new List<UserModel>() { user2 };
//        review4.MangaModels = new List<MangaModel>() { manga2 };
//        var review5 = new Review
//        {
//            StylesScore = 1.5,
//            StoryScore = 1,
//            GrammarScore = 1.4,
//            Created = DateTime.Now,
//            CharactersScore = 4.9,
//            Title = "SailorD",
//            Content = "Sailor Miss!."
//        };
//        review5.UserModels = new List<UserModel>() { user2 };
//        review5.MangaModels = new List<MangaModel>() { manga4 };
//        context.Reviews.Add(review5);
//        context.Reviews.Add(review4);
//        await context.SaveChangesAsync();
//    }
//    else
//    {
//        throw new Exception("Test user not found.");
//    }
//    var user3 = new UserModel { UserName = "TestUser3", Email = "TestUser3@hotmail.com", Allias = "Fishmuppet", };
//    var result5 = await userManager.CreateAsync(user3, "Rojhat123!");
//    if (result5.Succeeded)
//    {
//        var resultRole = await userManager.AddToRoleAsync(user3, "Updater");
//    }
//    if (user3 != null)
//    {
//        var review4 = new Review
//        {
//            StylesScore = 4.5,
//            StoryScore = 5,
//            GrammarScore = 4.4,
//            Created = DateTime.Now,
//            CharactersScore = 4.9,
//            Title = "Test Action",
//            Content = "Solo Monkey The story is gripping,  The characters are well-developed and have unique abilities. If you're a fan of action-packed manga, Solo Leveling is a must-read!."
//        };
//        review4.UserModels = new List<UserModel>() { user3 };
//        review4.MangaModels = new List<MangaModel>() { manga2 };
//        var review5 = new Review
//        {
//            StylesScore = 4.5,
//            StoryScore = 3,
//            GrammarScore = 2.4,
//            Created = DateTime.Now,
//            CharactersScore = 4.9,
//            Title = "MySalor",
//            Content = "Sailor Miss!"
//        };
//        review5.UserModels = new List<UserModel>() { user3 };
//        review5.MangaModels = new List<MangaModel>() { manga4 };
//        context.Reviews.Add(review4);
//        context.Reviews.Add(review5);
//        await context.SaveChangesAsync();
//    }
//    else
//    {
//        throw new Exception("Test user not found.");
//    }
//}