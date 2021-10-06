using Dapper;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    /// <summary>
    /// Repository of categories
    /// </summary>
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CategoriesRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

 
        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> categories = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                categories = connection.Query<Category>("SELECT Id, Name, Description, CategoryId FROM Categories ORDER BY CreateDate DESC");
            }
            return categories;
        }


        public Category GetById(int id)
        {
            Category category = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                category = connection.QueryFirstOrDefault<Category>("SELECT * FROM Categories WHERE Id = @CategoryId", new { CategoryId = id });

                if (category != null)
                {
                    category.Posts = connection.Query<Post>("SELECT * FROM Posts WHERE CategoryId = @CategoryId", new { CategoryId = id }).AsList();
                }
            }
            return category;
        }

        public IEnumerable<Post> GetPostsById(int id)
        {
            Category category = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                category = connection.QueryFirstOrDefault<Category>("SELECT * FROM Categories WHERE Id = @CategoryId", new { CategoryId = id });

                if (category != null)
                {
                    category.Posts = connection.Query<Post>("SELECT * FROM Posts WHERE CategoryId = @CategoryId", new { CategoryId = id }).AsList();
                }
            }
            return category.Posts;
        }
    }
}
