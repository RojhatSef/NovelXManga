﻿using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class GroupScanlationModelView
    {

        public int? GroupScanlatingID { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string? PhotoPath { get; set; }
        public string website { get; set; }
        public ICollection<ChapterModel>? chapterModels { get; set; }
        public int? MasterID { get; set; }
        public ICollection<MangaModel>? MangaModels { get; set; }
        public string? userID { get; set; }
        public ICollection<UserModel>? userModels { get; set; }
    }
}
