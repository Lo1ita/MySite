using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lo1ita.Model
{
    public class WeChatUser
    {
        //成员UserID。对应管理端的帐号
        public string userid { get; set; }
        //成员名称
        public string name { get; set; }
        //手机号码，第三方仅通讯录套件可获取
        public string mobile { get; set; }
        //成员所属部门id列表
        public string department { get; set; }
        //部门内的排序值，默认为0。数量必须和department一致，数值越大排序越前面。值范围是[0, 2^32)
        public int order { get; set; }
        //职位信息
        public string position { get; set; }
        //性别。0表示未定义，1表示男性，2表示女性
        public int gender { get; set; }
        //邮箱，第三方仅通讯录套件可获取
        public string email { get; set; }
        //	上级字段，标识是否为上级。
        public string isleader { get; set; }
        //头像url。注：如果要获取小图将url最后的”/0”改成”/100”即可
        public string avatar { get; set; }
        //座机
        public string telephone { get; set; }
        //英文名
        public string english_name { get; set; }
      
    }
}