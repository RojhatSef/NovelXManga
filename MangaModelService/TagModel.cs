using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string? TagName { get; set; }
        public string? TagDescription { get; set; }
        public int? TagHeavy { get; set; }

        public int? MangaID { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }

    }
}
#region 1000 Tags
//    public enum ListOfTagModels
//    {

//        None,
//        [Description("21st century")]
//        TwentyFirststCentur,
//        [Description("4-koma/Yonkoma")]
//        FourkomaYonkoma,
//        [Description("Abandoned Child/ren")]
//        AbandonedChildren,
//        [Description("Absent Parent/s")]
//        AbsentParents,
//        [Description("Accident/s")]
//        Accidents,

//        Acting,
//        [Description("Actor/s")]
//        Actors,
//        [Description("Adapted to Anime")]
//        AdaptedToAnime,
//        [Description("Adapted  to Drama CD")]
//        AdaptedToDramaCD,
//        [Description("Adapted to Game")]
//        AdaptedToGame,
//        [Description("Adapted to Live Action")]
//        AdaptedToLiveAction,
//        [Description("Adapted to Manga")]
//        AdaptedToManga,
//        [Description("Adapted to Movie")]
//        AdaptedToMovie,
//        [Description("Adopted Protagonist")]
//        AdoptedProtagonist,
//        Adoption,
//        [Description("Affectionate Seme")]
//        AffectionateSeme,
//        [Description("Age Gap,")]
//        AgeGap,
//        [Description("Alien/s")]
//        Aliens,
//        [Description("All-Boys School")]
//        AllBoysSchool,
//        [Description("All-Girls School")]
//        AllGirlsSchool,
//        [Description("Alternate Reality")]
//        AlternateReality,
//        [Description("Ambitious Goal/s")]
//        AmbitiousGoals,

//        America,

//        Amnesia,
//        [Description("Anal Intercourse")]
//        AnalIntercourse,
//        [Description("Androgynous Male Lead")]
//        AndrogynousMaleLead,
//        [Description("Angel/s")]
//        Angels,
//        Angst,
//        Anilingus,
//        [Description("Animal Characteristics")]
//        AnimalCharacteristics,
//        [Description("Animal Sidekick")]
//        AnimalSidekick,
//        [Description("Animal Transformation")]
//        AnimalTransformation,
//        [Description("Antihero / Antiheroine")]
//        AntiheroAntiheroine,
//        Apocalypse,
//        [Description("Appearance Changes")]
//        AppearanceChanges,
//        [Description("Appearance Different from Actual Age")]
//        AppearanceDifferentfromActualAge,
//        [Description("Appearance Different from Personality")]
//        AppearanceDifferentfromPersonality,
//        [Description("Aristocrat/s")]
//        Aristocrats,
//        [Description("Armed Combat")]
//        ArmedCombat,
//        [Description("Arranged Marriage")]
//        ArrangedMarriage,
//        [Description("Arrogant Male Lead")]
//        ArrogantMaleLead,
//        [Description("Artist/s")]
//        Artists,
//        [Description("Artificial Intelligence")]
//        ArtificialIntelligence,
//        [Description("Assassin/s")]
//        Assassins,
//        [Description("Attempted Murder")]
//        AttemptedMurder,
//        [Description("Attempted Rape")]
//        AttemptedRape,
//        [Description("Attempted Suicide")]
//        AttemptedSuicide,
//        [Description("Atypical Art Style")]
//        AtypicalArtStyle,
//        [Description("Award-Nominated Work")]
//        AwardNominatedWork,
//        [Description("Award-Winning Work Baseball")]
//        AwardWinningWorkBaseball,
//        [Description("Based on a Novel")]
//        BasedOnANovel,
//        [Description("Based on a Web Novel")]
//        BasedOnAWebNovel,
//        [Description("Bathroom Intercourse")]
//        BathroomIntercourse,

