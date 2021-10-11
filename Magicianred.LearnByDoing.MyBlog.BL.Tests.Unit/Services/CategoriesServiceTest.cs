using Magicianred.LearnByDoing.MyBlog.BL.Services;
using Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Helpers;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace Magicianred.LearnByDoing.MyBlog.BL.Tests.Unit.Services
{
    [TestFixture]
    public class CategoriesServiceTest
    {
        /// <summary>
        /// CategoriesService is our System Under Test
        /// </summary>
        private CategoriesService _sut;

        /// <summary>
        /// A mock of categories repository
        /// </summary>
        private ICategoriesRepository _categoriesRepository;

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
        public void should_retrieve_all_categories()
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            _categoriesRepository.GetAll().Returns(mockCategories);

            // Act
            var categories = _sut.GetAll();

            // Assert
            Assert.IsNotNull(categories);
            Assert.AreEqual(categories.Count, mockCategories.Count);
            foreach(var category in categories)
            {
                Assert.IsTrue(mockCategories.Contains(category));
                mockCategories.Remove(category);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_one_category_by_id(int id)
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var mockCategory = mockCategories.Where(x => x.Id == id).FirstOrDefault();

            _categoriesRepository.GetById(mockCategory.Id).Returns(mockCategory);

            // Act
            var category = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(category.Id == id);
        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_category()
        {
            // Arrange
            Category mockCategory = null;

            _categoriesRepository.GetById(1000).Returns(mockCategory);

            // Act
            var category = _sut.GetById(1000);

            // Assert
            Assert.IsNull(category);
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_all_posts_of_category(int id)
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var mockCategory = mockCategories.Where(x => x.Id == id).FirstOrDefault();

            var mockPosts = PostsHelper.GetDefaultMockData().Where(x => x.CategoryId == id).ToList();
            //var mockPostsById = mockPosts.Where(x => x.CategoryId == id).ToList();

            _categoriesRepository.GetById(mockCategory.Id).Returns(mockCategory);
            _categoriesRepository.GetPostsById(mockCategory.Id).Returns(mockPosts);

            // Act
            var category = _sut.GetById(id);
            var posts = _sut.GetPostsById(id);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(category.Id == id);
            Assert.AreEqual(posts.Count, mockPosts.Count);
            foreach (var post in posts)
            {
                Assert.IsTrue(mockPosts.Contains(post));
                mockPosts.Remove(post);
            }
        }

        [TestCase(1, 3)]
        [TestCase(2, 2)]
        [Category("Unit test")]
        public void should_retrieve_all_paginated_categories(int page, int pageSize)
        {
            // Arrange

            var mockCategories = CategoriesHelper.GetMockDataForPages().Take(pageSize).Skip(page).ToList();

            _categoriesRepository.GetPaginatedAll(page, pageSize).Returns(mockCategories);

            // Act
            var categories = _sut.GetPaginatedAll(page, pageSize);

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count <= pageSize);

            Assert.AreEqual(categories.Count, mockCategories.Count);
            foreach (var tag in categories)
            {
                Assert.IsTrue(mockCategories.Contains(tag));
                mockCategories.Remove(tag);
            }
        }
    }
}
