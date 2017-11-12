using AFANDEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFANDEM.Controllers
{
    public class HomeController : Controller
    {
        private afandemEntities6 db = new afandemEntities6();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            if (Session["UserID"] != null)
            {
                var x = Convert.ToInt32(Session["UserID"]);
                User user = db.User.Find(x);
                return View(user);
            }
            else { return RedirectToAction("../Login/Index"); }
        }

        // POST: /Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(User user)
        {
            if (Session["UserID"] != null )
            {
                if (ModelState.IsValid)
                {
                    user.Type = Convert.ToInt32(Session["UserType"]);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["UserName"] = user.Name;
                    return RedirectToAction("Profile");
                }
                return View(user);
            }
            else { return RedirectToAction("../Login/Index"); }
        }
    }
}