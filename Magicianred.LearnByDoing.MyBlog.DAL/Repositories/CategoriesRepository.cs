using Dapper;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    /// <summary>
    /// Repository of categories
    /// </summary>
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CategoriesRepository(IDatabaseConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionFactory = connectionFactory;
        }

 
        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> categories = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                categories = connection.Query<Category>("SELECT Id, Name, Description FROM Categories ORDER BY CreateDate DESC");
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
                    category.Posts = connection.Query<Post>("SELECT Id, Title, Text, CategoryId FROM Posts WHERE CategoryId = @CategoryId",
                        new { CategoryId = id }).AsList();
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
                    category.Posts = connection.Query<Post>("SELECT Id, Title, Text, CategoryId FROM Posts WHERE CategoryId = @CategoryId",
                        new { CategoryId = id }).AsList();
                }
            }
            return category.Posts;
        }

        public IEnumerable<Category> GetPaginatedAll(int page, int pageSize)
        {
            IEnumerable<Category> categories = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                var databaseType = _configuration.GetSection("DatabaseType").Value;
                if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mssql")
                {
                    categories = connection.Query<Category>(
                            "SELECT Id, Name, Description FROM Categories ORDER BY CreateDate DESC OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                            new { offset = ((page - 1) * pageSize), pageSize = pageSize });
                }
                else
                {
                    categories = connection.Query<Category>("SELECT Id, Name, Description FROM Categories ORDER BY CreateDate DESC " +
                    "LIMIT @offset, @pageSize ", new { offset = ((page - 1) * pageSize), pageSize = pageSize });
                }
            }

            return categories;
        }
    }
}
