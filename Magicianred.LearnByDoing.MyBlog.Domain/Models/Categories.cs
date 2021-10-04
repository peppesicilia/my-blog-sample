using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Post : IPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
    }
}
