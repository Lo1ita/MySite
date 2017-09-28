namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Details { get; set; }

        public DateTime? UpdateData { get; set; }

        public DateTime? CreatDate { get; set; }

        public int? Display { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        [StringLength(50)]
        public string CreaterGuid { get; set; }

        public int? Hits { get; set; }

        [StringLength(250)]
        public string Excerpt { get; set; }

        public int? Type { get; set; }
    }
}
