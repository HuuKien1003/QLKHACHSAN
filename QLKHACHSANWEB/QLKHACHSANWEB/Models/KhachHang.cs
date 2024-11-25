using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }
        public string KhachHangName { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        
    
         
      
        // Navigation Property
        public ICollection<PhieuDatPhong> PhieuDatPhongs { get; set; }
    }

}