//        BDSM,
//        [Description("Beautiful Female Lead")]
//        BeautifulFemaleLead,
//        [Description("Beauty Mark")]
//        BeautyMark,
//        [Description("Bespectacled Protagonist")]
//        BespectacledProtagonist,
//        [Description("Best Friends")]
//        BestFriends,

//        Betrayal,
//        [Description("Bickering Love")]
//        BickeringLove,
//        [Description("Big Breasts")]
//        BigBreasts,
//        [Description("Bisexual Character/s")]
//        BisexualCharacters,
//        Bishounen,
//        Blackmail,
//        [Description("Blood and Gore")]
//        BloodAndGore,
//        [Description("Blushing Male Lead")]
//        BlushingMaleLead,
//        [Description("Body Swap/s")]
//        BodySwaps,
//        [Description("Bodyguard/s")]
//        Bodyguards,
//        Bondage,
//        [Description("Borderline H")]
//        BorderlineH,
//        [Description("Boss-Subordinate Relationship")]
//        BossSubordinateRelationship,
//        [Description("Breakup/s")]
//        Breakups,

//        Bromance,
//        [Description("Brother and Sister")]
//        BrotherAndSister,
//        [Description("Brother Complex")]
//        BrotherComplex,
//        [Description("Brother/s")]
//        Brothers,
//        Bullying,
//        [Description("Businessman / Businesswoman")]
//        BusinessmanBusinesswoman,
//        [Description("Butler/s")]
//        Butlers,
//        [Description("Calm Protagonist")]
//        CalmProtagonist,
//        [Description("Cat/s")]
//        Cats,
//        [Description("Caught in the Act")]
//        CaughtInTheAct,
//        [Description("Celebrity/ies")]
//        Celebrityies,
//        [Description("Changed by Love")]
//        ChangedByLove,
//        [Description("Character Growth")]
//        CharacterGrowth,
//        [Description("Character Who Bullies the One They Love")]
//        CharacterWhoBulliesTheOneTheyLove,
//        [Description("Chasing After Love Interest")]
//        ChasingAfterLoveInterest,
//        [Description("Cheating/Infidelity")]
//        CheatingInfidelity,
//        [Description("Child Abuse")]
//        ChildAbuse,
//        [Description("Child/ren")]
//        Children,

//        Childcare,
//        [Description("ChildhoodFriend/s")]
//        ChildhoodFriends,
//        [Description("Childhood Love")]
//        ChildhoodLove,
//        [Description("ChildhoodPromise")]
//        ChildhoodPromise,
//        [Description("Childhood Trauma")]
//        ChildhoodTrauma,
//        [Description("Classmate/s Clever Protagonist")]
//        ClassmatesCleverProtagonist,
//        [Description("Club/s")]
//        Clubs,
//        [Description("Clumsy Protagonist")]
//        ClumsyProtagonist,

//        Cohabitation,
//        [Description("Cold Male Lead")]
//        ColdMaleLead,
//        [Description("Collection of Inter-Linked Stories")]
//        CollectionOfInterLinkedStories,
//        [Description("Collection of Stories")]
//        CollectionOfStories,
//        [Description("Comedic Undertone")]
//        ComedicUndertone,
//        [Description("Comic Artist/s")]
//        ComicArtists,
//        [Description("Coming of Age")]
//        ComingOfAge,
//        [Description("Competition/s")]
//        Competitions,
//        [Description("Conspiracy/ies")]
//        Conspiracyies,
//        [Description("Contract/s")]
//        Contracts,

//        Cooking,
//        [Description("Cool Male Lead")]
//        CoolMaleLead,
//        Cyberpunk,
//        Cosplay,

//        Countryside,
//        [Description("Couple Growth")]
//        CoupleGrowth,
//        [Description("Cousin/s")]
//        Cousins,
//        [Description("Coworker Relationships")]
//        CoworkerRelationships,
//        [Description("Coworker/s")]
//        Coworkers,
//        [Description("Crime/s")]
//        Crimes,
//        [Description("Cross-dressing")]
//        CrossDressing,

