using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NankinjoKey.Models;

namespace NankinjoKey.Controllers
{
    public class KeyLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: KeyLogs
        public ActionResult Index()
        {
            var keyLogs = db.KeyLogs.Include(k => k.KeyInfo);
            return View(keyLogs.ToList());
        }

        // GET: KeyLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyLog keyLog = db.KeyLogs.Find(id);
            if (keyLog == null)
            {
                return HttpNotFound();
            }
            return View(keyLog);
        }

        // GET: KeyLogs/Create
        public ActionResult Create()
        {
            ViewBag.KeyInfoId = new SelectList(db.KeyInfoes, "Id", "KeyCode");
            return View();
        }

        // POST: KeyLogs/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KeyInfoId,KeyId,UserName,Status,Place,UpdateDateTime,CreateDateTime,UpdateUser,CreateUser")] KeyLog keyLog)
        {
            if (ModelState.IsValid)
            {
                db.KeyLogs.Add(keyLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyInfoId = new SelectList(db.KeyInfoes, "Id", "KeyCode", keyLog.KeyInfoId);
            return View(keyLog);
        }

        // GET: KeyLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyLog keyLog = db.KeyLogs.Find(id);
            if (keyLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyInfoId = new SelectList(db.KeyInfoes, "Id", "KeyCode", keyLog.KeyInfoId);
            return View(keyLog);
        }

        // POST: KeyLogs/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KeyInfoId,KeyId,UserName,Status,Place,UpdateDateTime,CreateDateTime,UpdateUser,CreateUser")] KeyLog keyLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyInfoId = new SelectList(db.KeyInfoes, "Id", "KeyCode", keyLog.KeyInfoId);
            return View(keyLog);
        }

        // GET: KeyLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyLog keyLog = db.KeyLogs.Find(id);
            if (keyLog == null)
            {
                return HttpNotFound();
            }
            return View(keyLog);
        }

        // POST: KeyLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyLog keyLog = db.KeyLogs.Find(id);
            db.KeyLogs.Remove(keyLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 開閉ボタン 初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Button()
        {
            ViewBag.btnImage = "btnDisable";
            return View();
        }

        /// <summary>
        /// キーコード確認
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistCode(string keyCode)
        {
            Session.Remove("keyCode");

            string btnImage;

            if (db.KeyInfoes.Any(s => s.KeyCode == keyCode))
            {
                Session["keyCode"] = keyCode;
            }
            else
            {
                btnImage = "btnDisable";
                ViewBag.btnImage = btnImage;
                return View();
            }

            if (db.KeyInfoes.Where(s => s.KeyCode == keyCode).FirstOrDefault().Status == 0)
            {
                //鍵が閉まっている
                btnImage = "btnOpen";
            }
            else
            {
                //鍵が開いている
                btnImage = "btnClose";
            }

            ViewBag.btnImage = btnImage;

            return View("Button");
        }

        /// <summary>
        /// ボタンプッシュ
        /// </summary>
        /// <param name="btnImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PushButton(string btnImage)
        {
            string keyCode;
            if(Session["keyCode"] != null)
            {
                keyCode = (string)Session["keyCode"];
            }
            else
            {
                ViewBag.btnImage = "btnDisable";
                return View("Button");
            }

            var keyInfo = db.KeyInfoes.Where(s => s.KeyCode == keyCode).FirstOrDefault();

            if (btnImage == "btnOpen")
            {
                //鍵を開ける
                keyInfo.Status = 1;
                db.Entry(keyInfo).State = EntityState.Modified;
                db.SaveChanges();
                //ログ
                var log = new KeyLog();
                log.KeyInfoId = keyInfo.Id;
                log.KeyInfo = keyInfo;
                log.UserName = keyInfo.UserName;
                log.Status = 1;
                log.Place = "この場所で開けました。";
                db.KeyLogs.Add(log);
                db.SaveChanges();
                //ボタンイメージの変更
                ViewBag.btnImage = "btnClose";
            }
            else
            {
                //鍵を閉める
                keyInfo.Status = 0;
                db.Entry(keyInfo).State = EntityState.Modified;
                db.SaveChanges();
                //ログ
                var log = new KeyLog();
                log.KeyInfoId = keyInfo.Id;
                log.KeyInfo = keyInfo;
                log.UserName = keyInfo.UserName;
                log.Status = 0;
                log.Place = "この場所で閉めました。";
                db.KeyLogs.Add(log);
                db.SaveChanges();
                //ボタンイメージの変更
                ViewBag.btnImage = "btnOpen";
            }
            return View("Button");
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
