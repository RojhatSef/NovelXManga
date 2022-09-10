using System.ComponentModel;

namespace MangaModelService
{
    public enum GenresModel
    {
        None,
        Action,
        Adult,
        Adventure,
        Comedy,
        Drama,
        Ecchi,
        Fantasy,
        [Description("Gender Bender")]
        GenderBender,
        Harem,
        Historical,
        Horror,
        Josei,
        [Description("Martial Arts")]
        MartialArts,
        Mature,
        Mecha,
        Mystery,
        Psychological,
        Romance,
        [Description("School Life")]
        SchoolLife,
        [Description("Sci-fi")]
        Scifi,
        Seinen,
        Shoujo,
        [Description("Shoujo Ai")]
        ShoujoAi,
        Shounen,
        [Description("Shounen Ai")]
        ShounenAi,
        [Description("Slice Of Life")]
        SliceOfLife,
        Smut,
        Sports,
        Supernatural,
        Tragedy,
        Wuxia,
        Xianxia,
        Xuanhuan,
        Yaoi,
        Yuri,
    }
}