//        Cunnilingus,
//        [Description("Curse/s")]
//        Curses,
//        [Description("Dark Ambience")]
//        DarkAmbience,
//        [Description("Dead Family Member/s")]
//        DeadFamilyMembers,
//        [Description("Dead Friend/s")]
//        DeadFriends,
//        [Description("Dead Lover/s")]
//        DeadLovers,
//        [Description("Dead Parent/s")]
//        DeadParents,

//        Death,
//        [Description("Death of Loved One/s")]
//        DeathOfLovedOnes,
//        [Description("Debt/s")]
//        Debts,

//        Deception,
//        [Description("Delinquent/s")]
//        Delinquents,
//        [Description("Demon Lord/King")]
//        DemonLordKing,
//        [Description("Demon/s")]
//        Demons,
//        [Description("Dense When It Comes To Love")]
//        DenseWhenItComesToLove,

//        Depression,
//        [Description("Detective/s")]
//        Detectives,
//        [Description("Determined Protagonist")]
//        DeterminedProtagonist,
//        [Description("Devil/s")]
//        Devils,
//        [Description("Disability/ies")]
//        Disabilityies,

//        Discrimination,
//        [Description("Doctor/s")]
//        Doctors,
//        [Description("Dog/s")]
//        Dogs,
//        Dungeon,
//        [Description("Dormitory/ies")]
//        Dormitoryies,
//        [Description("Double / Multiple Penetration")]
//        DoubleMultiplePenetration,
//        [Description("Dragon/s")]
//        Dragons,
//        [Description("Dream/s")]
//        Dreams,
//        [Description("Drug/s")]
//        Drugs,
//        [Description("Drunken Intercourse")]
//        DrunkenIntercourse,
//        Dystopia,
//        [Description("Dubious Consent")]
//        DubiousConsent,
//        [Description("Elf/ves")]
//        Elves,
//        [Description("Emotionally Strong Female Lead")]
//        EmotionallyStrongFemaleLead,
//        [Description("Emotionally Weak Female Lead")]
//        EmotionallyWeakFemaleLead,
//        [Description("Emotionally Strong Male Lead")]
//        EmotionallyStrongMaleLead,
//        [Description("Emotionally Weak Male Lead")]
//        EmotionallyWeakMaleLead,
//        [Description("Enemies Become Friends")]
//        EnemiesBecomeFriends,

//        Episodic,

//        ESP,
//        [Description("European Ambience")]
//        EuropeanAmbience,

//        Exhibitionism,

//        Exorcism,
//        [Description("Experiment/s")]
//        Experiments,
//        [Description("Expressionless Protagonist")]
//        ExpressionlessProtagonist,
//        [Description("Extended Flashbacks")]
//        ExtendedFlashbacks,

//        Eyepatch,

//        Family,
//        [Description("Family Drama")]
//        FamilyDrama,
//        [Description("Fantasy World")]
//        FantasyWorld,
//        [Description("Fast Romance")]
//        FastRomance,
//        [Description("Father and Daughter")]
//        FatherAndDaughter,

//        Fellatio,
//        [Description("Female Demographic with Male Lead")]
//        FemaleDemographicWithMaleLead,
//        [Description("Female Dominance")]
//        FemaleDominance,
//        [Description("Female Fighter/s")]
//        FemaleFighters,
//        [Description("Female Lead Falls in Love First")]
//        FemaleLeadFallsInLoveFirst,
//        [Description("Fetish/es")]
//        Fetishes,
//        [Description("Finding Love Again")]
//        FindingLoveAgain,
//        [Description("First Kiss")]
//        FirstKiss,
//        [Description("First Love")]
//        FirstLove,
//        [Description("First-Time Intercourse")]
//        FirstTimeIntercourse,

//        Flashbacks,

