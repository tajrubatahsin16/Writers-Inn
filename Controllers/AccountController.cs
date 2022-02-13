using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WritersInn1.Models;
using WritersInn1.ViewModel;

namespace WritersInn1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {
            
            if (ModelState.IsValid)
            {
                
                using (var databaseContext = new WriterEntities())
                {
                    
                    WRITER reglog = new WRITER();

                    

                    reglog.WriterName = registerDetails.Name;
                    reglog.WriterEmail = registerDetails.Email;
                    reglog.WriterPass = registerDetails.Password;
                    reglog.WriterPhone = registerDetails.Phone;


                    
                    databaseContext.WRITERs.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "Writer Details Saved";
                return RedirectToAction("Login");
            }
            else
            {

                
                return View("Register", registerDetails);
            }
        }


        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {

                
                var isValidUser = IsValidUser(model);

                
                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    TempData["WriterName"] = isValidUser.WriterName;
                    return RedirectToAction("Success");
                }
                else
                {
                    
                    ModelState.AddModelError("Failure", "Wrong User email and password combination !");
                    return View();
                }
            }
            else
            {
                
                return View(model);
            }
        }

        
        public WRITER IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new WriterEntities())
            {
                
                WRITER user = dataContext.WRITERs.Where(query => query.WriterEmail.Equals(model.Email) && query.WriterPass.Equals(model.Password)).SingleOrDefault();
                
                if (user == null)
                    return null;
                
                else
                    return user;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); 
            return RedirectToAction("LoginOption","Home");
        }
    }
}