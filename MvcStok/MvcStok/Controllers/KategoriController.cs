using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db=new DbMvcStokEntities();
        public ActionResult Index()
        {
            var ktgr = db.TblKategori.ToList();

            return View(ktgr);
        }

        [HttpGet]
        public ActionResult YeniEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniEkle(TblKategori kategori)
        {
            db.TblKategori.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            
            var ktgr= db.TblKategori.Find(id);
            db.TblKategori.Remove(ktgr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var kategori = db.TblKategori.Find(id);
            return View(kategori);
        }

        [HttpPost]
        public ActionResult Guncelle(TblKategori kategori)
        {
            var ktgr = db.TblKategori.Find(kategori.id);
            ktgr.ad = kategori.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}