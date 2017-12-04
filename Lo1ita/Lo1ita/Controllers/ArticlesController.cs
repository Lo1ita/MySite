using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lo1ita.Model;

namespace Lo1ita.Controllers
{
    public class ArticlesController : Controller
    {
        private web_dbEntities db = new web_dbEntities();



        /// <summary>
        /// 文章查看页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            //文章点击数
            if (article.Hits == null)
            {
                article.Hits = 1;
            }
            else
            {
                article.Hits = article.Hits + 1;  //每进入一次详细文章页面，点击数加1
            }
            db.SaveChanges();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //写文章
        public ActionResult Create()
        {
            Article article = new Article();
            return View("Edit", article);
        }

        // POST: Articles/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article)
        {//如果ID为0,表示新增（事实上这种情况基本不存在，因为文章会自动保存，生成ID）
            if (article.ID == 0)
            {
                article.Author = "帅宝宝";
                article.CreateDate = DateTime.Now;
                article.UpdateDate = DateTime.Now;
                article.Display = 1;
                article.isDraft = 0;
                db.Articles.Add(article);
            }
            else
            {
                Article article2 = db.Articles.Find(article.ID);
                article2.UpdateDate = DateTime.Now;
                article2.isDraft = 0;
                article2.Textlength = article.Textlength;
                article2.Title = article.Title;
                article2.Details = article.Details;
                article2.Display = 1;
                db.Entry(article2).State = EntityState.Modified;
            }
            
            db.SaveChanges();
            return RedirectToAction("MyArticles");
        }
        //编辑页面
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View("Edit", article);
        }

        // POST: Articles/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            
            article.UpdateDate = DateTime.Now;
            article.isDraft = 0;
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyArticles");
        }
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Draft()
        {
            return View(db.Articles.ToList().OrderByDescending(r => r.UpdateDate).Where(r => r.isDraft ==1));
        }
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        public ActionResult MyArticles()
        {
            return View(db.Articles.ToList().OrderByDescending(r => r.UpdateDate).Where(r=>r.isDraft==0));
        }
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveDraft(int? id, string title, string content, int length)
        {
            var ret = new JsonResult();
            //如果是第一次加载，id为空
            if (id == null|| id == 0)
            {
                Article article = new Article();
                article.Author = "js";
                article.CreateDate = DateTime.Now;
                article.Details = content;  //正文
                article.isDraft = 1;   //是否为草稿
                article.Title = title;   //题目
                article.UpdateDate = DateTime.Now;
                article.Textlength = length;
                db.Articles.Add(article);
                article.Display = 1;
                db.SaveChanges();
                ret.Data = new { id = article.ID, msg = "success" };
                
            }
            else
            {
                Article article = db.Articles.Find(id);
                article.Title = title;
                article.Details = content;
                article.Display = 1;   //是否显示
                article.isDraft = 1;  //是否为草稿
                article.UpdateDate = DateTime.Now;
                article.Textlength = length;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                ret.Data = new { msg = "success" };
            }
            return ret;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteArticle(int articleID)
        {
            Article article = db.Articles.Find(articleID);
            db.Articles.Remove(article);
            db.SaveChanges();
            return Content("success");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
