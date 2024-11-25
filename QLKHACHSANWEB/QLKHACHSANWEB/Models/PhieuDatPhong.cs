using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class PhieuDatPhong
    {
        [Key]
        public int PhieuDatPhongId { get; set; }
        public int KhachHangId { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayTra { get; set; }
        

        public virtual KhachHang KhachHang { get; set; }
        public ICollection<ChiTietPhieuDatPhong> ChiTietPhieuDatPhongs { get; set; }
        public ICollection<CTDichVu> CTDichVus { get; set; }
    }

}

// Phiếu đặt phòng => 1 KH
// CT => Nhiều phòng