﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class UserModel : IdentityUser
    {
        public string? Allias { get; set; }

        [Required]
        public string ForumName { get; set; }

        public string? userPhotoPath { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be more than 500 characters.")]
        public string? Description { get; set; }

        public bool IsShadowBanned { get; set; }
        public bool IsDeleted { get; set; }

        //Social media whatever the user wants.
        public string? Twitter { get; set; }

        public DateTime? CreatedAcc { get; set; }
        public bool IsChecked { get; set; }
        public virtual ICollection<BlogModel>? UserBlogModel { get; set; }

        //navigation
        [ForeignKey("GroupScanlatingModel")]
        public int? groupScanlationID { get; set; }

        public virtual ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        //Hide no need to show.
        public DateTime? UserActivityTimer { get; set; }

        public virtual ICollection<MangaModel>? MangaModels { get; set; }
        public virtual ICollection<ReadingList>? ReadingList { get; set; }
        public virtual ICollection<CompletedBookList>? CompletedList { get; set; }
        public virtual ICollection<DroppedBookList>? DroppedList { get; set; }
        public virtual ICollection<WishBookList>? WishList { get; set; }
        public virtual ICollection<FavoritBookList>? FavoritList { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public string? postModelID { get; set; }

        public virtual ICollection<PostModel>? PostModel { get; set; }

        //hidden
        public virtual ICollection<UserBlock>? BlockedUsers { get; set; }

        //Hidden
        public virtual ICollection<UserBlock>? BlockedByUsers { get; set; }

        public virtual ICollection<PrivateConversation>? ConversationsAsUserOne { get; set; }

        public virtual ICollection<PrivateConversation>? ConversationsAsUserTwo { get; set; }
        public virtual UserSettings UserSettings { get; set; }
    }
}