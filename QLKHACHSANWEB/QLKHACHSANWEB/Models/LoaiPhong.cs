using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class LoaiPhong
    {
        [Key]
        public int LoaiPhongId { get; set; }
        public string TenLoai { get; set; }

        // Navigation Property
        public ICollection<Phong> Phongs { get; set; }
    }

}