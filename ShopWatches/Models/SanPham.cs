//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopWatches.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
    
        public int MaSP { get; set; }
        public Nullable<int> MaHangSX { get; set; }
        public string TenSP { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
        public string Anh { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string MoTa { get; set; }
        public string ChiTiet { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual HangSanXuat HangSanXuat { get; set; }
    }
}
