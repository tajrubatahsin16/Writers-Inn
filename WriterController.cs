using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WritersInn1.Models;
using WritersInn1.Repository;

namespace WritersInn.Controllers
{
    public class WriterController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork(); // GET: Writer
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Tbl_category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }



        public ActionResult UpdateCategory(int categoryId = 0)
        {
            CategoryDetail cd;
            if (categoryId != 0)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult Content()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Content>().GetContent());
        }
        public ActionResult ProductEdit(int ContentId)
        {

            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Content>().GetFirstorDefault(ContentId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Content tbl, HttpPostedFileBase file)
        {

            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ContentImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ContentImage = file != null ? pic : tbl.ContentImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Content>().Update(tbl);
            return RedirectToAction("Content");


        }

        public ActionResult ContentAdd()
        {


            ViewBag.CategoryList = GetCategory();

            return View();
        }
        [HttpPost]
        public ActionResult ContentAdd(Tbl_Content tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ContentImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ContentImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Content>().Add(tbl);
            return RedirectToAction("Content");

        }
    }


}