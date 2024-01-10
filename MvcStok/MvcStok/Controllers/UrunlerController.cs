using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        DbMvcStokEntities db=new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            //var urunler=db.TblUrunler.Where(x=>x.durum==true).ToList();
            var urunler = from x in db.TblUrunler select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = db.TblUrunler.Where(x => x.marka.Contains(p)||x.ad.Contains(p));   
            }
            return View(urunler.ToList());
        }
        public ActionResult KapaliUrunler()
        {
            var urunler=db.TblUrunler.Where(x=>x.durum==false).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktgr=(from x in db.TblKategori.ToList()
                                       select new SelectListItem
                                       {
                                           Text=x.ad,
                                           Value=x.id.ToString()
                                       }).ToList();
            ViewBag.drop = ktgr;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TblUrunler urun)
        {
            
            var ktgr = db.TblKategori.Where(x => x.id == urun.TblKategori.id).FirstOrDefault();
            urun.TblKategori = ktgr;
            urun.durum = urun.stok > 0 ? true : false;
            db.TblUrunler.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Getir(int ID)
        {
            var urun = db.TblUrunler.Find(ID);
            List<SelectListItem> ktgr=(from x in db.TblKategori.ToList()
                                       select new SelectListItem
                                       {
                                           Text=x.ad,
                                           Value=x.id.ToString()
                                       }).ToList();
            ViewBag.drops=ktgr;
            return View("Getir",urun);
        }
        public ActionResult Guncelle(TblUrunler urun)
        {
            var ktgr = db.TblKategori.Where(x => x.id == urun.TblKategori.id).FirstOrDefault();
            var dburun = db.TblUrunler.Find(urun.id);
            dburun.TblKategori= ktgr;
            dburun.ad= urun.ad;
            dburun.marka= urun.marka;
            dburun.stok= urun.stok;
            dburun.alisfiyat= urun.alisfiyat;
            dburun.satisfiyat= urun.satisfiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(TblUrunler urun)
        {
            var gelenurun = db.TblUrunler.Find(urun.id);
            gelenurun.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Yenile(int ID)
        {
            
            var urun=db.TblUrunler.Find(ID);
            urun.durum =urun.stok>0? true:false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}