using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Models
{
    public partial class Pagination : IPagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
    }
}
