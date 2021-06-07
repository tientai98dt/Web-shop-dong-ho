using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatches.Models;

namespace ShopWatches.Controllers
{
    public class QLHangController : Controller
    {
        // GET: QLHang
        private static int mahang;
        public ActionResult Index()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.HangSanXuats.ToList();
                return View(model);
            }

              
        }
        [HttpGet]
        public ActionResult ThemHang()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult ThemHang(HangSanXuat model)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                HangSanXuat hang = new HangSanXuat();
                hang.TenHang = model.TenHang;

                shop.HangSanXuats.Add(hang);
                shop.SaveChanges();
                Response.Redirect("Index");
                return RedirectToAction("Index", model);
            }
            
        }
        [HttpGet]
        public ActionResult SuaHang(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                mahang = id;
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.HangSanXuats.SingleOrDefault(s => s.MaHangSX == id);

                return View(model);
            }

          
        }
        [HttpPost]
        public ActionResult SuaHang(HangSanXuat hangsx)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var hang = shop.HangSanXuats.SingleOrDefault(s => s.MaHangSX == mahang);
                hang.TenHang = hangsx.TenHang;

                shop.SaveChanges();
                var model = shop.HangSanXuats.OrderByDescending(s => s.MaHangSX).ToList();
                return View("Index", model);
            }
           
        }


        public ActionResult XoaHang(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.HangSanXuats.OrderByDescending(s => s.MaHangSX).ToList();

                var hang = shop.HangSanXuats.SingleOrDefault(s => s.MaHangSX == id);
                if (hang != null)
                {
                    shop.HangSanXuats.Remove(hang);
                    shop.SaveChanges();
                }

                return RedirectToAction("Index", model);
            }
           

        }
    }
}