﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopWatchesEntities1 : DbContext
    {
        public ShopWatchesEntities1()
            : base("name=ShopWatchesEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        //public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
    }
}