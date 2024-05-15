using JCarrollOnlineV2.Entities;
using System.ComponentModel.DataAnnotations;

namespace jcarrollonline.react.backend.Models
{
    public class ForumPost
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = string.Empty;

        public bool Locked { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int PostNumber { get; set; }

        public int? ParentId { get; set; }

        public int? RootId { get; set; }

        //[Required]
        //public virtual ApplicationUser Author { get; set; }

        [Required]
        public virtual Forum Forum { get; set; } = new Forum();
    }
}
