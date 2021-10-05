using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Post : IPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }
}
