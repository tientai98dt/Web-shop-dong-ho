using PagedList;
using ShopWatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatches.Dao
{
    public class UserDao
    {
        ShopWatchesEntities1 db = null;
        public UserDao()
        {
            db = new ShopWatchesEntities1();
        }

        public String Insert(TaiKhoan entity)
        {
            db.TaiKhoans.Add(entity);
            db.SaveChanges();
            return entity.TenTaiKhoan;
        }

        public IEnumerable<TaiKhoan> ListAllPaging(string searchString, int page, int pageSize)
        {

            IQueryable<TaiKhoan> model = db.TaiKhoans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTaiKhoan.Contains(searchString) );
            }
            return model.OrderByDescending(x => x.MaTaiKhoan).ToPagedList(page, pageSize);
        }

        public int Login(String UserName, string PassWord, bool isLoginAdmin = false)
        {
            var res = db.TaiKhoans.SingleOrDefault(x => x.TenTaiKhoan == UserName);

            if (res == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (res.GroupID == CommonConstants.ADMIN_GROUP || res.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (res.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (res.MatKhau == PassWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (res.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (res.MatKhau == PassWord)
                            return 1;
                        else
                            return -2;
                    }
                }

            }

        }

        public TaiKhoan GetByID(string userName)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.TenTaiKhoan == userName);
        }

        public TaiKhoan ViewDetail(int id)
        {
            return db.TaiKhoans.Find(id);
        }


        public bool Update(TaiKhoan entity)
        {
            try
            {
                var user = db.TaiKhoans.Find(entity.MaTaiKhoan);
                user.TenTaiKhoan = entity.TenTaiKhoan;
                if (!string.IsNullOrEmpty(entity.MatKhau))
                {
                    user.MatKhau = entity.MatKhau;
                }
                user.MatKhau = entity.MatKhau;
                user.Create = DateTime.Now;
                user.GroupID = entity.GroupID;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}