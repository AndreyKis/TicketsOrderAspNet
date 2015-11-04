using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class PriceController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllPrices();
            return View(list);
        }

        public ActionResult Create()
        {
            var price = new Common.Price();
            ViewBag.canAdd = true;
            return View(price);

        }


        [HttpPost, ActionName("Create")]
        public ActionResult PriceCreate(Common.Price price)
        {

            if (db.AddPrice(price))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(price);
            }

        }

        public ActionResult Delete(int id)
        {
            var price = db.GetPriceByID(id);
            ViewBag.canDelete = true;
            return View(price);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult PriceDelete(int id)
        {

            if (db.DeletePrice(db.GetPriceByID(id)))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canDelete = false;
                return View(db.GetPriceByID(id));
            }

        }

    }
}
