using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService.DTO
{
    public class CreatorGenresAndTagsDto
    {
        public int CreatorId { get; set; }

        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}