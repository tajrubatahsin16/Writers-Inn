using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WritersInn1.Models;

namespace WritersInn1.Controllers
{
    public class WriterListController : Controller
    {
        WriterEntities dc1 = new WriterEntities();
        List<Cart> li = new List<Cart>();
        // GET: WriterList
        public ActionResult WriterListIndex()
        {
            var fetch = dc1.WRITERs;
            return View(fetch);
        }



    }
}