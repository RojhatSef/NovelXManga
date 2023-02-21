using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}