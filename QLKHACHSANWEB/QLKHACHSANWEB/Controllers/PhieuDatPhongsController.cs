using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKHACHSANWEB.Models;

namespace QLKHACHSANWEB.Controllers
{
    public class PhieuDatPhongsController : Controller
    {
        private QLKhachSanContext db = new QLKhachSanContext();

        // GET: PhieuDatPhongs
        public ActionResult Index()
        {
            var phieuDatPhongs = db.PhieuDatPhongs.Include(p => p.KhachHang);
            return View(phieuDatPhongs.ToList());
        }

        // GET: PhieuDatPhongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatPhong);
        }

        // GET: PhieuDatPhongs/Create
        public ActionResult Create()
        {
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "KhachHangId", "KhachHangName");
            return View();
        }

        // POST: PhieuDatPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhieuDatPhongId,KhachHangId,NgayDat,NgayTra")] PhieuDatPhong phieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.PhieuDatPhongs.Add(phieuDatPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "KhachHangId", "KhachHangName", phieuDatPhong.KhachHangId);
            return View(phieuDatPhong);
        }

        // GET: PhieuDatPhongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "KhachHangId", "KhachHangName", phieuDatPhong.KhachHangId);
            return View(phieuDatPhong);
        }

        // POST: PhieuDatPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhieuDatPhongId,KhachHangId,NgayDat,NgayTra")] PhieuDatPhong phieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "KhachHangId", "KhachHangName", phieuDatPhong.KhachHangId);
            return View(phieuDatPhong);
        }

        // GET: PhieuDatPhongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatPhong);
        }

        // POST: PhieuDatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhongs.Find(id);
            db.PhieuDatPhongs.Remove(phieuDatPhong);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
