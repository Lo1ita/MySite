using Lo1ita.Common;
using Lo1ita.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Lo1ita.Controllers
{
    public class WeChatController : Controller
    {
        
        
        // GET: WeChat
        public ActionResult Index()
        {
            string UserTicket = WechatHelper.GetUserID("Index");
            string AccessToken = WechatHelper.GetAccessToken();
            WeChatUser UserInfo = WechatHelper.getUserInfo(UserTicket, AccessToken);
            //WeChatUser UserInfo = new WeChatUser();
            ViewBag.UserInfo = UserInfo;
          
            return View();
        }

       
    }
}