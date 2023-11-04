using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class PrivateConversation
    {
        [Key]
        public int Id { get; set; }

        public string UserOneId { get; set; }
        public string UserTwoId { get; set; }

        [ForeignKey("UserOneId")]
        public virtual UserModel UserOne { get; set; }

        [ForeignKey("UserTwoId")]
        public virtual UserModel UserTwo { get; set; }

        public virtual ICollection<PrivateMessage> PrivateMessages { get; set; }
    }
}