using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Services
{
    public interface IPostsService
    {
        public List<Post> GetAll();
        public Post GetById(int id);
        public List<Tag> GetTagsById(int id);
        public List<Post> GetAllByAuthor(string author);
        public List<Post> GetPaginatedAll(int page, int pageSize);

    }
}
