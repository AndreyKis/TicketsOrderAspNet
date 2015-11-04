using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class RateController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllRates();
            return View(list);
        }


        public ActionResult Create()
        {
            var rate = new Common.Rate();
            ViewBag.canAdd = true;
            return View(rate);

        }


        [HttpPost, ActionName("Create")]
        public ActionResult RateCreate(Common.Rate rate)
        {

            if (db.AddRate(rate))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(rate);
            }

        }

        public ActionResult Delete(int id)
        {
            var rate = db.GetRateByID(id);
            ViewBag.canDelete = true;
            return View(rate);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult RateDelete(int id)
        {

            int y = 0;
            if (db.DeleteRate(db.GetRateByID(id)))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(db.GetRateByID(id));
            }

        }

    }
}
