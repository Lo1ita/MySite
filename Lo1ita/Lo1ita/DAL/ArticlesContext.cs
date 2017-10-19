using Lo1ita.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Lo1ita.DAL
{
    public class ArticlesContext:DbContext
    {
        public ArticlesContext() : base("WebDBEntities")
        {

        }
        public DbSet<Article> articles { get; set; }
        //默认情况生成复数形式的表
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}