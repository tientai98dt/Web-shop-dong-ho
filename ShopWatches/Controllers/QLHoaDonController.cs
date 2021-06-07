using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWatches.Models;

namespace ShopWatches.Controllers
{
    public class QLHoaDonController : Controller
    {
        // GET: QLHoaDon
     
        private static int mahd;
        public ActionResult Index()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model=new DonHangViewModel()
                {
                    DonHangDaDuyet = shop.HoaDons.Where(h => h.TrangThai == true).ToList(),
                    DonHangChuaDuyet = shop.HoaDons.Where(h => h.TrangThai == false).ToList()
            };
                
                return View(model);
            }
           
        }
        [HttpGet]
        public ActionResult ThemHd()
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
        public ActionResult ThemHd(HoaDon model)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                HoaDon hd = new HoaDon();
                hd.TenKhachHang = model.TenKhachHang;
                hd.DiaChi = model.DiaChi;
                hd.Email = model.Email;
                hd.SDT = model.SDT;
                hd.DiaChiGiaoHang = model.DiaChiGiaoHang;
                hd.ThoiGianGiaoHang = model.ThoiGianGiaoHang;
                hd.TongTien = 0;
                shop.HoaDons.Add(hd);
                shop.SaveChanges();
                Response.Redirect("Index");
                return View("Index");
            }
          
        }
        [HttpGet]
        public ActionResult SuaHd(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                mahd = id;
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.HoaDons.SingleOrDefault(s => s.MaHoaDon == id);

                return View(model);
            }
          
        }
        [HttpPost]
        public ActionResult SuaHd(HoaDon hoaDon)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var hd = shop.HoaDons.SingleOrDefault(s => s.MaHoaDon == mahd);
                hd.TenKhachHang = hoaDon.TenKhachHang;
                hd.DiaChi = hoaDon.DiaChi;
                hd.Email = hoaDon.Email;
                hd.SDT = hoaDon.SDT;
                hd.DiaChiGiaoHang = hoaDon.DiaChiGiaoHang;
                hd.ThoiGianGiaoHang = hoaDon.ThoiGianGiaoHang;
                shop.SaveChanges();
                var model = shop.HoaDons.OrderByDescending(s => s.MaHoaDon).ToList();
                return View("Index", model);
            }
           
        }


        public ActionResult XoaHd(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = new DonHangViewModel()
                {
                    DonHangDaDuyet = shop.HoaDons.Where(h => h.TrangThai == true).ToList(),
                    DonHangChuaDuyet = shop.HoaDons.Where(h => h.TrangThai == false).ToList()
                };


                var hd = shop.HoaDons.SingleOrDefault(s => s.MaHoaDon == id);
                if (hd != null)
                {
                    foreach (var item in shop.ChiTietHoaDons.Where(c => c.MaHoaDon == id).ToList())
                    {
                        var sp = shop.SanPhams.SingleOrDefault(s => s.MaSP == item.MaSanPham);
                        sp.SoLuong += item.SoLuong;
                        shop.ChiTietHoaDons.Remove(item);
                    }
                    shop.HoaDons.Remove(hd);
                    shop.SaveChanges();
                }

                return RedirectToAction("Index", model);
            }
           

        }

        public ActionResult ChiTietHd(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                mahd = id;
                ViewBag.mahd = id;
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.ChiTietHoaDons.Where(c => c.MaHoaDon == mahd).OrderByDescending(c => c.ChiTietHoaDon1).ToList();
                List<SelectListItem> sanPham = new List<SelectListItem>();
                for (int i = 0; i < shop.SanPhams.ToList().Count; i++)
                {
                    SelectListItem sl = new SelectListItem() { Text = shop.SanPhams.ToList()[i].TenSP, Value = shop.SanPhams.ToList()[i].MaSP.ToString() };
                    sanPham.Add(sl);
                }
                ViewBag.Sp = sanPham;
                return View(model);
            }
           
        }
        [HttpPost]
        public ActionResult ThemChiTietHd(int SanPham,int SoLuong)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var sp = shop.SanPhams.SingleOrDefault(s => s.MaSP == SanPham);
                var hd = shop.HoaDons.SingleOrDefault(h => h.MaHoaDon == mahd);
                ChiTietHoaDon ct = new ChiTietHoaDon();
                ct.MaHoaDon = mahd;
                ct.MaSanPham = SanPham;
                ct.SoLuong = SoLuong;
                ct.TongTien = SoLuong * sp.GiaBan;
                shop.ChiTietHoaDons.Add(ct);
                hd.TongTien += ct.TongTien;
                shop.SaveChanges();
                return RedirectToAction("ChiTietHd", new { id = mahd });
            }
           
        }
        public ActionResult XoaChiTietHd(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();

                var ct = shop.ChiTietHoaDons.SingleOrDefault(s => s.ChiTietHoaDon1 == id);
                var hd = shop.HoaDons.SingleOrDefault(h => h.MaHoaDon == mahd);
                if (ct != null)
                {
                    shop.ChiTietHoaDons.Remove(ct);
                    hd.TongTien -= ct.TongTien;
                    shop.SaveChanges();
                }

                return RedirectToAction("ChiTietHd", new { id = mahd });

            }

        }
        //[HttpPost]
        //public ActionResult ThemChiTietHd(ChiTietHoaDon model)
        //{
        //    ShopWatchesEntities1 shop=new ShopWatchesEntities1();

        //    return View();
        //}
        [HttpPost]
        public ActionResult SuaChiTietHd(int id, int soLuong)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var ct = shop.ChiTietHoaDons.SingleOrDefault(c => c.ChiTietHoaDon1 == id);

                ct.HoaDon.TongTien += (soLuong * ct.SanPham.GiaBan) - ct.TongTien;
                ct.SoLuong = soLuong;
                ct.TongTien = soLuong * ct.SanPham.GiaBan;
                shop.SaveChanges();
                return this.Json(new { data = true }, JsonRequestBehavior.AllowGet); ;
            }
           
        }
        public ActionResult XacNhanDonHang()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = shop.HoaDons.Where(h => h.TrangThai == false).ToList();
                return View(model);
            }
            
        }

        public ActionResult Duyet(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ShopWatchesEntities1 shop = new ShopWatchesEntities1();
                var model = new DonHangViewModel()
                {
                    DonHangDaDuyet = shop.HoaDons.Where(h => h.TrangThai == true).ToList(),
                    DonHangChuaDuyet = shop.HoaDons.Where(h => h.TrangThai == false).ToList()
                };


                var hd = shop.HoaDons.SingleOrDefault(h => h.MaHoaDon == id);
                hd.TrangThai = true;

                var listChiTiet = shop.ChiTietHoaDons.Where(c => c.MaHoaDon == id).ToList();
                foreach (var item in listChiTiet)
                {
                    var sp = shop.SanPhams.SingleOrDefault(s => s.MaSP == item.MaSanPham);
                    if (sp.SoLuong > item.SoLuong)
                    {
                        sp.SoLuong -= item.SoLuong;
                    }

                }
                shop.SaveChanges();
                return RedirectToAction("Index", model);
            }
            
        }
    }
}