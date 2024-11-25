using QLKHACHSANWEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace QLKHACHSANWEB.Controllers
{
    public class HomeController : Controller
    {
        QLKhachSanContext db = new QLKhachSanContext();
        // GET: Home
        [HttpGet]
        public ActionResult Index(string strSearch="", string sr="")
        {List<Phong> phongs = db.Phongs.ToList();
            ViewBag.phongs = phongs;
            List<LoaiPhong> loaiPhongs = db.LoaiPhongs.ToList();
            ViewBag.loai = loaiPhongs;
            ViewBag.Search = strSearch;
             if (!string.IsNullOrEmpty(strSearch))
           {
                var kq = db.Phongs.Where(r => r.LoaiPhong.TenLoai.ToString().Contains(strSearch)).ToList();
                             
                return View(kq);
                
              }
            if (!string.IsNullOrEmpty(sr))
            {
                var kq = db.Phongs.Where(r => r.TrangThai.ToString().Contains(sr)).ToList();
                return View(kq);
            }
            
            return View(phongs);
        }
        public ActionResult ChiTiet(int id)
        {
            Phong nxb = db.Phongs.Where(row => row.PhongId == id).FirstOrDefault();
            LoaiPhong loai = db.LoaiPhongs.Where(r => r.LoaiPhongId== nxb.LoaiPhongId).FirstOrDefault();
            ViewBag.loai= loai.TenLoai;
            Session["UserId"] = id;
            return View(nxb);
        }
        [HttpPost]
        public ActionResult ChiTiet(KhachHang k ,Phong p)
        {
            int? id = Session["UserId"] as int?;
            // Tạo khách hàng
            // Tạo phiếu đặt
            Phong phong = db.Phongs.Where(row => row.PhongId ==id ).FirstOrDefault();
            phong.TrangThai = 1;

            db.KhachHangs.Add(k);
           
            
            //db.PhieuDatPhongs.Add(pdp);
            db.SaveChanges();
            

            return RedirectToAction("PhieuDatPhong", new { id = k.KhachHangId });
        }
        public ActionResult  OrderThem (int id)
        {
            ChiTietPhieuDatPhong ctpdp = db.ChiTietPhieuDatPhongs.Where(row=>row.PhongId == id).FirstOrDefault();
            Phong p = db.Phongs.Where(row => row.PhongId == id).FirstOrDefault();
            ViewBag.pname = p.PhongName;
            List<DichVu> dv = db.DichVus.ToList();
            ViewBag.DichVus = dv;
            PhieuDatPhong pdp =db.PhieuDatPhongs.Where(row=>row.PhieuDatPhongId==ctpdp.PhieuDatPhongId).FirstOrDefault();
            ViewBag.pdpid=pdp.PhieuDatPhongId;           
            return View();
        }
        [HttpPost]
        public ActionResult OrderThem(PhieuDatPhong pdp, string[] DichVuId, string[] SoLuong)
        {
            if (DichVuId == null)
            {
                return RedirectToAction("XacNhanOder", "Home");
            }
            if (DichVuId != null)
            {
                for (int i = 0; i < DichVuId.Length; i++)
                {
                    if (!string.IsNullOrEmpty(DichVuId[i])) // Chỉ lưu những dịch vụ được chọn
                    {
                        CTDichVu ctdv = new CTDichVu
                        {
                            PhieuDatPhongId = pdp.PhieuDatPhongId,
                            DichVuId = Convert.ToInt32(DichVuId[i]),
                            SoLuong = Convert.ToInt32(SoLuong[i])
                        };
                        db.CTDichVus.Add(ctdv);
                    }
                }
            }
            db.SaveChanges(); // Lưu tất cả chi tiết dịch vụ
            return RedirectToAction("XacNhanOder", "Home");

        }
        public ActionResult PhieuDatPhong(int id)
        {
         var khachHang = db.KhachHangs.Find(id);
            Session["makh"] = id;
            ViewBag.maKH=id;

          KhachHang Khachang=db.KhachHangs.Where(row => row.KhachHangId == id).FirstOrDefault();
            ViewBag.tenKH = Khachang.KhachHangName;
            ViewBag.sdt = Khachang.SDT;
            List<DichVu> dv = db.DichVus.ToList();
            ViewBag.DichVus = dv;
            return View(khachHang);
        }
        [HttpPost]
        public ActionResult PhieuDatPhong(PhieuDatPhong pdp, string SoNgay, string[] DichVuId, string[] SoLuong)
        {
            // Xử lý dữ liệu đặt phòng chính
            int SoNgayThue = Convert.ToInt16(SoNgay);
            DateTime dateNgayDat = DateTime.Today;
            DateTime dateNgayTra = dateNgayDat.AddDays(SoNgayThue);
            pdp.NgayTra = dateNgayTra;

            db.PhieuDatPhongs.Add(pdp);
            db.SaveChanges(); // Lưu trước để có pdp.PhieuDatPhongId
            if (DichVuId == null)
            {

            }
            if (DichVuId != null) { 
            // Xử lý chi tiết dịch vụ
            for (int i = 0; i < DichVuId.Length; i++)
            {
                if (!string.IsNullOrEmpty(DichVuId[i])) // Chỉ lưu những dịch vụ được chọn
                {
                    CTDichVu ctdv = new CTDichVu
                    {
                        PhieuDatPhongId = pdp.PhieuDatPhongId,
                        DichVuId = Convert.ToInt32(DichVuId[i]),
                        SoLuong = Convert.ToInt32(SoLuong[i])
                    };
                    db.CTDichVus.Add(ctdv);
                }
            }
            }

            db.SaveChanges(); // Lưu tất cả chi tiết dịch vụ

            return RedirectToAction("CTPhieuDatPhong", new { pdid = pdp.PhieuDatPhongId });
        }
        public ActionResult CTPhieuDatPhong(int pdid)
        {
            ViewBag.id = pdid;
            Session["pdpid"] = pdid;
            var id = Session["UserId"] as int?;
            ViewBag.pid = id;
              Phong p=db.Phongs.Where(row => row.PhongId == id).FirstOrDefault();
            var tl = p.LoaiPhongId;
            LoaiPhong lp = db.LoaiPhongs.Where(r => r.LoaiPhongId == tl).FirstOrDefault();
            ViewBag.LP = lp.TenLoai;
            var mkh = Session["makh"] as int?;
            KhachHang kh =db.KhachHangs.Where(n=>n.KhachHangId==mkh).FirstOrDefault();
            ViewBag.tkh = kh.KhachHangName;
            ViewBag.CCCD = kh.CCCD;
            ViewBag.SDT = kh.SDT;

            return View(p); 
        }
        [HttpPost]
        public ActionResult CTPhieuDatPhong(ChiTietPhieuDatPhong ct )
        {
            var pdpid = Session["pdpid"] as int?;
            var id = Session["UserId"] as int?;
            ct.PhieuDatPhongId = (int)pdpid;

            ct.PhongId = (int)id;
            
            db.ChiTietPhieuDatPhongs.Add(ct);
            Session["UserId"] = null;
            Session["makh"]=null;
            Session["pdpid"] = null;
            db.SaveChanges();


            return RedirectToAction("ThanhCong");
        }
        public ActionResult ThanhCong()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult DonDep(Phong p, int id)
        {
             p = db.Phongs.Where(row => row.PhongId == id).FirstOrDefault();

            p.TrangThai = 0;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult XacNhanOder()
        {
            return View();
        }
        public ActionResult QLLuuTru()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DoiPhong(int id,string strSearch = "", string sr = "")
        {
            ViewBag.maphong = id;
            List<Phong> phongs = db.Phongs.Where(r=>r.TrangThai==0&& r.PhongId !=id).ToList();
            ViewBag.phongs = phongs;
            Phong p = db.Phongs.Where(r => r.PhongId == id).FirstOrDefault();
            ViewBag.nem = p.PhongName;
            List<LoaiPhong> loaiPhongs = db.LoaiPhongs.ToList();
            ViewBag.loai = loaiPhongs;
            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                var kq = db.Phongs.Where(r => r.LoaiPhong.TenLoai.ToString().Contains(strSearch)).ToList();

                return View(kq);

            }
            if (!string.IsNullOrEmpty(sr))
            {
                var kq = db.Phongs.Where(r => r.TrangThai.ToString().Contains(sr)).ToList();
                return View(kq);
            }


            return View(phongs);
        }
        [HttpPost]
        public ActionResult DoiPhong(int id, string roomName, int roomStatus, string roomType)
        {
            List<LoaiPhong> loaiPhongs = db.LoaiPhongs.ToList();
            ViewBag.loai = loaiPhongs;
            Phong pm =db.Phongs.Where(r=>r.PhongName == roomName).FirstOrDefault();
            Phong pc =db.Phongs.Where(r=>r.PhongId==id).FirstOrDefault();
            ChiTietPhieuDatPhong ctpdp=db.ChiTietPhieuDatPhongs.Where(r=>r.PhongId == id).FirstOrDefault();
            pc.TrangThai = 2;
            pm.TrangThai = 1;
            ctpdp.PhongId = pm.PhongId;
            db.SaveChanges();

            return RedirectToAction("DoiPhonngThanhCong", "Home");
        }
        public ActionResult DoiPhonngThanhCong()
        {
            return View();
        }
       


    }
}