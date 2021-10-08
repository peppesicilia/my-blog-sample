using Magicianred.LearnByDoing.MyBlog.BL.Services;
using Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Services
{
    [TestFixture]
    public class PostsServiceTest
    {
        /// <summary>
        /// PostsService is our System Under Test
        /// </summary>
        private PostsService _sut;

        /// <summary>
        /// A mock of posts repository
        /// </summary>
        private IPostsRepository _postsRepository;

        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _postsRepository = Substitute.For<IPostsRepository>();
            _sut = new PostsService(_postsRepository);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            _postsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_posts()
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            _postsRepository.GetAll().Returns(mockPosts);

            // Act
            var posts = _sut.GetAll();

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.Count, mockPosts.Count);
            foreach(var post in posts)
            {
                Assert.IsTrue(mockPosts.Contains(post));
                mockPosts.Remove(post);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Category("Unit test")]
        public void should_retrieve_one_post_by_id(int id)
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();

            _postsRepository.GetById(mockPost.Id).Returns(mockPost);

            // Act
            var post = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(post);
            Assert.IsTrue(post.Id == id);
        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_post()
        {
            // Arrange
            Post mockPost = null;

            _postsRepository.GetById(1000).Returns(mockPost);

            // Act
            var post = _sut.GetById(1000);

            // Assert
            Assert.IsNull(post);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Category("Unit test")]
        public void should_retrieve_all_tags_of_post(int id)
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();
            var mockPostTags = PostTagsHelper.GetDefaultMockData().Where(x => x.PostId == id).ToList();

            //By Simone
            var mockPostTagsIds = mockPostTags.Select(x => x.TagId).ToList();
            var mockTagsById = TagsHelper.GetDefaultMockData().Where(x => mockPostTagsIds.Contains(x.Id)).ToList();

            /*
            var mockTags = TagsHelper.GetDefaultMockData().ToList();
            List<Tag> mockTagsById = new List<Tag>();
            foreach (var tag in mockTags)
            {
                foreach (var postTag in mockPostTags)
                {
                    if (tag.Id == postTag.TagId)
                    {
                        mockTagsById.Add(tag);
                    }
                }
            }
            */

            _postsRepository.GetById(mockPost.Id).Returns(mockPost);
            _postsRepository.GetTagsById(mockPost.Id).Returns(mockTagsById);

            // Act
            var post = _sut.GetById(id);
            var tags = _sut.GetTagsById(id);

            // Assert
            Assert.IsNotNull(post);
            Assert.IsTrue(post.Id == id);
            Assert.AreEqual(tags.Count, mockTagsById.Count);
            foreach (var tag in tags)
            {
                Assert.IsTrue(mockTagsById.Contains(tag));
                mockTagsById.Remove(tag);
            }
        }

        [TestCase("Tom")]
        [TestCase("Jim")]
        [Category("Unit test")]
        public void should_retrieve_all_posts_by_author(string author)
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData().Where(x => x.Author.Equals(author)).ToList();

            _postsRepository.GetAllByAuthor(author).Returns(mockPosts);

            // Act
            var posts = _sut.GetAllByAuthor(author);

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.Count, mockPosts.Count);
            foreach (var post in posts)
            {
                Assert.IsTrue(mockPosts.Contains(post));
                mockPosts.Remove(post);
            }
        }

        [TestCase(1, 3)]
        [TestCase(2, 4)]
        [Category("Unit test")]
        public void should_retrieve_all_paginated_posts(int page, int pageSize)
        {
            // Arrange
            
            var mockPosts = PostsHelper.GetMockDataForPages().Take(pageSize).Skip(page).ToList();

            _postsRepository.GetPaginatedAll(page, pageSize).Returns(mockPosts);

            // Act
            var posts = _sut.GetPaginatedAll(page, pageSize);

            // Assert
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count <= pageSize);

            Assert.AreEqual(posts.Count, mockPosts.Count);
            foreach (var post in posts)
            {
                Assert.IsTrue(mockPosts.Contains(post));
                mockPosts.Remove(post);
            }

        }
    }
}
