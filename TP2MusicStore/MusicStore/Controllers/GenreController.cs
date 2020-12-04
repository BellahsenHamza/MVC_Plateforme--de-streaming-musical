using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize(Users = "admin")]
    public class GenreController : Controller
    {
        private Depot depot = new Depot();
        // GET: Genre

        [HttpGet]
        public ActionResult Index()
        {
            List <Genre> lg = depot.Genres.List();
            return View(lg);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var g = new Genre();
            return View(g);
        }

        [HttpPost]
        public ActionResult Create(Genre g)
        {
            if (ModelState.IsValid)
            {
                depot.Genres.Add(g);
                //retourner à Index
                return this.RedirectToAction("Index", "Genre");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Genre g = depot.Genres.Find(id);

            return this.View(g);
        }

        [HttpPost]
        public ActionResult Edit(Genre g)
        {
            if (ModelState.IsValid)
            {
                depot.Genres.Update(g);
                return this.RedirectToAction("Index", "Genre");
            }
            return View();

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Genre g = depot.Genres.Find(id);
            return this.View(g);
        }

        [HttpPost]
        public ActionResult Delete(Genre g)
        {
            depot.Genres.Remove(g.GenreId);
            return this.RedirectToAction("Index", "Genre");

        }
    }
}