using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AFANDEM.Models;
using System.Data.Entity;

namespace AFANDEM.Controllers
{
    public class RequestController : Controller
    {
        private afandemEntities6 db = new afandemEntities6();
        public ActionResult AddRequest()
        {
            if (Session["UserID"] != null)
            {
                var x = Convert.ToInt32(Session["UserID"]);
                User user = db.User.Find(x);
                ViewBag.user = user;
                ViewBag.reqdate = DateTime.Now;
                return View();
            }
            else { return RedirectToAction("../Login/Index"); }
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRequest(Request request)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    
                    request.EmpId = Convert.ToInt32(Session["UserID"]);
                    request.RequestDate = DateTime.Now;
                    request.State = 1;
                    if(request.Loan != null)
                    {
                        request.Type = "Loan";
                    }else if(request.Start != null && request.End != null)
                    {
                        request.Type = "Vacation";
                    }
                    else
                    {
                        request.Type = "Open Request";
                    }
                    db.Request.Add(request);
                    db.SaveChanges();
                    
                    return RedirectToAction("SubmittedRequest");
                }
                return View(request);
            }
            else { return RedirectToAction("../Login/Index"); }
        }
        // GET: Request
   
        public ActionResult EmployeeRequest()
        {
            ViewBag.allreq = db.Request.OrderByDescending(o => o.RequestDate).Where(o => o.State != 1).ToList();
            return View();
        }
        public ActionResult PinddingRequest()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.req = db.Request.OrderByDescending(o => o.RequestDate).Where(o=> o.State ==1 ).ToList();
                return View();
            }
            else { return RedirectToAction("../Login/Index"); }
        }
   
        public ActionResult accept(int id)
        {
            var x = db.Request.Find(id);
            x.State = 2;
            db.Entry(x).State = EntityState.Modified;
           
            db.SaveChanges();
            return RedirectToAction("PinddingRequest");
        }
        public ActionResult reject(int id)
        {
            var x = db.Request.Find(id);
            x.State = 3;
            db.Entry(x).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("PinddingRequest");
        }
        public ActionResult SubmittedRequest()
        {
            ViewBag.myreq = db.Request.OrderByDescending(o => o.RequestDate).ToList();
            return View();
        }
    }
}