using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcarrollonline.react.backend.Models
{
    public class ForumThread
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public bool Locked { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int PostNumber { get; set; }

        // Will be null for no parent (root)
        public int? ParentId { get; set; }

        // Will be null for root (no parent)
        public int? RootId { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey(nameof(AuthorId))]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int ForumId { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [ForeignKey(nameof(ForumId))]
        public virtual Forum Forum { get; set; }
    }
}
