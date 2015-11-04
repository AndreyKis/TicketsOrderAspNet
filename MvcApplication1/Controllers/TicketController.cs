using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesToTicketsDB;
using Common;

namespace MvcApplication1.Controllers
{
    public class TicketController : Controller
    {
        //
        // GET: /Order/
        AccessToTicketsDB db = new AccessToTicketsDB();

        public ActionResult Index()
        {
            var list = db.GetAllTickets();
            return View(list);
        }


        public ActionResult Create()
        {
            var ticket = new Common.Ticket();
            ViewBag.List = db.GetAllPrices();
            ViewBag.canAdd = true;
            return View(ticket);

        }


        [HttpPost, ActionName("Create")]
        public ActionResult TicketCreate(Common.Ticket ticket)
        {

            if (db.AddTicket(ticket, db.GetPriceIDByName(ticket.Price.ToString()), db.GetRateIDByName(ticket.Rate), 
                db.GetTypeIDByName(ticket.Type)) == 0)
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(ticket);
            }

        }

        public ActionResult Delete(int id)
        {
            var ticket = db.GetTicketByID(id);
            ViewBag.canDelete = true;
            return View(ticket);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult TicketDelete(int id)
        {

            if (db.DeleteTicket(db.GetTicketByID(id)))
            {

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.canAdd = false;
                return View(db.GetTicketByID(id));
            }

        }

    }
}
