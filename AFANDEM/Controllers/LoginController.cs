using AFANDEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFANDEM.Controllers
{
    public class LoginController : Controller
    {
        private afandemEntities6 db = new afandemEntities6();
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return View();
            }
            return RedirectToAction("../Home/Profile");
        }
        [HttpPost]
        public ActionResult Index(User user)
        {

            var usr = db.User.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                Session["UserType"] = usr.Type.ToString();

                ModelState.AddModelError("", "Login Faild !!!");

                return RedirectToAction("../Home/Profile");


            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wronge");
            }

            return View();
        }
    
}
}