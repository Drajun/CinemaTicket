using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaTicket.Models;
using System.IO;

namespace CinemaTicket.Controllers
{
    [Authorize(Roles ="admin")]
    public class MovieManageController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: MovieManage
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        //图片上传
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/MovieImage/"),fileName);
                    file.SaveAs(filePath);

                    string message = "../MovieImage/" + file.FileName;
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Message"] = "空文件";
                }
            }
            else
            {
                TempData["Message"] = "未选择文件";
            }

            return View("Create");
        }

        // GET: MovieManage/Details/5
        public ActionResult Details(int id)
        {
            if (id <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // GET: MovieManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "type,name,poster,area,releaseTime,price,remarks")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                //默认id，到数据库会自动变成自增长
                movieModels.id = -1;
                db.Movies.Add(movieModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieModels);
        }

        // GET: MovieManage/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // POST: MovieManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,type,name,poster,area,releaseTime,price,remarks")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieModels);
        }

        // GET: MovieManage/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // POST: MovieManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieModels movieModels = db.Movies.Find(id);
            db.Movies.Remove(movieModels);
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
