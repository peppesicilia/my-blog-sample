﻿using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services
{
    public interface ITagsService
    {
        public List<Tag> GetAll();
        public Tag GetById(int id);
        public List<Post> GetPostsById(int id);
        public List<Tag> GetPaginatedAll(int page, int pageSize);
    }
}
