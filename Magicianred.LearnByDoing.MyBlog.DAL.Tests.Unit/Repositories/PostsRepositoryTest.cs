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
    public class PostsRepositoryTest
    {
        /// <summary>
        /// PostsService is our System Under Test
        /// </summary>
        private PostsRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new PostsRepository(_connectionFactory);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            //_postsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_posts()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetMockDataWithTags(mockTags);
            var db = new InMemoryDatabase();
            db.Insert<Tag>(mockTags);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Post>(mockPosts);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var posts = _sut.GetAll();
            var postsList = posts.ToList();

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.Count(), mockPosts.Count);

            mockPosts = mockPosts.OrderBy(o => o.Id).ToList();
            postsList = postsList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockPosts.Count; i++)
            {
                var mockPost = mockPosts[i];
                var post = postsList[i];
                Assert.IsTrue(mockPost.Id == post.Id);
                Assert.IsTrue(mockPost.Title == post.Title);
                Assert.IsTrue(mockPost.Text == post.Text);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Category("Unit test")]
        public void should_retrieve_post_by_id(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetMockDataWithTags(mockTags);

            var db = new InMemoryDatabase();
            
            db.Insert<Tag>(mockTags);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Post>(mockPosts);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var post = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(post);

            Assert.IsTrue(mockPost.Id == post.Id);
            Assert.IsTrue(mockPost.Title == post.Title);
            Assert.IsTrue(mockPost.Text == post.Text);

        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_post()
        {
            // Arrange
            var mockPosts = PostsHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Post>(mockPosts);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

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
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockPostTags = PostTagsHelper.GetDefaultMockData();
            //var mockPosts = PostsHelper.GetDefaultMockData();
            var mockPosts = PostsHelper.GetMockDataWithTags(mockTags);
            //mockTags = TagsHelper.GetMockDataWithPosts(mockPosts);

            var db = new InMemoryDatabase();

            db.Insert<Tag>(mockTags);
            db.Insert<PostTag>(mockPostTags);
            db.Insert<Post>(mockPosts);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockPost = mockPosts.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var post = _sut.GetById(id);

            List<Domain.Models.Tag> tagsList = null;
            if (post != null && post.Tags != null && post.Tags.Any())
            {
                tagsList = post.Tags;
            }

            // Assert
            Assert.IsNotNull(post);
            Assert.IsTrue(mockPost.Id == post.Id);
            Assert.IsTrue(mockPost.Title == post.Title);
            Assert.IsTrue(mockPost.Text == post.Text);
            //Assert.IsTrue(mockPost.Tags.Equals(post.Tags));

            Assert.AreEqual(tagsList.Count(), mockPost.Tags.Count());

            var mockTagsById = mockPost.Tags.OrderBy(o => o.Id).ToList();
            tagsList = tagsList.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockTagsById.Count(); i++)
            {
                var mockTag = mockTagsById.ElementAt(i);
                var tag = tagsList[i];
                Assert.IsTrue(mockTag.Id == tag.Id);
                Assert.IsTrue(mockTag.Name == tag.Name);
                Assert.IsTrue(mockTag.Description == tag.Description);
            }
        }
    }
}
