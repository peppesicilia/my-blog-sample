using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceStack.DataAnnotations;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    [Alias("PostTags")]
    public class PostTag : Magicianred.LearnByDoing.MyBlog.Domain.Models.PostTag
    {
    }
}
