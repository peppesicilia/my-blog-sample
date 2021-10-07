using Dapper;
using Magicianred.LearnByDoing.MyBlog.Domain.Interfaces.Repositories;
using Magicianred.LearnByDoing.MyBlog.Domain.Models;
using System.Collections.Generic;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public TagsRepository(IDatabaseConnectionFactory connectionFactory)
        {
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
    }
}
