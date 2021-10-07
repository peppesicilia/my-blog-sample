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
    public class TagsServiceTest
    {
        private TagsService _sut;

        private ITagsRepository _tagsRepository;

        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _tagsRepository = Substitute.For<ITagsRepository>();
            _sut = new TagsService(_tagsRepository);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            _tagsRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_tags()
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            _tagsRepository.GetAll().Returns(mockTags);

            // Act
            var tags = _sut.GetAll();

            // Assert
            Assert.IsNotNull(tags);
            Assert.AreEqual(tags.Count, mockTags.Count);
            foreach(var tag in tags)
            {
                Assert.IsTrue(mockTags.Contains(tag));
                mockTags.Remove(tag);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_one_tag_by_id(int id)
        {
            // Arrange
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            _tagsRepository.GetById(mockTag.Id).Returns(mockTag);

            // Act
            var tag = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id == id);
        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_tag()
        {
            // Arrange
            Tag mockTag = null;

            _tagsRepository.GetById(1000).Returns(mockTag);

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
            var mockTags = TagsHelper.GetDefaultMockData();
            var mockTag = mockTags.Where(x => x.Id == id).FirstOrDefault();

            var mockPostTags = PostTagsHelper.GetDefaultMockData().Where(x => x.TagId == id).ToList();
            
            //Opzione 1
            var mockPostTagsIds = mockPostTags.Select(x => x.PostId).ToList();
            var mockPostsById = PostsHelper.GetDefaultMockData().Where(x => mockPostTagsIds.Contains(x.Id)).ToList();

            /*
            Opzione 2
            var mockPosts = PostsHelper.GetDefaultMockData().ToList();
            List<Post> mockPostsById = new List<Post>();
            foreach (var p in mockPosts)
            {
                foreach (var postTag in mockPostTags)
                {
                    if (p.Id == postTag.PostId)
                    {
                        mockPostsById.Add(p);
                    }
                }
            }

            _tagsRepository.GetPostsById(mockTag.Id).Returns(mockPostsById);
            */

            _tagsRepository.GetById(mockTag.Id).Returns(mockTag);
            _tagsRepository.GetPostsById(mockTag.Id).Returns(mockPostsById);
            

            // Act
            var tag = _sut.GetById(id);
            var posts = _sut.GetPostsById(id);

            // Assert
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id == id);
            Assert.AreEqual(posts.Count, mockPostsById.Count);
            foreach (var post in posts)
            {
                Assert.IsTrue(mockPostsById.Contains(post));
                mockPostsById.Remove(post);
            }
        }
    }
}