//        Food,
//        [Description("Forbidden Love")]
//        ForbiddenLove,
//        [Description("Foreigner/s")]
//        Foreigners,
//        [Description("Friends Become Enemies")]
//        FriendsBecomeEnemies,
//        [Description("Friends Become Lovers")]
//        FriendsBecomeLovers,
//        [Description("Friends Grow Distant")]
//        FriendsGrowDistant,

//        Friendship,
//        [Description("Full Color")]
//        FullColor,

//        Futanari,

//        Future,
//        [Description("Game Elements")]
//        GameElements,
//        GameLit,
//        [Description("Game/s")]
//        Games,
//        [Description("Gang Rape")]
//        GangRape,
//        [Description("Gang/s")]
//        Gangs,
//        [Description("Genetically Engineered")]
//        GeneticallyEngineered,
//        [Description("Genius/es")]
//        Geniuses,
//        [Description("Ghost/s")]
//        Ghosts,
//        [Description("Glasses-Wearing Female Lead")]
//        GlassesWearingFemaleLead,
//        [Description("Glasses-Wearing Male Lead")]
//        GlassesWearingMaleLead,
//        [Description("Glasses-Wearing Uke God/s")]
//        GlassesWearingUkeGods,
//        [Description("Goddess/es")]
//        Goddesses,

//        Groping,
//        [Description("Group Intercourse")]
//        GroupIntercourse,
//        [Description("Guardian Relationship")]
//        GuardianRelationship,

//        Guilt,
//        [Description("Gun/s")]
//        Guns,

//        Handjob,
//        [Description("Handsome Male Lead")]
//        HandsomeMaleLead,
//        [Description("Handsome Female Lead")]
//        HandsomeFemaleLead,
//        [Description("Hard-Working Protagonist")]
//        HardWorkingProtagonist,
//        [Description("Height Difference")]
//        HeightDifference,
//        [Description("Hero/es")]
//        Heroes,
//        [Description("Hidden Past")]
//        HiddenPast,
//        [Description("Hidden Power/s")]
//        HiddenPowers,
//        [Description("High School")]
//        HighSchool,
//        [Description("High School Student/s")]
//        HighSchoolStudents,

//        Homophobia,
//        [Description("Honest Protagonist")]
//        HonestProtagonist,
//        [Description("Host/Hostess")]
//        HostHostess,
//        [Description("Hot-Blooded Male Lead")]
//        HotBloodedMaleLead,
//        [Description("Human Experiment/s")]
//        HumanExperiments,
//        [Description("Human-Nonhuman Relationship")]
//        HumanNonhumanRelationship,
//        [Description("Hunted Protagonist")]
//        HuntedProtagonist,
//        [Description("Idol/s,")]
//        Idols,

//        Immortality,
//        [Description("Important Non-Romantic Relationship/s")]
//        ImportantNonRomanticRelationships,

//        Incest,
//        [Description("Independent Female Lead")]
//        IndependentFemaleLead,
//        [Description("Inexperienced in Love")]
//        InexperiencedInLove,
//        [Description("Inferiority Complex")]
//        InferiorityComplex,
//        [Description("Injury/ies")]
//        Injuryies,
//        [Description("Inner Demon/s")]
//        InnerDemons,
//        [Description("Innocent Female Lead")]
//        InnocentFemaleLead,
//        [Description("Innocent Male Lead")]
//        InnocentMaleLead,

//        Insecurity,

//        Isekai,
//        [Description("Island/s")]
//        Islands,

//        Japan,

//        Jealousy,
//        [Description("Kidnapping/s")]
//        Kidnappings,
//        [Description("Kind Female Lead")]
//        KindFemaleLead,
//        [Description("Kind Male Lead")]
//        KindMaleLead,
//        [Description("Knight/s")]
//        Knights,

