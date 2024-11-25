using QLKHACHSANWEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKHACHSANWEB.Controllers
{
    public class TraPhongController : Controller
    {
        QLKhachSanContext db =new QLKhachSanContext();
        // GET: TraPhong
        public ActionResult Index()
        {
            List<Phong> phongs = db.Phongs.Where(row=>row.TrangThai==1).ToList();
            List<LoaiPhong> loaiPhongs = db.LoaiPhongs.ToList();
            ViewBag.loai = loaiPhongs;
            return View(phongs);
        }
       
        public ActionResult ChiTietTP(int id)
        {
            Session["idp"] = id;

            List<LoaiPhong> loaiPhongs = db.LoaiPhongs.ToList();
            ViewBag.loai = loaiPhongs;

            Phong nxb = db.Phongs.Where(row => row.PhongId == id).FirstOrDefault();
            ViewBag.giaaa = nxb.GiaPhong;

            ChiTietPhieuDatPhong ctpdp = db.ChiTietPhieuDatPhongs.Where(row => row.PhongId == id).FirstOrDefault();
            ViewBag.ctpdp = ctpdp;

            PhieuDatPhong pdp = db.PhieuDatPhongs.Where(row => row.PhieuDatPhongId == ctpdp.PhieuDatPhongId).FirstOrDefault();
            ViewBag.pdpnd = pdp.NgayDat;

            int soNgayThue = (pdp.NgayTra - pdp.NgayDat).Days;
            ViewBag.SoNgayThue = soNgayThue;

            List<CTDichVu> ctdv = db.CTDichVus.Where(row => row.PhieuDatPhongId == pdp.PhieuDatPhongId).ToList();
            ViewBag.ctdv = ctdv;

            // Tạo danh sách dịch vụ bao gồm tên dịch vụ, giá và số lượng
            var dichVuDetails = (from dv in db.DichVus
                                 join ct in db.CTDichVus on dv.DichVuId equals ct.DichVuId
                                 where ct.PhieuDatPhongId == pdp.PhieuDatPhongId
                                 select new JoinDV
                                 {
                                     DichVuName = dv.DichVuName,
                                     Gia = dv.Gia,
                                     SoLuong = ct.SoLuong
                                 }).ToList();

            ViewBag.DichVuDetails = dichVuDetails;

            double giadv = 0;
            foreach (var item in dichVuDetails)
            {
                giadv += Convert.ToDouble(item.Gia * item.SoLuong);
                string output = $"Dịch vụ: {item.DichVuName}, Giá: {item.Gia}, Số lượng: {item.SoLuong}";
                Console.WriteLine(output); // Chuỗi không còn dấu {}
            }
            ViewBag.giadv = giadv;
            KhachHang kh = db.KhachHangs.Where(row => row.KhachHangId == pdp.KhachHangId).FirstOrDefault();
            ViewBag.khn = kh.KhachHangName;
            ViewBag.cccd = kh.CCCD;

            double gia = Convert.ToDouble(nxb.GiaPhong * soNgayThue) + giadv;
            ViewBag.gia = gia;

            return View(nxb);
        }



        [HttpPost]
        public ActionResult ChiTietTP(Phong phong)
        {
            var idp = Session["idp"] as int?;
             phong=db.Phongs.Where(r=>r.PhongId==idp).FirstOrDefault();
            phong.TrangThai = 2;

            ChiTietPhieuDatPhong ctpdp = db.ChiTietPhieuDatPhongs.Where(r => r.PhongId == idp).FirstOrDefault();

            PhieuDatPhong pdp = db.PhieuDatPhongs.Where(r => r.PhieuDatPhongId == ctpdp.PhieuDatPhongId).FirstOrDefault();

            var item = db.PhieuDatPhongs.Find(pdp.PhieuDatPhongId);  // Tìm item theo id
            if (item != null)
            {
                db.PhieuDatPhongs.Remove(item);      
            }
            List<CTDichVu> ctdv = db.CTDichVus.Where(r => r.PhieuDatPhongId == pdp.PhieuDatPhongId).ToList();

            if (ctdv != null && ctdv.Any())
            {
                foreach (var x in ctdv)
                {
                    db.CTDichVus.Remove(x);  // Xóa từng đối tượng
                }  // Lưu thay đổi
            }

            db.SaveChanges();
             return RedirectToAction("QR");
        }
        public ActionResult QR()
        {

            return View();
        }
    }
}