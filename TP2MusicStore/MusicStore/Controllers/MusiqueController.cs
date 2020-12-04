using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class MusiqueController : Controller
    {
        Depot depot = new Depot();
        public ActionResult Index(int? genreid=null)
        {
            List<Album> la = depot.Albums.List();
            if (!(genreid == null))
                la = la.Where(model => (int)model.GenreId == genreid).ToList();

            return View(la);
        }
    }
}