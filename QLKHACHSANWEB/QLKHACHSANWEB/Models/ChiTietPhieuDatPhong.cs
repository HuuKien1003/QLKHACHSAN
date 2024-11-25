using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class ChiTietPhieuDatPhong
    {
        public int ChiTietPhieuDatPhongId { get; set; }
        public int PhieuDatPhongId { get; set; }
        public int PhongId { get; set; }
        
        // Navigation Property
        public PhieuDatPhong PhieuDatPhong { get; set; }
        public Phong Phong { get; set; }
    }
}