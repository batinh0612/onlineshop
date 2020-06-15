using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();

            var productDao = new ProductDao();
            ViewBag.ListNewProduct = productDao.ListNewProduct(4);
            ViewBag.ListFeatureProduct = productDao.ListFeatureProduct(4);

            //set title
            ViewBag.Title = ConfigurationManager.AppSettings["HomeTitle"];
            ViewBag.Keywords = ConfigurationManager.AppSettings["HomeKeywords"];
            ViewBag.Decrestion = ConfigurationManager.AppSettings["HomeDecrestion"];

            return View();
        }

        [ChildActionOnly]
        //childactiononly thì không được có location, varyparma, không dùng được cách 2 trong webconfig
        [OutputCache(Duration = 3600)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        //Cache cách thông thường, cách 1
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCartPartial()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        [ChildActionOnly]
        //Cache cách thông thường, cách 1
        [OutputCache(Duration = 3600 * 24)]
        //[OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}