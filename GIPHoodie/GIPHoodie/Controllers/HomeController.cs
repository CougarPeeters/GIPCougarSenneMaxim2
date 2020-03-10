using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GIPHoodie.Models;
using GIPHoodie.Persistence;
using Microsoft.AspNetCore.Http;

namespace GIPHoodie.Controllers
{
    public class HomeController : Controller
    {
        PersistenceCode persistenceCode = new PersistenceCode();

        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("id", 1);
            ArtikelRepository artikelRepo = new ArtikelRepository();
            artikelRepo.Artikels = persistenceCode.loadArtikels();
            return View(artikelRepo);
        }

        //[HttpPost]

        //public IActionResult Index()
        //{

        //}


        public IActionResult Toevoegen(int ArtID)
        {
            Artikel GeselecteerdeArtikel = persistenceCode.loadArtikel(ArtID);
            VMArtikelAantal vmArtikelAantal = new VMArtikelAantal();
            vmArtikelAantal.GeselecteerdArtikel = GeselecteerdeArtikel;

            return View(vmArtikelAantal);
        }
    }
}
