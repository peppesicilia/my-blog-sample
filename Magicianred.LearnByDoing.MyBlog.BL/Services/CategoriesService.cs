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
    /// <summary>
    /// Service of categories
    /// </summary>
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Retrieve all categories
        /// </summary>
        /// <returns>list of categories</returns>
        public List<Category> GetAll()
        {
            return _categoriesRepository.GetAll().ToList();
        }

        /// <summary>
        /// Retrieve the category by own id
        /// </summary>
        /// <param name="id">id of category to retrieve</param>
        /// <returns>the category, null if id not found</returns>
        public Category GetById(int id)
        {
            return _categoriesRepository.GetById(id);
        }

        public List<Post> GetPostsById(int id)
        {
            return _categoriesRepository.GetPostsById(id).ToList();
        }

        public List<Category> GetPaginatedAll(int page, int pageSize)
        {
            return _categoriesRepository.GetPaginatedAll(page, pageSize).ToList();
        }
    }
}
