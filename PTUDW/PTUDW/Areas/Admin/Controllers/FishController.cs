using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTUDW.Areas.Admin.Controllers
{
    public class FishController : Controller
    {
        // GET: Admin/Fish
        public ActionResult Index()
        {
            return View();
        }
    }
}