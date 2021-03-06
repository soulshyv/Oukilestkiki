﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Oukilestkiki;
using Oukilestkiki.Models;
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

        [HttpPost]
        public ActionResult Index(string nom)
        {
            var ListeRecherche = db.Recherches.ToList();
            var ListeRechercheFinal = new List<Recherche>();
            var mot1 = nom.Split(' ');

            ListeRecherche.ForEach(recherche =>
            {
                foreach (var critere in mot1)
                {
                    if (recherche.Localisation != null)
                    {
                        if (recherche.Localisation.ToLower().Equals(critere.ToLower()))
                        {
                            ListeRechercheFinal.Add(recherche);
                        }
                    }

                    if (recherche.Animal.Nom != null)
                    {
                        if (recherche.Animal.Nom.ToLower().Contains(critere.ToLower()))
                        {
                            if (ListeRechercheFinal.All(element => element.Id != recherche.Id))
                            {
                                ListeRechercheFinal.Add(recherche);
                            }
                        }
                    }
                }
            });

            return View(ListeRechercheFinal);
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
            return View(new RechercheCreateEditViewModel
            {
                Types = db.TypeAnimaux.ToList()
            });
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
                vm.Recherche.Animal.Type = db.TypeAnimaux.Find(vm.TypeId);
                db.Animaux.Add(vm.Recherche.Animal);
                db.Recherches.Add(vm.Recherche);

                if (Authentification.EstConnecte())
                {
                    var sessionUtilisateur = Authentification.GetSessionUtilisateur();
                    var utilisateur = db.Utilisateurs.Find(sessionUtilisateur.Id);
                    vm.Recherche.Animal.Maitre = utilisateur;
                }

                var photoService = new PhotosService();
                var photosRecherche = photoService.Add(new PhotoRechercheViewModel
                {
                    Photos = vm.ImageFilesRecherche,
                    Recherche = vm.Recherche
                });

                var photosAnimal = photoService.Add(new PhotoRechercheViewModel
                {
                    Photos = vm.ImageFilesAnimal,
                    Animal = vm.Recherche.Animal
                });

                vm.Recherche.Photos = photosRecherche;
                vm.Recherche.Animal.Photos = photosAnimal;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
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
            return View(new RechercheCreateEditViewModel
            {
                Recherche = recherche,
                Types = db.TypeAnimaux.ToList()
            });
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

                recherche.Active = vm.Recherche.Active;
                recherche.DerniereApparition = vm.Recherche.DerniereApparition;
                recherche.Description = vm.Recherche.Description;
                recherche.Localisation = vm.Recherche.Localisation;
                db.SaveChanges();

                var photoService = new PhotosService();
                var photosRecherche = photoService.UpdatePhotosRecherche(new PhotoRechercheViewModel
                {
                    Photos = vm.ImageFilesRecherche,
                    Recherche = recherche,
                });

                var photosAnimal = photoService.UpdatePhotosAnimal(new PhotoRechercheViewModel
                {
                    Photos = vm.ImageFilesAnimal,
                    Animal = vm.Recherche.Animal,
                });

                recherche.Photos.AddRange(photosRecherche.Select(p => db.Photos.Find(p.Id)));
                recherche.Animal.Photos.AddRange(photosAnimal.Select(p => db.Photos.Find(p.Id)));
                db.SaveChanges();

                return RedirectToAction("Details", new { id = recherche.Id });
            }
            return View(vm);
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
