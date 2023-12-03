using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTUDW.Areas.Admin.Controllers
{
    public class AiLaTrieuPhuController : Controller
    {
        // GET: Admin/AiLaTrieuPhu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HowToPlay()
        {
            return View();
        }
        public ActionResult PlayGame()
        {
            return View();
        }
    }
}