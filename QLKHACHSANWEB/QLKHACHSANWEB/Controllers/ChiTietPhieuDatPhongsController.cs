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
    public class ChiTietPhieuDatPhongsController : Controller
    {
        private QLKhachSanContext db = new QLKhachSanContext();

        // GET: ChiTietPhieuDatPhongs
        public ActionResult Index()
        {
            var chiTietPhieuDatPhongs = db.ChiTietPhieuDatPhongs.Include(c => c.PhieuDatPhong).Include(c => c.Phong);
            return View(chiTietPhieuDatPhongs.ToList());
        }

        // GET: ChiTietPhieuDatPhongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhongs.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuDatPhong);
        }

        // GET: ChiTietPhieuDatPhongs/Create
        public ActionResult Create()
        {
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId");
            ViewBag.PhongId = new SelectList(db.Phongs, "PhongId", "PhongName");
            return View();
        }

        // POST: ChiTietPhieuDatPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChiTietPhieuDatPhongId,PhieuDatPhongId,PhongId")] ChiTietPhieuDatPhong chiTietPhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuDatPhongs.Add(chiTietPhieuDatPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", chiTietPhieuDatPhong.PhieuDatPhongId);
            ViewBag.PhongId = new SelectList(db.Phongs, "PhongId", "PhongName", chiTietPhieuDatPhong.PhongId);
            return View(chiTietPhieuDatPhong);
        }

        // GET: ChiTietPhieuDatPhongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhongs.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", chiTietPhieuDatPhong.PhieuDatPhongId);
            ViewBag.PhongId = new SelectList(db.Phongs, "PhongId", "PhongName", chiTietPhieuDatPhong.PhongId);
            return View(chiTietPhieuDatPhong);
        }

        // POST: ChiTietPhieuDatPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChiTietPhieuDatPhongId,PhieuDatPhongId,PhongId")] ChiTietPhieuDatPhong chiTietPhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", chiTietPhieuDatPhong.PhieuDatPhongId);
            ViewBag.PhongId = new SelectList(db.Phongs, "PhongId", "PhongName", chiTietPhieuDatPhong.PhongId);
            return View(chiTietPhieuDatPhong);
        }

        // GET: ChiTietPhieuDatPhongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhongs.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuDatPhong);
        }

        // POST: ChiTietPhieuDatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhongs.Find(id);
            db.ChiTietPhieuDatPhongs.Remove(chiTietPhieuDatPhong);
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
