using Magicianred.LearnByDoing.MyBlog.DAL.Repositories;
using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Repositories
{
    [TestFixture]
    public class TagsRepositoryTest
    {
        private TagsRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new TagsRepository(_connectionFactory);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            //_tagsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_tags()
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockTags = TagsHelper.GetMockDataWithPosts(mockPosts);

            var db = new InMemoryDatabase();
            
            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Tag>(mockTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var tags = _sut.GetAll().ToList();

            // Assert
            Assert.IsNotNull(tags);
            Assert.AreEqual(tags.Count(), mockTags.Count);

            mockTags = mockTags.OrderBy(o => o.Id).ToList();
            tags = tags.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockTags.Count; i++)
            {
                var mockTag = mockTags[i];
                var tag = tags[i];
                Assert.IsTrue(mockTag.Id == tag.Id);
                Assert.IsTrue(mockTag.Name == tag.Name);
                Assert.IsTrue(mockTag.Description == tag.Description);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_tag_by_id(int id)
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockTags = TagsHelper.GetMockDataWithPosts(mockPosts);

            var db = new InMemoryDatabase();

            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Tag>(mockTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var tag = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(tag);

            Assert.IsTrue(mockTag.Id == tag.Id);
            Assert.IsTrue(mockTag.Name == tag.Name);
            Assert.IsTrue(mockTag.Description == tag.Description);
        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_tag()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var tag = _sut.GetById(1000);

            // Assert
            Assert.IsNull(tag);
        }


        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_all_posts_of_tag(int id)
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockTags = TagsHelper.GetMockDataWithPosts(mockPosts);

            var db = new InMemoryDatabase();

            db.Insert<Post>(mockPosts);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Tag>(mockTags);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var tag = _sut.GetById(id);

            List<Domain.Models.Post> postsList = null;
            if (tag != null && tag.Posts != null && tag.Posts.Any())
            {
                postsList = tag.Posts;
            }

            // Assert
            Assert.IsNotNull(tag);
            Assert.IsTrue(mockTag.Id == tag.Id);
            Assert.IsTrue(mockTag.Name == tag.Name);
            Assert.IsTrue(mockTag.Description == tag.Description);
            
            //Assert.IsTrue(mockTag.Posts.Equals(tag.Posts));

            Assert.AreEqual(postsList.Count(), mockTag.Posts.Count());

            var mockPostsById = mockTag.Posts.OrderBy(o => o.Id).ToList();
            postsList = postsList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockPostsById.Count(); i++)
            {
                var mockPost = mockPostsById.ElementAt(i);
                var post = postsList[i];
                Assert.IsTrue(mockPost.Id == post.Id);
                Assert.IsTrue(mockPost.Title == post.Title);
                Assert.IsTrue(mockPost.Text == post.Text);
            }
        }

        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [Category("Unit test")]
        public void should_retrieve_all_paginated_tags(int page, int pageSize)
        {
            // Arrange
            
            var mockTags = TagsHelper.GetMockDataForPages().ToList();

            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var recordSize = (page-1)*pageSize;
            mockTags = mockTags.Skip(recordSize).Take(pageSize).ToList();

            // Act
            var tags = _sut.GetPaginatedAll(page, pageSize).ToList();

            // Assert
            Assert.IsNotNull(tags);
            Assert.IsTrue(tags.Count() <= pageSize, "ERRORE: Il numero dei tag è maggiore della dimensione della pagina!");
            Assert.AreEqual(tags.Count, mockTags.Count);

            mockTags = mockTags.OrderBy(o => o.Id).ToList();
            tags = tags.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockTags.Count; i++)
            {
                var mockTag = mockTags[i];
                var tag = tags[i];
                Assert.IsTrue(mockTag.Id == tag.Id);
                Assert.IsTrue(mockTag.Name == tag.Name);
                Assert.IsTrue(mockTag.Description == tag.Description);
            }
        }
    }
}
