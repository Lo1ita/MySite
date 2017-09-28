namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attachment")]
    public partial class Attachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? RefTableID { get; set; }

        [StringLength(100)]
        public string RefTableName { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        [StringLength(256)]
        public string DisplayName { get; set; }

        [StringLength(256)]
        public string RelativePath { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public DateTime? CreatDate { get; set; }

        public int? IsDeleted { get; set; }
    }
}
