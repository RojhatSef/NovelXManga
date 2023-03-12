﻿using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class VoiceActorModel
    {
        [Key]
        public int VoiceActorId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhotoPath { get; set; }
        public string? Gender { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }
        public string? Twitter { get; set; }
        public string? reddit { get; set; }
        public string? BirthPlace { get; set; }
        public string? CharacterName { get; set; }
        public string? Biography { get; set; }
        public string? NameInNative { get; set; }
        public string? WikiPedia { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public int? AmountOfWork { get; set; }
        public string? WorkingAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? VoiceBorn { get; set; }
        public DateTime? VoiceDeath { get; set; }
        public string? Contact { get; set; }
        public bool IsChecked { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }
    }
}