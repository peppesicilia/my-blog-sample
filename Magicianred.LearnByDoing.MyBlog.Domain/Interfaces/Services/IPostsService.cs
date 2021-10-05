﻿using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services
{
    public interface IPostsService
    {
        public List<Post> GetAll();
        public Post GetById(int id);
    }
}
