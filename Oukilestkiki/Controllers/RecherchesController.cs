using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Oukilestkiki;
using Oukilestkiki.Services.Photos;
using Oukilestkiki.ViewModels;

namespace Oukilestkiki.Controllers
{
    public class RecherchesController : Controller
    {
        private Context db = new Context();

        // GET: Recherches
        public ActionResult Index()
        {
            return View(db.Recherches.ToList());
        }

        // GET: Recherches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recherche = db.Recherches.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            return View(recherche);
        }

        // GET: Recherches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recherches/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RechercheCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var recherche = db.Recherches.Add(vm.Recherche);

                var photoService = new PhotosService();
                var photos = photoService.Add(new PhotoRechercheViewModel
                {
                    Photos = vm.Photos,
                    Recherche = recherche,
                    Animal = recherche.Animal
                });

                recherche.Photos = photos;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Recherches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recherche = db.Recherches.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            return View(recherche);
        }

        // POST: Recherches/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RechercheCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var recherche = db.Recherches.Find(vm.Recherche.Id);

                if (recherche == null)
                {
                    return HttpNotFound();
                }

                var photoService = new PhotosService();
                var photos = photoService.UpdatePhotosRecherche(new PhotoRechercheViewModel
                {
                    Photos = vm.Photos,
                    Recherche = recherche,
                    Animal = recherche.Animal
                });

                recherche.Photos = photos;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = recherche.Id });
            }
            return View(vm.Recherche);
        }

        // GET: Recherches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recherche = db.Recherches.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            return View(recherche);
        }

        // POST: Recherches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var recherche = db.Recherches.Find(id);
            db.Recherches.Remove(recherche);
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
