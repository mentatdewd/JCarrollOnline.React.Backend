﻿using System.ComponentModel.DataAnnotations;

namespace jcarrollonline.react.backend.Models.ViewModels
{
    public class ForumThreadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public string LastPost { get; set; }
        public DateTime LastPostedDate { get; set; }
    }
}