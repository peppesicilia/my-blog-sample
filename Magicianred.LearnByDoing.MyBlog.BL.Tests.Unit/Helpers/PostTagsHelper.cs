using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers
{
    public static class PostTagsHelper
    {
        public static List<PostTag> GetDefaultMockData()
        {
            List<PostTag> mockPostTags = new List<PostTag>();
            mockPostTags.Add(new PostTag()
            {
                PostId = 1,
                TagId = 1
            });
            mockPostTags.Add(new PostTag()
            {
                PostId = 2,
                TagId = 2
            });
            mockPostTags.Add(new PostTag()
            {
                PostId = 3,
                TagId = 2
            });

            return mockPostTags;
        }
    }
}