//        Korea,
//        [Description("LGBT Character in Non-Yaoi/Yuri Manga")]
//        LGBTCharacterInNonYaoiYuriManga,
//        [Description("LGBT Parent/s")]
//        LGBTParents,
//        [Description("LGBT Scenes")]
//        LGBTScenes,
//        [Description("Lifestyle Change")]
//        LifestyleChange,
//        [Description("Live-in Lover")]
//        LiveInLover,
//        Loneliness,
//        [Description("Loner Protagonist")]
//        LonerProtagonist,
//        [Description("Long-Haired Male Lead")]
//        LongHairedMaleLead,
//        [Description("Love at First Sight")]
//        LoveAtFirstSight,
//        [Description("Love Confession/s")]
//        LoveConfessions,
//        [Description("Love Interests Who Don't Get Along")]
//        LoveInterestsWhoDontGetAlong,
//        [Description("Love Polygon")]
//        LovePolygon,
//        [Description("Love Triangle/s")]
//        LoveTriangles,
//        [Description("Love-Hate Relationship")]
//        LoveHateRelationship,

//        Lust,

//        Mafia,

//        Magic,
//        [Description("Magic School")]
//        MagicSchool,
//        [Description("Magical Creature/s")]
//        MagicalCreatures,
//        [Description("Magical Girl/s")]
//        MagicalGirls,
//        [Description("Maid/s")]
//        Maids,
//        [Description("Male Demographic with Female Lead")]
//        MaleDemographicWithFemaleLead,
//        [Description("Male Lead Falls in Love First")]
//        MaleLeadFallsInLoveFirst,
//        [Description("Male Protagonist")]
//        MaleProtagonist,

//        Manipulation,
//        [Description("Manly Gay Couple")]
//        ManlyGayCouple,

//        Marriage,
//        [Description("Marriage Proposal")]
//        MarriageProposal,
//        [Description("Married Couple")]
//        MarriedCouple,
//        [Description("Masculine Uke")]
//        MasculineUke,
//        [Description("Masochist/s")]
//        Masochists,
//        [Description("Master-Pet Relationship")]
//        MasterPetRelationship,
//        [Description("Master-Servant Relationship")]
//        MasterServantRelationship,

//        Masturbation,
//        [Description("Mature Child")]
//        MatureChild,
//        [Description("Middle School")]
//        MiddleSchool,

//        Military,
//        [Description("Mind Break")]
//        MindBreak,
//        [Description("Misunderstanding/s")]
//        Misunderstandings,
//        [Description("Misunderstood Protagonist")]
//        MisunderstoodProtagonist,
//        [Description("Model/s")]
//        Models,

//        Modeling,
//        [Description("Monster/s")]
//        Monsters,
//        [Description("Multiple Couples")]
//        MultipleCouples,
//        [Description("Multiple Protagonists")]
//        MultipleProtagonists,
//        [Description("Murder/s")]
//        Murders,

//        Music,
//        [Description("Music Band/s")]
//        MusicBands,
//        Mythos,
//        [Description("Naive Female Lead")]
//        NaiveFemaleLead,
//        [Description("Naive Male Lead")]
//        NaiveMaleLead,
//        [Description("Near-Death Experience")]
//        NearDeathExperience,
//        [Description("Neighbor/s")]
//        Neighbors,

//        Netorare,
//        [Description("Ninja/s")]
//        Ninjas,

//        Nobility,

//        Nudity,
//        [Description("Obsessive Love")]
//        ObsessiveLove,
//        [Description("Odd Situation/s")]
//        OddSituations,
//        [Description("Office Love")]
//        OfficeLove,
//        [Description("Office Worker/s")]
//        OfficeWorkers,
//        [Description("Older Brother")]
//        OlderBrother,
//        [Description("Older Female Lead")]
//        OlderFemaleLead,
//        [Description("Older Female Younger Male")]
//        OlderFemaleYoungerMale,
//        [Description("Older Male Younger Female")]
//        OlderMaleYoungerFemale,
//        [Description("Older Seme Younger Uke")]
//        OlderSemeYoungerUke,
//        [Description("Older Sister")]
//        OlderSister,
//        [Description("Older Uke Younger Seme")]
//        OlderUkeYoungerSeme,

