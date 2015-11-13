using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NankinjoKey.Models;
using NankinjoKey.Classes;

namespace NankinjoKey.Controllers
{
    public class KeyInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private object keyGenerator;

        // GET: KeyInfoes
        public ActionResult Index()
        {
            return View(db.KeyInfoes.ToList());
        }

        // GET: KeyInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyInfo keyInfo = db.KeyInfoes.Find(id);
            if (keyInfo == null)
            {
                return HttpNotFound();
            }
            return View(keyInfo);
        }

        // GET: KeyInfoes/Create
        public ActionResult Create()
        {
            var keyGenerator = new KeyCodeGenerator();
            var keyCode = keyGenerator.Generate(5);
            ViewBag.keyCode = keyCode;

            return View();
        }

        // POST: KeyInfoes/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KeyCode,UserName,Status,StartDate,EndDate,UpdateDateTime,CreateDateTime,UpdateUser,CreateUser")] KeyInfo keyInfo)
        {
            if (ModelState.IsValid)
            {
                db.KeyInfoes.Add(keyInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keyInfo);
        }

        // GET: KeyInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyInfo keyInfo = db.KeyInfoes.Find(id);
            if (keyInfo == null)
            {
                return HttpNotFound();
            }
            return View(keyInfo);
        }

        // POST: KeyInfoes/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KeyCode,UserName,Status,StartDate,EndDate,UpdateDateTime,CreateDateTime,UpdateUser,CreateUser")] KeyInfo keyInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyInfo);
        }

        // GET: KeyInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyInfo keyInfo = db.KeyInfoes.Find(id);
            if (keyInfo == null)
            {
                return HttpNotFound();
            }
            return View(keyInfo);
        }

        // POST: KeyInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyInfo keyInfo = db.KeyInfoes.Find(id);
            db.KeyInfoes.Remove(keyInfo);
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
