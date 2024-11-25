using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QLKHACHSANWEB.Models
{
    public class QLKhachSanContext : DbContext
    {
        public QLKhachSanContext() : base("name=QLKhachSan")
        {
        }
        

        public DbSet<Phong> Phongs { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<PhieuDatPhong> PhieuDatPhongs { get; set; }
        public DbSet<ChiTietPhieuDatPhong> ChiTietPhieuDatPhongs { get; set; }
        public DbSet< DichVu> DichVus { get; set; }
        public DbSet < CTDichVu>   CTDichVus { get; set ; } 
        public DbSet <User> users { get; set; }
    }
}