﻿using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.BL.Services
{
    public class TagsService : ITagsService
    {
        private ITagsRepository _tagsRepository;

        public TagsService(ITagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public List<Tag> GetAll()
        {
            return _tagsRepository.GetAll().ToList();
        }

        /// <summary>
        /// Retrieve the tag by own id
        /// </summary>
        /// <param name="id">id of tag to retrieve</param>
        /// <returns>the tag, null if id not found</returns>
        public Tag GetById(int id)
        {
            return _tagsRepository.GetById(id);
        }

        public List<Post> GetPostsById(int id)
        {
            return _tagsRepository.GetPostsById(id).ToList();
        }

        public List<Tag> GetPaginatedAll(int page, int pageSize)
        {
            return _tagsRepository.GetPaginatedAll(page, pageSize).ToList();
        }
    }
}
