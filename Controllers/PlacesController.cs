using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuideMe.Models.Business_models;
using GuideMe.Models.Identity_models;
using PagedList;
using PagedList.Mvc;

namespace GuideMe.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PlacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Places
        public ActionResult Index(int? page)
        {
            var places = db.Places.Include(p => p.City).Include(p => p.Category);
            return View(places.ToList().ToPagedList(page ?? 1 ,9));
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }
        [Authorize(Roles ="Admin")]
        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }
         
        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,CityID,Address,GoogleLocation,Image,ImageFile")] Place place)
        {

            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", place.CityID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", place.CategoryID);
            place.ID = db.Places.Select(p => p.ID).Max() + 1;
            if (place.ImageFile != null && place.ImageFile.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/img/uploads"),
                                                Path.GetFileName(place.ImageFile.FileName));
                    place.ImageFile.SaveAs(path);
                    place.Image = place.ImageFile.FileName;

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");

        }

        // GET: Places/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", place.CityID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", place.CategoryID);
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,CityID,Address,GoogleLocation,Image,ImageFile")] Place place)
        {
            if (ModelState.IsValid)
            {
                Place oldPlaceObj = db.Places.AsNoTracking().Where(P => P.ID == place.ID).FirstOrDefault();
                if (place.ImageFile != null && place.ImageFile.ContentLength > 0)
                    try
                    {
                        if (oldPlaceObj.Image != null)
                        {
                            //deleting old img
                            string oldPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                                        Path.GetFileName(oldPlaceObj.Image));
                            System.IO.File.Delete(oldPath);
                        }
                        //saving new img
                        string newPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                                    Path.GetFileName(place.ImageFile.FileName));
                        place.ImageFile.SaveAs(newPath);
                        place.Image = place.ImageFile.FileName;
                        
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", place.CityID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", place.CategoryID);
            return View(place);
        }

        [Authorize(Roles ="Admin")]
        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        [Authorize(Roles = "Admin")]
        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            if (place.Image != null)
            {
                string imgPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                           Path.GetFileName(place.Image));
                System.IO.File.Delete(imgPath);
            }
            db.Places.Remove(place);
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
