using System.ComponentModel.DataAnnotations;

namespace jcarrollonline.react.backend.Models.ViewModels
{
    public class PostDTO
    {
        [DataType(DataType.Text)]
        [StringLength(256)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public bool Locked { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } //           :null => false

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } //          :null => false

        public int PostNumber { get; set; }

        public int? ParentId { get; set; }

        public int? RootId { get; set; }

        public string Author { get; set; }
        public string ForumTitle { get; set; }
    }
}
