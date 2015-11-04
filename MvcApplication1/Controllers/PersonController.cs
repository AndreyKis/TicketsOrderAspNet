using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllPersons();
            return View(list);
        }


        public ActionResult Create()
        {
            var person = new Common.Person();
            ViewBag.canAdd = true;
            return View(person);

        }


        [HttpPost, ActionName("Create")]
        public ActionResult PersonCreate(Common.Person person)
        {

            if (db.AddPerson(person) == 0)
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(person);
            }

        }

        public ActionResult Delete(int id)
        {
            var person = db.GetPersonByID(id);
            ViewBag.canDelete = true;
            return View(person);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult PersonDelete(int id)
        {

            if (db.DeletePerson(db.GetPersonByID(id)))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(db.GetPersonByID(id));
            }

        }

    }
}
