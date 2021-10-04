using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CategoryId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Post> Posts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
