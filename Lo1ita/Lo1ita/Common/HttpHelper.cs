using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Lo1ita.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponse(string url)
        {
            if (url.StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
        public static T GetResponse<T>(string url)
            where T : class, new()
        {
            if (url.StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            T result = default(T);
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;
                result = JsonConvert.DeserializeObject<T>(s);
            }
            return result;
        }
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostResponse(string url, string postData)
        {
            if (url.StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        public static T PostResponse<T>(string url, string postData)
            where T : class, new()
        {
            if (url.StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();
            T result = default(T);
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;
                result = JsonConvert.DeserializeObject<T>(s);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T PostXmlResponse<T>(string url, string xmlString)
            where T : class, new()
        {
            if (url.StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }
            HttpContent httpContent = new StringContent(xmlString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();
            T result = default(T);

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = XmlDeserialize<T>(s);
            }
            return result;
        }
        /// <summary>
        /// 反序列化XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlString)
         where T : class, new()
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(xmlString))
                {
                    return (T)ser.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
            }
        }
    }
}
