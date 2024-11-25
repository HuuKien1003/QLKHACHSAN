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
    public class CTDichVusController : Controller
    {
        private QLKhachSanContext db = new QLKhachSanContext();

        // GET: CTDichVus
        public ActionResult Index()
        {
            var cTDichVus = db.CTDichVus.Include(c => c.DichVu).Include(c => c.PhieuDatPhong);
            return View(cTDichVus.ToList());
        }

        // GET: CTDichVus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDichVu cTDichVu = db.CTDichVus.Find(id);
            if (cTDichVu == null)
            {
                return HttpNotFound();
            }
            return View(cTDichVu);
        }

        // GET: CTDichVus/Create
        public ActionResult Create()
        {
            ViewBag.DichVuId = new SelectList(db.DichVus, "DichVuId", "DichVuName");
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId");
            return View();
        }

        // POST: CTDichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTDichVuId,DichVuId,PhieuDatPhongId,SoLuong")] CTDichVu cTDichVu)
        {
            if (ModelState.IsValid)
            {
                db.CTDichVus.Add(cTDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DichVuId = new SelectList(db.DichVus, "DichVuId", "DichVuName", cTDichVu.DichVuId);
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", cTDichVu.PhieuDatPhongId);
            return View(cTDichVu);
        }

        // GET: CTDichVus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDichVu cTDichVu = db.CTDichVus.Find(id);
            if (cTDichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DichVuId = new SelectList(db.DichVus, "DichVuId", "DichVuName", cTDichVu.DichVuId);
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", cTDichVu.PhieuDatPhongId);
            return View(cTDichVu);
        }

        // POST: CTDichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTDichVuId,DichVuId,PhieuDatPhongId,SoLuong")] CTDichVu cTDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DichVuId = new SelectList(db.DichVus, "DichVuId", "DichVuName", cTDichVu.DichVuId);
            ViewBag.PhieuDatPhongId = new SelectList(db.PhieuDatPhongs, "PhieuDatPhongId", "PhieuDatPhongId", cTDichVu.PhieuDatPhongId);
            return View(cTDichVu);
        }

        // GET: CTDichVus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDichVu cTDichVu = db.CTDichVus.Find(id);
            if (cTDichVu == null)
            {
                return HttpNotFound();
            }
            return View(cTDichVu);
        }

        // POST: CTDichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTDichVu cTDichVu = db.CTDichVus.Find(id);
            db.CTDichVus.Remove(cTDichVu);
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
