//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lo1ita.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attachment
    {
        public int ID { get; set; }
        public Nullable<int> RefTableID { get; set; }
        public string RefTableName { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string DisplayName { get; set; }
        public string RelativePath { get; set; }
        public string ContentType { get; set; }
        public Nullable<System.DateTime> CreatDate { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
