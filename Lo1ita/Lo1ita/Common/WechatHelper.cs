using Lo1ita.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
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
        /// 获取用户ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetUserID(string type)
        {
            string UserId = "";
            if (HttpContext.Current.Request.Cookies["UserId"] == null)
            {
               
                string code = GetCode(type);//获取code
                string token = GetAccessToken(); //获取accessToken
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", token, code);
                try
                {
                    string result = HttpHelper.GetResponse(url);
                    JObject outputObj = JObject.Parse(result);
                    UserId = outputObj["UserId"].ToString();
                    if(!string.IsNullOrEmpty(UserId))
                        SetCookie("UserId", UserId, 5);
                }
                catch (Exception e)
                {
                }
             
            }
            else
            {
                UserId = HttpContext.Current.Request.Cookies["UserId"].Value;
            }
            return UserId;
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
                    //将code存到cookies,时间为5分钟
                    SetCookie("code", HttpContext.Current.Request.QueryString["code"], 5);
                    code = HttpContext.Current.Request.QueryString["code"];
                }
                else   //如果code没有获取成功，重新拉去一遍
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
               
                string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CORPID, SECRET);
                try
                {
                    string result = HttpHelper.GetResponse(url);
                    JObject outputObj = JObject.Parse(result);
                    accessToken = outputObj["access_token"].ToString();
                    if(!string.IsNullOrEmpty(accessToken))
                        SetCookie("access_token", accessToken, 120);
                }
                catch (Exception e)
                {

                   
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

        public static WeChatUser getUserInfo(string userid,string accesstoken)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", accesstoken,userid);
            WeChatUser weChatUser = new WeChatUser();
            try
            {
                string result = HttpHelper.GetResponse(url);
                JObject outputObj = JObject.Parse(result);
                
                if (outputObj != null)
                {
                    weChatUser.avatar = outputObj["avatar"].ToString();
                    weChatUser.department = outputObj["department"].ToString();
                    weChatUser.email = outputObj["email"].ToString();

                    weChatUser.gender = Convert.ToInt32(outputObj["gender"]);

                    weChatUser.mobile = outputObj["mobile"].ToString();
                    weChatUser.name = outputObj["name"].ToString();

                    weChatUser.position = outputObj["position"].ToString();

                    weChatUser.userid = outputObj["userid"].ToString();
                }
                else
                {
                    throw new Exception("你还不是本企业员工!");
                }
              
               
            }
            catch (Exception e)
            {

                throw e;
            }
           
           
            return weChatUser;

        }
    }
}