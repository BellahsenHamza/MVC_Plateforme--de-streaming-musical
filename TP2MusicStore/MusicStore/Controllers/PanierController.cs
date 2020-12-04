using MusicStore.Models;
using MusicStore.Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class PanierController : Controller
    {
        Depot depot = new Depot();
        [HttpGet]
        public ActionResult Index()
        {
            Utilisateur u=depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            List<Album> la = depot.Paniers.TousLesArticles(u.UtilisateurId).ToList();
            double Total = 0;
            foreach(Album a in la)
            {
                Total = Total + a.Prix;
               
            }
            ViewBag.Total = Total;
            return View(la);
        }
        [HttpGet]
        public ActionResult Ajouter(int AlbumId)
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            depot.Paniers.AjouterUnArticle(AlbumId, u.UtilisateurId);
            return this.RedirectToAction("Index", "Panier");
        }
        [HttpGet]
        public ActionResult SupprimerUnArticle(int AlbumId)
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            depot.Paniers.SupprimerUnArticle(AlbumId, u.UtilisateurId);
            return this.RedirectToAction("Index", "Panier");
        }
        [HttpGet]
        public ActionResult ViderPanier()
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            depot.Paniers.ViderLePanier(u.UtilisateurId);
            return this.RedirectToAction("Index", "Panier");
        }
    }
}