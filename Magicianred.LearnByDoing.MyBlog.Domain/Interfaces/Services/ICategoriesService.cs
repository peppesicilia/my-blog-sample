using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services
{
    public interface ICategoriesService
    {
        public List<Category> GetAll();
        public Category GetById(int id);

        public List<Post> GetPostsById(int id);
    }
}
