﻿using System;
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

namespace GuideMe.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cities
        public ActionResult Index()
        {
            return View(db.Cities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [Authorize(Roles ="Admin")]
        // GET: Cities/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Image,ImageFile")] City city)
        {
            city.ID = db.Cities.Select(c => c.ID).Max() + 1;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (city.ImageFile != null && city.ImageFile.ContentLength > 0) {
                var fileExtension = Path.GetExtension(city.ImageFile.FileName).ToLower();

                try
                {
                    foreach (var ext in allowedExtensions)
                    {
                        if (ext.Contains(fileExtension))
                        {
                            string path = Path.Combine(Server.MapPath("~/img/uploads"),
                                                            Path.GetFileName(city.ImageFile.FileName));
                            city.ImageFile.SaveAs(path);
                            city.Image = city.ImageFile.FileName;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
       
        }
        [Authorize(Roles = "Admin")]

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Image,ImageFile")] City city)
        {

            if (ModelState.IsValid)
            {
                var oldCityobj = db.Cities.AsNoTracking().Where(P => P.ID == city.ID).FirstOrDefault();
                if (city.ImageFile != null && city.ImageFile.ContentLength > 0)
                    try
                    {

                        if (oldCityobj.Image != null)
                        {
                            //deleting old img
                            string oldPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                                        Path.GetFileName(oldCityobj.Image));
                            System.IO.File.Delete(oldPath);
                        }
                        //saving new img
                        string newPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                                    Path.GetFileName(city.ImageFile.FileName));
                        city.ImageFile.SaveAs(newPath);
                        city.Image = city.ImageFile.FileName;

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }

                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            if (city.Image != null)
            {
                string imgPath = Path.Combine(Server.MapPath("~/img/uploads"),
                                            Path.GetFileName(city.Image));
                System.IO.File.Delete(imgPath);
            }
            db.Cities.Remove(city);
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