//        Omegaverse,
//        [Description("Opposites Attract")]
//        OppositesAttract,
//        [Description("Orphan/s")]
//        Orphans,

//        Otaku,
//        [Description("Outdoor Intercourse")]
//        OutdoorIntercourse,

//        Paizuri,

//        Panchira,
//        [Description("Parallel Universe")]
//        ParallelUniverse,
//        [Description("Parental Abandonment")]
//        ParentalAbandonment,

//        Parody,
//        [Description("Part-Time Job")]
//        PartTimeJob,
//        [Description("Partial Nudity")]
//        PartialNudity,
//        [Description("Past Plays a Big Role")]
//        PastPlaysABigRole,
//        [Description("Persistent Seme")]
//        PersistentSeme,
//        [Description("Personality Change/s")]
//        PersonalityChanges,
//        [Description("Pervert/s")]
//        Perverts,
//        [Description("Perverted Female Lead")]
//        PervertedFemaleLead,
//        [Description("Perverted Male Lead")]
//        PervertedMaleLead,

//        Photography,
//        [Description("Play or Die Situation")]
//        PlayOrDieSituation,
//        [Description("Playboy/s")]
//        Playboys,
//        [Description("Police")]
//        Police,

//        Politics,
//        [Description("Politics Involving Royalty")]
//        PoliticsInvolvingRoyalty,
//        [Description("Popular Female Lead")]
//        PopularFemaleLead,
//        [Description("Popular Male Lead")]
//        PopularMaleLead,
//        [Description("Possessive Lover/s")]
//        PossessiveLovers,
//        [Description("Post-Secondary School")]
//        PostSecondarySchool,
//        [Description("Post-Secondary Student/s")]
//        PostSecondaryStudents,

//        [Description("Post Apocalyptic")]
//        PostApocalyptic,

//        Pregnancy,
//        [Description("Pretend Lovers")]
//        PretendLovers,
//        [Description("Priest/s")]
//        Priests,
//        [Description("Prince/s")]
//        Princes,
//        [Description("Princess/es")]
//        Princesses,

//        Prodigy,
//        [Description("Prostitute/s")]
//        Prostitutes,

//        Prostitution,
//        [Description("Protagonist Strong from the Start")]
//        ProtagonistStrongFromTheStart,
//        [Description("Psychopath/s")]
//        Psychopaths,
//        [Description("Public Sex")]
//        PublicSex,
//        [Description("Quirky Character/s")]
//        QuirkyCharacters,

//        Rape,
//        [Description("Rape Victim/s")]
//        RapeVictims,

//        Regret,
//        [Description("Reader Interactive")]
//        ReaderInteractive,
//        [Description("Reincarnated in Another World")]
//        ReincarnatedInAnotherWorld,

//        Reincarnation,

//        Rescue,

//        Restaurant,
//        [Description("Reunion/s")]
//        Reunions,

//        Revenge,
//        [Description("Reverse Harem")]
//        ReverseHarem,
//        [Description("Rich Family")]
//        RichFamily,
//        [Description("Rich Female Lead")]
//        RichFemaleLead,
//        [Description("Rich Kid/s")]
//        RichKids,
//        [Description("Rich Male Lead")]
//        RichMaleLead,
//        [Description("Rich Protagonist")]
//        RichProtagonist,

//        Rivalry,
//        [Description("Robot/s")]
//        Robots,
//        [Description("Romantic Subplot")]
//        RomanticSubplot,

//        Roommates,

//        Royalty,
//        [Description("Runaway/s")]
//        Runaways,
//        [Description("Rushed Ending / Axed")]
//        RushedEndingAxed,
//        [Description("Sadist/s")]
//        Sadists,

//        Sadomasochism,

