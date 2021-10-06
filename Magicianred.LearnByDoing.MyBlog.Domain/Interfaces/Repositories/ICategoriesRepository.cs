using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories
{
    public interface ICategoriesRepository
    {
        public IEnumerable<Category> GetAll();
        Category GetById(int id);
        public IEnumerable<Post> GetPostsById(int id);
    }
}
