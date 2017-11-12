using AFANDEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFANDEM.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private afandemEntities6 db = new afandemEntities6();
        public ActionResult List()
        {
            List<object> myModel = new List<object>();
            myModel.Add(db.User.ToList());    //Users
            return View(myModel);
        }

        public ActionResult DeleteConfirm(int? id)
        {
            if (Session["UserID"] != null)
            {
                User user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else { return RedirectToAction("../Login/Index"); }
        }

        public ActionResult Edit()
        {
            if (Session["UserID"] != null )
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
        public ActionResult Edit( User user)
        {
            if (Session["UserID"] != null )
            {
                if (ModelState.IsValid)
                {
                    user.Type = 1;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["UserName"] = user.Name;
                    return RedirectToAction("List");
                }
                return View(user);
            }
            else { return RedirectToAction("../Login/Index"); }
        }
    }
}