//        Salaryman,
//        [Description("Samurai")]
//        Samurai,
//        [Description("Scar/s")]
//        Scars,
//        [Description("School Boy/s")]
//        SchoolBoys,
//        [Description("School Girl/s")]
//        SchoolGirls,
//        [Description("School Intercourse")]
//        SchoolIntercourse,
//        [Description("Scientist/s")]
//        Scientists,
//        [Description("Secret Identity")]
//        SecretIdentity,
//        [Description("Secret Organization/s")]
//        SecretOrganizations,
//        [Description("Secret Relationship")]
//        SecretRelationship,
//        [Description("Secret/s")]
//        Secrets,
//        [Description("Seeing Things Other Humans Can't")]
//        SeeingThingsOtherHumansCant,
//        [Description("Senpai-Kouhai Relationship")]
//        SenpaiKouhaiRelationship,
//        [Description("Serial Killer/s")]
//        SerialKillers,
//        [Description("Serious/Studious Character/s")]
//        SeriousStudiousCharacters,
//        [Description("Sex Addict/s")]
//        SexAddicts,
//        [Description("Sex Friends Become Lovers")]
//        SexFriendsBecomeLovers,
//        [Description("Sex Toy/s")]
//        SexToys,
//        [Description("Sexual Abuse")]
//        SexualAbuse,
//        [Description("Sexual Assault")]
//        SexualAssault,
//        [Description("Sexual Innuendo")]
//        SexualInnuendo,
//        [Description("Shameless Female Lead")]
//        ShamelessFemaleLead,

//        Shinigami,
//        [Description("Short Chapter/s")]
//        ShortChapters,
//        [Description("Short Female Lead")]
//        ShortFemaleLead,
//        [Description("Short Male Lead")]
//        ShortMaleLead,

//        Showbiz,
//        [Description("Shy Female Lead")]
//        ShyFemaleLead,
//        [Description("Shy Protagonist")]
//        ShyProtagonist,
//        [Description("Sibling/s")]
//        Siblings,
//        [Description("Single Parent")]
//        SingleParent,
//        [Description("Sister Complex")]
//        SisterComplex,
//        [Description("Sister/s")]
//        Sisters,
//        [Description("Slave/s")]
//        Slaves,
//        [Description("Slow Romance")]
//        SlowRomance,
//        [Description("Small Breasts")]
//        SmallBreasts,
//        [Description("Smart Female Lead")]
//        SmartFemaleLead,
//        [Description("Smart Male Lead")]
//        SmartMaleLead,

//        Smoking,
//        [Description("Social Hierarchy")]
//        SocialHierarchy,
//        [Description("Social Outcast/s")]
//        SocialOutcasts,
//        [Description("Special Ability/ies")]
//        SpecialAbilityies,
//        [Description("Spirit/s")]
//        Spirits,
//        [Description("Split Personality")]
//        SplitPersonality,
//        [Description("Stalker/s")]
//        Stalkers,
//        [Description("Stepsibling Love")]
//        StepsiblingLove,

//        Stepsiblings,
//        [Description("Stolen Kiss")]
//        StolenKiss,
//        [Description("Straight Seme")]
//        StraightSeme,
//        [Description("Straight Uke")]
//        StraightUke,
//        [Description("Strategic Minds")]
//        StrategicMinds,
//        [Description("Strong Female Lead")]
//        StrongFemaleLead,
//        [Description("Strong Male Lead")]
//        StrongMaleLead,
//        [Description("Strong-Willed Female Lead")]
//        StrongWilledFemaleLead,
//        [Description("Strong-Willed Male Lead")]
//        StrongWilledMaleLead,
//        [Description("Student Council")]
//        StudentCouncil,
//        [Description("Student-Student Relationship")]
//        StudentStudentRelationship,
//        [Description("Student-Teacher Relationship")]
//        StudentTeacherRelationship,
//        [Description("Student/s")]
//        Students,
//        [Description("Subtle Romance")]
//        SubtleRomance,
//        [Description("Sudden Appearance")]
//        SuddenAppearance,
//        [Description("Suicide/s")]
//        Suicides,
//        [Description("Super Heroes")]
//        SuperHowers,
//        [Description("Super Powers")]
//        SuperPowers,


