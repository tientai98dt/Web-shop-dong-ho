using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopWatches.Models;

namespace ShopWatches.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ShopWatchesEntities1 shop=new ShopWatchesEntities1();
            var model = shop.HangSanXuats.OrderByDescending(h => h.MaHangSX).ToList().Skip(0).Take(5).ToList();
            
            return View(model);
        }

        public ActionResult ChiTietSp(int id)
        {
            ShopWatchesEntities1 shop=new ShopWatchesEntities1();
            var model = shop.SanPhams.SingleOrDefault(s => s.MaSP == id);
            var list = shop.SanPhams.Where(s => s.MaHangSX == model.MaHangSX).ToList();
            var hang= shop.HangSanXuats.SingleOrDefault(h => h.MaHangSX == model.MaHangSX);
            var listhang = shop.HangSanXuats.ToList();
            listhang.Remove(hang);
            list.Remove(model);
            ViewBag.LienQuan = list.ToList().Skip(0).Take(6).ToList();
            ViewBag.Hang = listhang.ToList();
            return View(model);
        }

        public ActionResult TinTuc(int?page)
        {
            IPagedList<TinTuc> model = DsTin(page);
            return View(model);
        }

        public IPagedList<TinTuc> DsTin(int? page)
        {
            ShopWatchesEntities1 shop = new ShopWatchesEntities1();
            var tin = shop.TinTucs.OrderByDescending(t => t.NgayDang);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //tin.ToPagedList(pageNumber, pageSize);
            return tin.ToPagedList(pageNumber, pageSize);
        } 
        public ActionResult ChiTietTinTuc(int id)
        {
            ShopWatchesEntities1 shop=new ShopWatchesEntities1();
            var model = shop.TinTucs.Where(t => t.MaTin == id).SingleOrDefault();
           
            return View(model);
        }
        public ActionResult ChuyenMuc()
        {
            ShopWatchesEntities1 shop=new ShopWatchesEntities1();
            var model = shop.HangSanXuats.ToList();
            return View(model);
        }

        public ActionResult ChiTietChuyenMuc(int id)
        {
            ShopWatchesEntities1 shop = new ShopWatchesEntities1();
            var model = shop.SanPhams.Where(s=>s.MaHangSX==id).ToList();
            ViewBag.TenChuyenMuc = model[0].HangSanXuat.TenHang;
            return View(model);
        }

        public ActionResult TimKiem(string search,int?page)
        {
            ViewBag.KQ = search;
            var model = DanhSachTimKiem(search, page);
            return View(model);
        }

        public IPagedList<SanPham> DanhSachTimKiem(string search,int?page)

        {
            ShopWatchesEntities1 shop = new ShopWatchesEntities1();
            var model = shop.SanPhams.Where(s => s.TenSP.ToLower().Contains(search.ToLower())).OrderByDescending(c=>c.MaSP);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            model.ToPagedList(pageNumber, pageSize);
            return model.ToPagedList(pageNumber, pageSize);
        } 
    }
}