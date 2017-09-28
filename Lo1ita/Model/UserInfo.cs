namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string UserGuid { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        public int? Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public DateTime? CreatDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
