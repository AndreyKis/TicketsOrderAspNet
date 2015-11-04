using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllOrders();
            return View(list);
        }


        public ActionResult Create()
        {
            return View();
        }

    }
}
