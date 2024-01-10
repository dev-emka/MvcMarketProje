using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList.Mvc;
using PagedList;

namespace MvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var satislar = db.TblSatislar.ToList().ToPagedList(sayfa, 50);
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> mstr = (from x in db.TblMusteriler
                                         select new SelectListItem
                                         {
                                             Text = x.ad + " " + x.soyad,
                                             Value = x.id.ToString()
                                         }).ToList();
            List<SelectListItem> urun=(from x in db.TblUrunler select new SelectListItem {Text=x.ad,Value=x.id.ToString() }).ToList();
            List<SelectListItem> personel=(from x in db.TblPersonel.Where(m=>m.departman=="Satış") select new SelectListItem {Text=x.ad+" "+x.soyad,Value=x.id.ToString() }).ToList();
            
            ViewBag.musteriAd = mstr;
            ViewBag.urunAd=urun;
            ViewBag.personelAd=personel;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TblSatislar satis)
        {
            var mstr=db.TblMusteriler.Where(m=>m.id==satis.TblMusteriler.id).FirstOrDefault();
            var urun=db.TblUrunler.Where(m=>m.id==satis.TblUrunler.id).FirstOrDefault();
            var personel=db.TblPersonel.Where(m=>m.id==satis.TblPersonel.id).FirstOrDefault();
            satis.TblMusteriler = mstr;
            satis.TblUrunler = urun;
            satis.TblPersonel = personel;
            db.TblSatislar.Add(satis);
            satis.fiyat = satis.TblUrunler.satisfiyat;
            satis.tarih = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}