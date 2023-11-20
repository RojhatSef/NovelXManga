using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaAccessService.DTO.UserProfileMangaDTO
{
    public class UserMangaImageDto
    {
        public int MangaID { get; set; }
        public string MangaName { get; set; }
        public string? PhotoPath { get; set; }
    }
}