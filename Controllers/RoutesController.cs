using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceApplication.Controllers
{
    public class RoutesController : Controller
    {
        // GET: RoutesDemo
        public ActionResult AddForm()
        {
            return View();
        }

        public ActionResult PageList()
        {
            return View();
        }
        public ActionResult InvoiceList()
        {
            return View();
        }
    }
}