using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class TypeController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllTransportTypes();
            return View(list);
        }


        public ActionResult Create()
        {
            var type = new Common.TransportType();
            ViewBag.canAdd = true;
            return View(type);

        }


        [HttpPost, ActionName("Create")]
        public ActionResult RateCreate(Common.TransportType type)
        {

            if (db.AddType(type))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(type);
            }

        }

        public ActionResult Delete(int id)
        {
            var type = db.GetTypeByID(id);
            ViewBag.canDelete = true;
            return View(type);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult TypeDelete(int id)
        {

            int y = 0;
            if (db.DeleteType(db.GetTypeByID(id)))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(db.GetTypeByID(id));
            }

        }

    }
}
