﻿using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Post : IPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public List<Tag> Tags { get; set; }
        public string Author { get; set; }
    }
}
