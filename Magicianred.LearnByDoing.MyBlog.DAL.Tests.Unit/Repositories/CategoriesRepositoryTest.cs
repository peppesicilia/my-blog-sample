using Dapper;
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
    public class CategoriesRepositoryTest
    {
     
        private CategoriesRepository _sut;

        private IDatabaseConnectionFactory _connectionFactory;


        #region SetUp and TearDown

        [OneTimeSetUp]
        public void SetupUpOneTime()
        {
            // Instance of mock
            _connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            _sut = new CategoriesRepository(_connectionFactory);
        }

        [OneTimeTearDown]
        public void TearDownOneTime()
        {
            // dispose
            //_categoriesRepository = null;
        }

        #endregion

        [Test]
        [Category("Unit test")]
        public void should_retrieve_all_categories()
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            // Act
            var categories = _sut.GetAll().ToList();

            // Assert
            Assert.IsNotNull(categories);
            Assert.AreEqual(categories.Count(), mockCategories.Count);

            mockCategories = mockCategories.OrderBy(o => o.Id).ToList();
            categories = categories.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockCategories.Count; i++)
            {
                var mockCategory = mockCategories[i];
                var category = categories[i];
                Assert.IsTrue(mockCategory.Id == category.Id);
                Assert.IsTrue(mockCategory.Name == category.Name);
                Assert.IsTrue(mockCategory.Description == category.Description);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [Category("Unit test")]
        public void should_retrieve_category_by_id(int id)
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            // insert post because of InMemory DB must have table Posts
            var mockPosts = PostsHelper.GetDefaultMockData();

            var db = new InMemoryDatabase();
            
            db.Insert<Category>(mockCategories);
            db.Insert<Post>(mockPosts);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockCategory = mockCategories.Where(x => x.Id == id).FirstOrDefault();

            // Act
            var category = _sut.GetById(id);

            // Assert
            Assert.IsNotNull(category);

            Assert.IsTrue(mockCategory.Id == category.Id);
            Assert.IsTrue(mockCategory.Name == category.Name);
            Assert.IsTrue(mockCategory.Description == category.Description);

        }

        [Test]
        [Category("Unit test")]
        public void should_retrieve_no_one_category()
        {
            // Arrange
            var mockCategories = CategoriesHelper.GetDefaultMockData();
            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

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
            // insert post because of InMemory DB must have table Posts
            var mockPosts = PostsHelper.GetDefaultMockData();

            var db = new InMemoryDatabase();

            db.Insert<Category>(mockCategories);
            db.Insert<Post>(mockPosts);

            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var mockCategory = mockCategories.Where(x => x.Id == id).FirstOrDefault();
            mockCategory.Posts = mockPosts.Where(x => x.CategoryId == id).ToList();
            var mockPostsById = mockPosts.Where(x => x.CategoryId == id).ToList();

            // Act
            var category = _sut.GetById(id);
            // var posts = category.Posts;

            List<Domain.Models.Post> postsList = null;
            if (category != null && category.Posts != null && category.Posts.Any())
            {
                postsList = category.Posts;
            }

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(mockCategory.Id == category.Id);
            Assert.IsTrue(mockCategory.Name == category.Name);
            Assert.IsTrue(mockCategory.Description == category.Description);
            //Assert.IsTrue(mockCategory.Posts.Equals(category.Posts));
         
            Assert.AreEqual(postsList.Count(), mockPostsById.Count());

            mockPostsById = mockPostsById.OrderBy(o => o.Id).ToList();
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
        public void should_retrieve_all_paginated_categories(int page, int pageSize)
        {
            // Arrange

            var mockCategories = CategoriesHelper.GetMockDataForPages().ToList();

            var db = new InMemoryDatabase();
            db.Insert<Category>(mockCategories);
            _connectionFactory.GetConnection().Returns(db.OpenConnection());

            var recordSize = (page - 1) * pageSize;
            mockCategories = mockCategories.Skip(recordSize).Take(pageSize).ToList();

            // Act
            var categories = _sut.GetPaginatedAll(page, pageSize).ToList();

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() <= pageSize, "ERRORE: Il numero delle categorie è maggiore della dimensione della pagina!");
            Assert.AreEqual(categories.Count, mockCategories.Count);

            mockCategories = mockCategories.OrderBy(o => o.Id).ToList();
            categories = categories.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < mockCategories.Count; i++)
            {
                var mockCategory = mockCategories[i];
                var category = categories[i];
                Assert.IsTrue(mockCategory.Id == category.Id);
                Assert.IsTrue(mockCategory.Name == category.Name);
                Assert.IsTrue(mockCategory.Description == category.Description);
            }
        }
    }
}
