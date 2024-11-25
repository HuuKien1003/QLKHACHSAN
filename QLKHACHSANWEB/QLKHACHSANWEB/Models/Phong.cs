using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class Phong
    {
        [Key]
        public int PhongId { get; set; }
        public string PhongName { get; set; }
        public int LoaiPhongId { get; set; }
        public int TrangThai { get; set; }
        public decimal GiaPhong { get; set; }

        // Navigation Property
    
        public LoaiPhong LoaiPhong { get; set; }
        public ICollection<ChiTietPhieuDatPhong> ChiTietPhieuDatPhongs { get; set; }
    }

}