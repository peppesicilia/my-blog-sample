﻿using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Tag : ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }
    }
}
