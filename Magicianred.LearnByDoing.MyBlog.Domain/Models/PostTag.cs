﻿using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class PostTag : IPostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
