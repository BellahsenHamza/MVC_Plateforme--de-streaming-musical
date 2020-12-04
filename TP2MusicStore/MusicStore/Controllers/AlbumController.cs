using MusicStore.Models;
using MusicStore.Models.DataModels;
using MusicStore.Models.ViewModels.Album;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize(Users = "admin")]
    public class AlbumController : Controller
    {
        private Depot depot = new Depot();
        // GET: Album
        [HttpGet]
        public ActionResult Index(string filtreTitre = "", string filtreArtiste = "", int? filtreGenre = null)
        {
            List<Album> la = depot.Albums.List();
            if (!(filtreTitre == null))
                la = la.Where(model => model.Titre.Contains(filtreTitre)).ToList();
            if (!(filtreArtiste == null))
                la = la.Where(model => model.Artiste.Contains(filtreArtiste)).ToList();
            if (!(filtreTitre == null))
                la = la.Where(model => model.Titre.Contains(filtreTitre)).ToList();
            if (!(filtreGenre == null))
                la = la.Where(model => (int)model.GenreId == filtreGenre).ToList();

            return View(la);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new CreerAlbum());

        }

        [HttpPost]
        public ActionResult Create(CreerAlbum ma)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    Album a = new Album();
                    a.AnneeParution = ma.AnneeParution;
                    a.Artiste = ma.Artiste;
                    a.Description = ma.Description;
                    a.GenreId = ma.GenreId;
                    a.Prix = ma.Prix;
                    a.Titre = ma.Titre;
                    depot.Albums.Add(a);
                    if (ma.image != null)
                        ma.image.SaveAs(Server.MapPath(a.Cover));
                    return RedirectToAction("Index", "Album");
                }
                catch (SqlException e)
                {
                     ModelState.AddModelError("", e.Message);

                }
            }
            return this.View(ma);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {          
            Album a = depot.Albums.Find(id);
            var m = new modifAlbum();
            m.AlbumId = a.AlbumId;
            m.AnneeParution = a.AnneeParution;
            m.Artiste = a.Artiste;
            m.Description = a.Description;
            m.Titre = a.Titre;
            m.Prix = a.Prix;
            m.GenreId = a.GenreId;
            return this.View(m);
        }

        [HttpPost]
        public ActionResult Edit(modifAlbum ma)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Album a = depot.Albums.Find(ma.AlbumId);
                    a.AnneeParution = ma.AnneeParution;
                    a.Artiste = ma.Artiste;
                    a.Description = ma.Description;
                    a.GenreId = ma.GenreId;
                    a.Prix = ma.Prix;
                    a.Titre = ma.Titre;
                    if (ma.image != null)
                        ma.image.SaveAs(Server.MapPath(a.Cover));
                    depot.Albums.Update(a);
                    return this.RedirectToAction("Index", "Album");
                }
                catch (SqlException e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }
            return View(ma);

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Album a = depot.Albums.Find(id);

            return this.View(a);
        }

        [HttpPost]
        public ActionResult Delete(Album a)
        {
            try
            {
                depot.Albums.Remove(a.AlbumId);
                return this.RedirectToAction("Index", "Album");
            }
            catch(SqlException e)
            {
                ModelState.Clear();
                ModelState.AddModelError("Erreur", e.Message);
               
            }
            return View(a);
        }
    }
}