using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lo1ita.Controllers
{
    public class WeChatController : Controller
    {
        static string CORPID = ConfigurationManager.AppSettings["CorpID"];
        static string SECRET= ConfigurationManager.AppSettings["Secret"];
        static DateTime _lastGetTimeOfAccessToken = DateTime.Now.AddSeconds(-7201);
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }

        public static GetAccessToken()
        {
            try
            {
                if(_lastGetTimeOfAccessToken < DateTime.Now)
                {
                    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CORPID, SECRET);
                    string responseText= HttpHelper.Instance.get(url);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}