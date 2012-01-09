using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMajor.Models;
using CMajor.Infrastructure;

namespace CMajor.Controllers {
    public class ArtistsController : ApplicationController {
        public ArtistsController(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        //
        // GET: /Artists/
        public ViewResult Index() {
            return View(Artists.ToArray());
        }

        //
        // GET: /Artists/Create
        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Artists/Create
        [HttpPost]
        public ActionResult Create(Artist artist) {
            if (ModelState.IsValid) {
                Artists.Add(artist);
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        //
        // GET: /Artists/Edit/5
        public ActionResult Edit(int id) {
            Artist artist = Artists.Single(x => x.Id == id);
            return View(artist);
        }

        //
        // POST: /Artists/Edit/5
        [HttpPost]
        public ActionResult Edit(Artist artist) {
            if (ModelState.IsValid) {
                DbContext.Entry(artist).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        //
        // GET: /Artists/Delete/5
        public ActionResult Delete(int id) {
            var artist = Artists.Single(x => x.Id == id);
            return View(artist);
        }

        //
        // POST: /Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            var artist = Artists.Single(x => x.Id == id);
            Artists.Remove(artist);
            return RedirectToAction("Index");
        }
    }
}