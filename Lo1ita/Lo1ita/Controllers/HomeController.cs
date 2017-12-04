using Lo1ita.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lo1ita.Controllers
{
    public class HomeController : Controller
    {
        private web_dbEntities db = new web_dbEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MainPage(FormCollection collection)
        {
            string searchText = collection["searchText"];
            IEnumerable<Article> article = db.Articles.ToList().OrderByDescending(r => r.UpdateDate).Where(r => r.isDraft == 0);
            return View(article);
        }
    }
}