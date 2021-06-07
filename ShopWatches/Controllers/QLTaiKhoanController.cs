using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatches.Dao;
using ShopWatches.Models;

namespace ShopWatches.Controllers
{
    public class QLTaiKhoanController : Controller
    {
        // GET: QLTaiKhoan
        private static int matk;
        public ActionResult Index()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.TaiKhoans.ToList();
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult ThemTk()
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
        public ActionResult ThemTk(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var id = dao.Insert(model);
                var db = new ShopWatchesEntities1();
                    if (id == null)
                    {
                        return RedirectToAction("Index", "QLTaiKhoan");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Sản phẩm thành công");
                    }
                }
            return RedirectToAction("Index", model);

        }
        [HttpGet]
        public ActionResult SuaTk(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                matk = id;
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.TaiKhoans.SingleOrDefault(s => s.MaTaiKhoan == id);

                return View(model);
            }
           
        }
        [HttpPost]
        public ActionResult SuaTk(TaiKhoan taiKhoan)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var tk = shop.TaiKhoans.SingleOrDefault(s => s.MaTaiKhoan == matk);
                tk.TenTaiKhoan = taiKhoan.TenTaiKhoan;
                tk.MatKhau = taiKhoan.MatKhau;
                tk.GroupID = taiKhoan.GroupID;
                tk.Status = taiKhoan.Status;
                shop.SaveChanges();
                var model = shop.TaiKhoans.OrderByDescending(s => s.MaTaiKhoan).ToList();
                return RedirectToAction("Index", model);
            }
           
        }


        public ActionResult XoaTk(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.TaiKhoans.OrderByDescending(s => s.MaTaiKhoan).ToList();

                var tk = shop.TaiKhoans.SingleOrDefault(s => s.MaTaiKhoan == id);
                if (tk != null)
                {
                    shop.TaiKhoans.Remove(tk);
                    shop.SaveChanges();
                }

                return RedirectToAction("Index", model);
            }
           

        }
    }
}