using Lo1ita.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Lo1ita.Common
{
    public class WechatHelper
    {
        public static string SiteUrl= ConfigurationManager.AppSettings["SiteUrl"];
        public  static string CORPID = ConfigurationManager.AppSettings["CorpID"];
       public  static string SECRET = ConfigurationManager.AppSettings["Secret"];
        public static string AgentId = ConfigurationManager.AppSettings["AgentId"];
        public static  string code = "";
        /// <summary>
        /// 获取用户Ticket
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetUserTicket(string type)
        {
            string userticket = "";
            if (HttpContext.Current.Request.Cookies["user_ticket"] == null)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var client = new System.Net.WebClient();
                var serializer = new JavaScriptSerializer();
                string code = GetCode(type);//获取code
                string token = GetAccessToken(); //获取accessToken
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", token, code);
                client.Encoding = System.Text.Encoding.UTF8;
                string userInfo = "";
                try
                {
                    userInfo = client.DownloadString(url);
                }
                catch (Exception)
                {

                    throw;
                }

                //获取字典
                dict = serializer.Deserialize<Dictionary<string, string>>(userInfo);


                if (dict.TryGetValue("user_ticket", out userticket))//判断userticket是否存在
                {
                    SetCookie("user_ticket", dict["user_ticket"], 5);
                }
                else  //access_Token 失效时重新发送。
                {
                    //存log方法
                }

            }
            else
            {
                userticket = HttpContext.Current.Request.Cookies["user_ticket"].Value;
            }
            return userticket;
        }
        /// <summary>
        /// 获取code代码
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static string GetCode(string TypeName)
        {
            
            if (HttpContext.Current.Request.QueryString["code"] != null)  //判断code是否存在
            {
                
                if(HttpContext.Current.Request.Cookies["code"] ==null)  //判断是否是第二次进入
                {
                    SetCookie("code", HttpContext.Current.Request.QueryString["code"], 5);
                    code = HttpContext.Current.Request.QueryString["code"];
                }
                else
                {
                    delCookies("code");   //删除cookies
                    CodeURL(TypeName); //code重新跳转URL
                }

            }
            else
            {
                CodeURL(TypeName); //code跳转URL
            }
            return code;
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            string accessToken = "";
            if (HttpContext.Current.Request.Cookies["access_token"] == null)
            {
                Dictionary<string, string> obj = new Dictionary<string, string>();
                var client = new System.Net.WebClient();
                var serializer = new JavaScriptSerializer();
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CORPID, SECRET);
                client.Encoding = System.Text.Encoding.UTF8;
                string dataaccess = "";
                try
                {
                    dataaccess = client.DownloadString(url);
                    
                }
                catch (Exception e)
                {
                    // 存log方法

                }
                //获取字典
                obj = serializer.Deserialize<Dictionary<string, string>>(dataaccess);
                

                if (obj.TryGetValue("access_token", out accessToken))//判断access_Token是否存在
                {
                    SetCookie("access_token", obj["access_token"], 120);
                }
                else  //access_Token 失效时重新发送。
                {
                    //存log方法
                }
                
            }
            else
            {
                accessToken= HttpContext.Current.Request.Cookies["access_token"].Value;
            }
            return accessToken;

        }
        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void SetCookie(string name,string value,int time)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Name = name;
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(time);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="name"></param>
     public static void delCookies(string name)
        {
            foreach(string cookiename in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Today.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Request.Cookies.Remove(name);
                }
            }
        }
        /// <summary>
        /// 跳转codeUrl
        /// </summary>
        /// <param name="TypeName"></param>
        public static void CodeURL(string TypeName)
        {
            string url = "";
            string locationhref = "http://" + SiteUrl + "/WeChat/" + TypeName;
            url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&agentid={2}&state=STATE#wechat_redirect", CORPID, locationhref, AgentId);
            HttpContext.Current.Response.Redirect(url);
        }

        public static WeChatUser getUserInfo()
        {
            string url = "";
            string postdata= @"{"user_ticket": "USER_TICKET"}"
            HttpHelper.PostFunction(url,)
            PostFunction()
        }
    }
}