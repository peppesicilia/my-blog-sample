using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Models
{
    public interface ICategory
    {
        int Id { get; set; }
        string Title { get; set; }
        string Text { get; set; }

        int CategoryId { get; set; }
    }
}
