using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WritersInn1.Models;

namespace WritersInn1.Controllers
{
    public class ContentsCustomerController : Controller
    {
        WriterEntities dc2 = new WriterEntities();
        List<Cart> li = new List<Cart>();
        // GET: ContentsCustomer
        public ActionResult ContentsViewCustomer()
        { 
            if (TempData["cart"] != null)
            {
                int x = 0;

                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["total"] = x;
                TempData["item_count"] = li2.Count();
            }
            TempData.Keep();

            var fetch = dc2.getAllContents.ToList();
                return View(fetch);
        }


        public ActionResult AddtoCart(int id) 
        {
            var query= dc2.getAllContents.Where(x=> x.CID== id).SingleOrDefault();
            return View(query); 
        }

        [HttpPost]
        public ActionResult AddtoCart(int id, int qty)
        {
            getAllContent p = dc2.getAllContents.Where(x => x.CID == id).SingleOrDefault();
            Cart c = new Cart();
            c.proid = id;
            c.proname = p.CName;
            c.price = Convert.ToInt32(p.CPrice);
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.proid == c.proid)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }

                }
                if (flag == 0)
                {
                    li2.Add(c);
                    //new item
                }
                TempData["cart"] = li2;

            }

            TempData.Keep();

            return RedirectToAction("Checkout");
        }


        public ActionResult Checkout()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(string ins)
        {
            if (ModelState.IsValid)
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                // tblInvoice iv = new tblInvoice();
                // iv.UserId = Convert.ToInt32(Session["uid"].ToString());
                // iv.InvoiceDate = System.DateTime.Now;
                // iv.Bill = (int)TempData["total"];
                // iv.Payment = "cash";
                // db.tblInvoices.Add(iv);
                // db.SaveChanges();
                //order book
                foreach (var item in li2)
                {
                    ORDER od = new ORDER();
                    od.Content_Id = item.proid;
                    od.Quantity = item.qty;
                    od.Customer_Id = 1000;
                    od.Total_Cost = item.bill;
                    od.Order_Status = "Ordered";
                    dc2.ORDERS.Add(od);
                    dc2.SaveChanges();

                }
                TempData.Remove("total");
                TempData.Remove("cart");
                // TempData["msg"] = "Order Book Successfully!!";
                return RedirectToAction("CusIndex","CustomerHome");
            }
            TempData.Keep();
            return View();
        }




        public ActionResult remove(int? id)
        {
            if (TempData["cart"] == null)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                Cart c = li2.Where(x => x.proid == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.bill;
                }
                TempData["total"] = s;

            }

            return RedirectToAction("Checkout");
        }
    }
}
