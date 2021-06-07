using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatches.Models;

namespace ShopWatches.Controllers
{
   
    public class QLSanPhamController : Controller
    {
        private static int masp;
        // GET: QLSanPham
        public ActionResult Index()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.SanPhams.OrderByDescending(s => s.MaSP).ToList();
                return View(model);
            }
          
        }
        [HttpGet]
        public ActionResult ThemSp()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var listHang = shop.HangSanXuats.OrderByDescending(p => p.MaHangSX).ToList();
                List<SelectListItem> slSanPham = listHang.Select(t => new SelectListItem() { Text = t.TenHang, Value = t.MaHangSX.ToString() }).ToList();

                ViewBag.Hang = slSanPham;
                return View();
            }

           
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSp(SanPham model, HttpPostedFileBase file)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                file = file ?? Request.Files["file"];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/images/products/"), fileName);
                        file.SaveAs(path);
                        SanPham sp = new SanPham();
                        sp.MaHangSX = model.MaHangSX;
                        sp.TenSP = model.TenSP;
                        sp.Anh = "/images/products/" + fileName;
                        sp.GiaBan = model.GiaBan;
                        sp.GiaNhap = model.GiaNhap;
                        sp.MoTa = model.MoTa;
                        sp.ChiTiet = model.ChiTiet;
                        sp.SoLuong = model.SoLuong;
                        sp.NgayDang = DateTime.Now;
                        shop.SanPhams.Add(sp);
                        shop.SaveChanges();
                        Response.Redirect("Index");
                    }
                }
                else
                {
                    SanPham sp = new SanPham();
                    sp.MaHangSX = model.MaHangSX;
                    sp.TenSP = model.TenSP;
                    sp.Anh = "/images/p-8.png";
                    sp.GiaBan = model.GiaBan;
                    sp.GiaNhap = model.GiaNhap;
                    sp.MoTa = model.MoTa;
                    sp.SoLuong = model.SoLuong;
                    sp.ChiTiet = model.ChiTiet;
                    sp.NgayDang = DateTime.Now;
                    shop.SanPhams.Add(sp);
                    shop.SaveChanges();
                    Response.Redirect("Index");
                }


                return RedirectToAction("Index", model);
            }
            
        }
        [HttpGet]
        public ActionResult SuaSp(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                masp = id;
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.SanPhams.SingleOrDefault(s => s.MaSP == id);
                var listHang = shop.HangSanXuats.OrderByDescending(p => p.MaHangSX).ToList();
                List<SelectListItem> slSanPham = listHang.Select(t => new SelectListItem() { Text = t.TenHang, Value = t.MaHangSX.ToString() }).ToList();

                ViewBag.Hang = slSanPham;
                return View(model);
            }
          
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSp(SanPham sp,string img,HttpPostedFileBase file)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                file = file ?? Request.Files["file"];

                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var sanpham = shop.SanPhams.SingleOrDefault(s => s.MaSP == masp);
                sanpham.TenSP = sp.TenSP;
                sanpham.MaHangSX = sp.MaHangSX;
                sanpham.GiaNhap = sp.GiaNhap;
                sanpham.GiaBan = sp.GiaBan;
                sanpham.SoLuong = sp.SoLuong;
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/images/products/"), fileName);
                        file.SaveAs(path);
                        sanpham.Anh = "/images/products/" + fileName;
                    }
                }
                else
                {
                    sanpham.Anh = img;
                }
                sanpham.MoTa = sp.MoTa;
                sanpham.ChiTiet = sp.ChiTiet;
                shop.SaveChanges();

                var model = shop.SanPhams.OrderByDescending(s => s.MaSP).ToList();
                return View("Index", model);
            }
            
        }

     
        public ActionResult XoaSp(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.SanPhams.OrderByDescending(s => s.MaSP).ToList();

                var sanpham = shop.SanPhams.SingleOrDefault(s => s.MaSP == id);
                if (sanpham != null)
                {
                    shop.SanPhams.Remove(sanpham);
                    shop.SaveChanges();
                }

                return RedirectToAction("Index", model);
            }
           

        }
    }
}