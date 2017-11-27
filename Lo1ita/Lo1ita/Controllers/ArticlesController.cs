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
                article.Hits = article.Hits + 1;
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
        {
            article.Author = "帅宝宝";
            article.CreateDate = DateTime.Now;
            article.UpdateDate = DateTime.Now;
            article.Display = 1;
            db.Articles.Add(article);
            db.SaveChanges();
            return RedirectToAction("MyArticles");
        }
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
            return View(article);
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
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Draft()
        {
    
            return View(db.Articles.ToList().OrderByDescending(r=>r.UpdateDate));
        }
        /// <summary>
        /// 我的文章
        /// </summary>
        /// <returns></returns>
        public ActionResult MyArticles()
        {
            return View(db.Articles.ToList().OrderByDescending(r => r.UpdateDate));
        }

      
      

      

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
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
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
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
