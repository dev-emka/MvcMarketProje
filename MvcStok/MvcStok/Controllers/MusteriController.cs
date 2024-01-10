using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        DbMvcStokEntities db=new DbMvcStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            var musteriler=db.TblMusteriler.Where(m=>m.durum==true).ToList().ToPagedList(sayfa,5);
            return View(musteriler);
        }
        public ActionResult BakiyesizMusteriler(int sayfa=1)
        {
            var musteriler=db.TblMusteriler.Where(m=>m.durum==false).ToList().ToPagedList(sayfa,5);
            return View(musteriler);    
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler musteri)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            musteri.durum = false;
            musteri.bakiye = 0;
            db.TblMusteriler.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            if (db.TblMusteriler.Find(id).bakiye > 0)
                return Index();
            else
            {
                db.TblMusteriler.Find(id).durum = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult MusteriGuncelle(TblMusteriler musteri)
        {
            var guncellenecek=db.TblMusteriler.Find(musteri.id);
            guncellenecek.ad=musteri.ad;
            guncellenecek.soyad=musteri.soyad;
            guncellenecek.sehir=musteri.sehir;
            guncellenecek.bakiye=musteri.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}