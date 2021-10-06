using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceStack.DataAnnotations;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    /// <summary>
    /// Class for fix problem with OrmLite
    /// Use attribute Alias for correct TableName from Post in Posts
    /// Add DateTime CreateDate because of is returned from OrmLite, if not present exception
    /// </summary>
    [Alias("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
