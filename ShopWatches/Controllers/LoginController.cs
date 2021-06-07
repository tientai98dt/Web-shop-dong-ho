using ShopWatches.App_Start;
using ShopWatches.Common;
using ShopWatches.Dao;
using ShopWatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatches.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var res = dao.Login(model.UserName, model.PassWord, true);
                if (res == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    Session["MaTk"] = user.MaTaiKhoan;
                    Session["TenTk"] = user.TenTaiKhoan;
                    Session["GroupID"] = user.GroupID;
                    return Redirect("/QLHoaDon/Index");

                   
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (res == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View("Index");

        }

        public ActionResult DangXuat()
        {
            Session["MaTk"] = null;
            Session["TenTk"] = null;
            //return View("Index");
            return Redirect("/Home/Index");
        }
    }
}