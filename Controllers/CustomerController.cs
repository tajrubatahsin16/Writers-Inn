using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WritersInn1.Models;

namespace WritersInn1.Controllers
{
    public class CustomerController : Controller
    {

        //Registration Action
        public ActionResult CusRegister()
        {
            return View();
        }

        //Registration POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CusRegister(CUSTOMER U)
        {
            if (ModelState.IsValid)
            {
                using (WriterEntities dc = new WriterEntities())
                {
                    dc.CUSTOMERs.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Successful";
                    return RedirectToAction("CusLogin", "Customer");
                }
            }
            return View(U);

        }

        //Login
        [HttpGet]
        public ActionResult CusLogin()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CusLogin(CustomerLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (WriterEntities dc = new WriterEntities())
            {
                var v = dc.CUSTOMERs.Where(a => a.Cus_Email == login.EmailID && a.Cus_Password == login.Password).FirstOrDefault();
                if (v != null)
                {
                    //RedirectToAction("Index", "CustomerHome");

                    int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);


                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("CusIndex", "CustomerHome");
                    }
                }


                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult CusLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("CusLogin", "Customer");
        }
    }
}