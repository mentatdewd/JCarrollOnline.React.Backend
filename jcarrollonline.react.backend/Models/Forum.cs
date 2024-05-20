using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcarrollonline.react.backend.Models
{
    [Table("Fora")]
    public class Forum
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; } // :null => false

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime UpdatedAt { get; set; } //:null => false

        public virtual ICollection<ForumThread> Threads { get; }
    }
}
