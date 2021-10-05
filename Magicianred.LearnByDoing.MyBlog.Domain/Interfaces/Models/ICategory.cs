using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models
{
    public interface ICategory
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        DateTime CreateDate { get; set; }

        public List<Post> Posts { get; set; }
    }
}
