using Dapper;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public TagsRepository(IDatabaseConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionFactory = connectionFactory;
        }

 
        public IEnumerable<Tag> GetAll()
        {
            IEnumerable<Tag> tags = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                tags = connection.Query<Tag>("SELECT Id, Name, Description FROM Tags ORDER BY CreateDate DESC");
            }
            return tags;
        }


        public Tag GetById(int id)
        {
            Tag tag = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                tag = connection.QueryFirstOrDefault<Tag>("SELECT Id, Name, Description FROM Tags WHERE Id = @TagId",
                    new { TagId = id });

                if (tag != null)
                {
                    tag.Posts = connection.Query<Post>("SELECT Id, Title, Text, CategoryId FROM Posts WHERE Id IN " +
                        "( SELECT PostId from PostTags WHERE TagId = @TagId )", new { TagId = id }).AsList();
                }
            }
            return tag;
        }

        public IEnumerable<Post> GetPostsById(int id)
        {
            Tag tag = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                // TOP 1 is not a command for SQLite, remove
                tag = connection.QueryFirstOrDefault<Tag>("SELECT Id, Name, Description FROM Tags WHERE Id = @TagId",
                    new { TagId = id });

                if (tag != null)
                {
                    tag.Posts = connection.Query<Post>("SELECT Id, Title, Text, CategoryId FROM Posts WHERE Id IN " +
                        "( SELECT PostId from PostTags WHERE TagId = @TagId )", new { TagId = id }).AsList();
                }
            }
            return tag.Posts;
        }

        public IEnumerable<Tag> GetPaginatedAll(int page, int pageSize)
        {
            IEnumerable<Tag> tags = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                var databaseType = _configuration.GetSection("DatabaseType").Value;
                if (!string.IsNullOrWhiteSpace(databaseType) && databaseType.ToLower().Trim() == "mssql")
                {
                    tags = connection.Query<Tag>(
                            "SELECT Id, Name, Description FROM Tags ORDER BY CreateDate DESC OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                            new { offset = ((page - 1) * pageSize), pageSize = pageSize });
                }
                else
                {
                    tags = connection.Query<Tag>("SELECT Id, Name, Description FROM Tags ORDER BY CreateDate DESC " +
                    "LIMIT @offset, @pageSize ", new { offset = ((page - 1) * pageSize), pageSize = pageSize });
                }
            }

            return tags;
        }
    }
}
