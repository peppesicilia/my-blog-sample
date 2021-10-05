using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceStack.DataAnnotations;

namespace Magicianred.LearnByDoing.MyBlog.DAL.Tests.Unit.Models
{
    [Alias("Categories")]
    public class Category : Magicianred.LearnByDoing.MyBlog.Domain.Models.Category
    {
        public new DateTime CreateDate { get; set; }
    }
}
