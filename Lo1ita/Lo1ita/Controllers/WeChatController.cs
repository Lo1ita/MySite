using Lo1ita.Common;
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

            ViewBag.UserTicket = WechatHelper.GetUserTicket("Index");
            ViewBag.AccessToken = WechatHelper.GetAccessToken();
           

            return View();
        }

       
    }
}