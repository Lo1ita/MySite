namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Web : DbContext
    {
        public Web()
            : base("name=WebDBEntities")
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.DisplayName)
                .IsFixedLength();
        }
    }
}