//        Survival,
//        [Description("Swimsuit/s")]
//        Swimsuits,
//        [Description("Sword and Sorcery")]
//        SwordAndSorcery,

//        Swordplay,

//        Swordsman,

//        Swordswoman,
//        [Description("Talented Female Lead")]
//        TalentedFemaleLead,
//        [Description("Talented Male Lead")]
//        TalentedMaleLead,
//        [Description("Tall Female Lead")]
//        TallFemaleLead,
//        [Description("Tall Male Lead")]
//        TallMaleLead,
//        [Description("Tattoo/s")]
//        Tattoos,
//        [Description("Teacher/s")]
//        Teachers,

//        Teamwork,

//        Threesome,
//        [Description("Time Skip")]
//        TimeSkip,
//        [Description("Time Loop")]
//        TimeLoop,
//        [Description("Time Travel")]
//        TimeTravel,
//        [Description("Tomboy/s")]
//        Tomboys,

//        Torture,
//        [Description("Tournament/s")]
//        Tournaments,
//        [Description("Tragic Past")]
//        TragicPast,

//        Training,
//        [Description("Transfer Student/s")]
//        TransferStudents,
//        [Description("Transformation/s")]
//        Transformations,
//        [Description("Transgender Character/s")]
//        TransgenderCharacters,
//        [Description("Transported to Another World")]
//        TransportedToAnotherWorld,

//        Trap,
//        [Description("Traumatic Past")]
//        TraumaticPast,

//        Travel,

//        Tsundere,
//        [Description("Tsundere Female Lead")]
//        TsundereFemaleLead,
//        [Description("Tutor/s")]
//        Tutors,
//        [Description("Twin/s")]
//        Twins,
//        [Description("Undergarment/s")]
//        Undergarments,
//        [Description("Unexpected Feelings")]
//        UnexpectedFeelings,
//        [Description("Unexpressed Feeling/s")]
//        UnexpressedFeelings,
//        [Description("University Student/s")]
//        UniversityStudents,
//        [Description("Unorthodox Female Love Interest")]
//        UnorthodoxFemaleLoveInterest,
//        [Description("Unorthodox Male Love Interest")]
//        UnorthodoxMaleLoveInterest,
//        [Description("Unpopular Protagonist")]
//        UnpopularProtagonist,
//        [Description("Unrealistic Fighting")]
//        UnrealisticFighting,
//        [Description("Unrequited Love")]
//        UnrequitedLove,
//        [Description("Unrequited Love Becomes Requited")]
//        UnrequitedLoveBecomesRequited,
//        [Description("Urination")]
//        Urination,
//        [Description("Vampire/s")]
//        Vampires,

//        Violence,
//        [Description("Virgin/s")]
//        Virgins,
//        [Description("Villainous lead")]
//        Villainouslead,
//        [Description("Virtual Reality")]
//        VirtualReality,
//        Voyeurism,
//        [Description("War/s")]
//        Wars,
//        [Description("Weak Male Lead")]
//        WeakMaleLead,
//        [Description("Weak to Strong")]
//        WeakToStrong,
//        [Description("Web Comic")]
//        WebComic,

//        Webtoon,
//        [Description("Werewolf/ves")]
//        Werewolfves,
//        [Description("Wish/es")]
//        Wishes,
//        [Description("Witch/es")]
//        Witches,
//        [Description("Workplace Romance")]
//        WorkplaceRomance,
//        [Description("World Travel")]
//        WorldTravel,
//        [Description("Writer/s")]
//        Writers,

//        Yandere,

//        Youkai,
//        [Description("Young Female Lead")]
//        YoungFemaleLead,
//        [Description("Young Male Lead")]
//        YoungMaleLead,
//        [Description("Younger Brother")]
//        YoungerBrother,
//        [Description("Younger Sister")]
//        YoungerSister,
//        [Description("Zombie/s")]
//        Zombies,


//    }
#endregion
