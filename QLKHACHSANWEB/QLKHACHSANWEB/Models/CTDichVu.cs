using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class CTDichVu
    {
       public int CTDichVuId { get; set; }
    public int DichVuId { get; set; }
       public int PhieuDatPhongId { get; set; }
        public int SoLuong {  get; set; }
        public virtual PhieuDatPhong PhieuDatPhong { get; set; }
        public virtual DichVu DichVu { get; set; }

    }
}