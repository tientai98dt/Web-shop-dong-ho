using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatches.App_Start
{
    [Serializable]
    public class UserLogin
    {
        public long MaTaiKhoan { set; get; }
        public string TenTaiKhoan { set; get; }
        public string GroupID { set; get; }
    }
}