using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers
{
    public static class TagsHelper
    {
        public static List<Tag> GetDefaultMockData()
        {
            List<Tag> mockTags = new List<Tag>();
            mockTags.Add(new Tag()
            {
                Id = 1,
                Name = "This is a name for tag 1",
                Description = "This is a description for tag 1"
            });
            mockTags.Add(new Tag()
            {
                Id = 2,
                Name = "This is a name for tag 2",
                Description = "This is a description for tag 2"
            });
            return mockTags;
        }

        public static List<Tag> GetMockDataForPages()
        {
            List<Tag> mockTags = TagsHelper.GetDefaultMockData();

            mockTags.Add(new Tag()
            {
                Id = 3,
                Name = "This is a name for tag 3",
                Description = "This is a description for tag 3"
            });
            mockTags.Add(new Tag()
            {
                Id = 4,
                Name = "This is a name for tag 4",
                Description = "This is a description for tag 4"
            });

            return mockTags;
        }
    }
}
