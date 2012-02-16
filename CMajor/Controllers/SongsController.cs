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
    public class SongsController : ApplicationController
    {
        public SongsController(IUnitOfWork unitOfWork)
            : base(unitOfWork) {
        }

        //
        // GET: /Songs/

        public ViewResult Index()
        {
            return View(DbContext.Songs.Include(song => song.Albums).Include(song => song.Artists).ToList());
        }

        //
        // GET: /Songs/Details/5

        public ViewResult Details(int id)
        {
            Song song = DbContext.Songs.Single(x => x.Id == id);
            return View(song);
        }

        //
        // GET: /Songs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Songs/Create

        [HttpPost]
        public ActionResult Create(Song song)
        {
            if (ModelState.IsValid)
            {
                DbContext.Songs.Add(song);
                return RedirectToAction("Index");  
            }

            return View(song);
        }
        
        //
        // GET: /Songs/Edit/5
 
        public ActionResult Edit(int id)
        {
            Song song = DbContext.Songs.Single(x => x.Id == id);
            return View(song);
        }

        //
        // POST: /Songs/Edit/5

        [HttpPost]
        public ActionResult Edit(Song song)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(song).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View(song);
        }

        //
        // GET: /Songs/Delete/5
 
        public ActionResult Delete(int id)
        {
            Song song = DbContext.Songs.Single(x => x.Id == id);
            return View(song);
        }

        //
        // POST: /Songs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = DbContext.Songs.Single(x => x.Id == id);
            DbContext.Songs.Remove(song);
            return RedirectToAction("Index");
        }
    }
}