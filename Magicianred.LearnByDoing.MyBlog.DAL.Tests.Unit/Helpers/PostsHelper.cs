using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Helpers
{
    public static class PostsHelper
    {
        public static List<Post> GetDefaultMockData()
        {
            List<Post> mockPosts = new List<Post>();
            mockPosts.Add(new Post()
            {
                Id = 1,
                Title = "This is a title for post 1",
                Text = "This is a text for post 1",
                CategoryId = 1,
                Author = "Tom"
            }); 
            mockPosts.Add(new Post()
            {
                Id = 2,
                Title = "This is a title for post 2",
                Text = "This is a text for post 2",
                CategoryId = 1,
                Author = "Jim"
            });
            mockPosts.Add(new Post()
            {
                Id = 3,
                Title = "This is a title for post 3",
                Text = "This is a text for post 3",
                CategoryId = 2,
                Author = "Tom"
            });
            return mockPosts;
        }

        public static List<Post> GetMockDataWithTags(List<Tag> mockTags)
        {
            List<Post> mockPosts = PostsHelper.GetDefaultMockData();
            mockPosts[0].Tags = new List<Tag>();
            mockPosts[0].Tags.Add(mockTags[0]);

            mockPosts[1].Tags = new List<Tag>();
            mockPosts[1].Tags.Add(mockTags[1]);

            mockPosts[2].Tags = new List<Tag>();
            mockPosts[2].Tags.Add(mockTags[1]);

            return mockPosts;
        }
    }
}