using AFANDEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFANDEM.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Callender

        public ActionResult MyCalendar()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else { return RedirectToAction("../Login/Index"); }
        }

        public ActionResult Calendar()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else { return RedirectToAction("../Login/Index"); }
        }

        public JsonResult GetEvents()
        {
            using (afandemEntities6 dc = new afandemEntities6())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Events e)
        {
            var status = false;
            using (afandemEntities6 dc = new afandemEntities6())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (afandemEntities6 dc = new afandemEntities6())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
