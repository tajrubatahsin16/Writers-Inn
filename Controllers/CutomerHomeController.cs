using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WritersInn1.Controllers
{
    public class CustomerHomeController : Controller
    {
        // GET: CustomerHome
        [Authorize]
        public ActionResult CusIndex()
        {
            return View();
        }
    }
}