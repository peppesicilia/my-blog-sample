﻿using Magicianred.LearnByDoing.MyBlog.BL.Services;
using Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using NSubstitute;
using NUnit.Framework;
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
            _categoriesRepository = Substitute.For<ICategoriesRepository>();
            _sut = new CategoriesService(_categoriesRepository);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            _categoriesRepository = null;
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
    }
}
