﻿namespace jcarrollonline.react.backend.Models.ViewModels
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ThreadCount { get; set; }
        public string LastThreadPost { get; set; }
    }
}
