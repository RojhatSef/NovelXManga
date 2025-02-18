﻿using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class PostModel
    {
        [Key]
        public int? PostId { get; set; }

        public string? Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(750)]
        public string postComment { set; get; }

        public double? score { get; set; }
        public DateTime CommentPostedTime { get; set; }
        public DateTime? NextAllowedPostTime { get; set; }
        public virtual ICollection<BlogModel>? BlogModels { get; set; }
        public bool IsChecked { get; set; }
        public string? UserID { get; set; }

        public virtual ICollection<UserModel>? UserModel { get; set; }
        public int? ParentPostId { get; set; }
        public virtual PostModel? ParentPost { get; set; }
        public virtual ICollection<PostModel>? Replies { get; set; }
    }
}