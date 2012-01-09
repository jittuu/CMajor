using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMajor.Models;
using CMajor.Infrastructure;

namespace CMajor.Controllers
{   
    public class AlbumsController : ApplicationController
    {
        public AlbumsController(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
        //
        // GET: /Albums/

        public ViewResult Index()
        {
            return View(DbContext.Albums.ToList());
        }

        //
        // GET: /Albums/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Albums/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                DbContext.Albums.Add(album);
                return RedirectToAction("Index");  
            }

            return View(album);
        }
        
        //
        // GET: /Albums/Edit/5
 
        public ActionResult Edit(int id)
        {
            Album album = DbContext.Albums.Single(x => x.Id == id);
            return View(album);
        }

        //
        // POST: /Albums/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(album).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View(album);
        }

        //
        // GET: /Albums/Delete/5
 
        public ActionResult Delete(int id)
        {
            Album album = DbContext.Albums.Single(x => x.Id == id);
            return View(album);
        }

        //
        // POST: /Albums/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = DbContext.Albums.Single(x => x.Id == id);
            DbContext.Albums.Remove(album);
            return RedirectToAction("Index");
        }
    }
}