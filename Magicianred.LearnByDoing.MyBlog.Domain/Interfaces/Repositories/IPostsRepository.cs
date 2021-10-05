using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        public IEnumerable<Post> GetAll();
        Post GetById(int id);
    }
}
