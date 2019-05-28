using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using BO;
using Oukilestkiki;
using Oukilestkiki.ViewModels;

namespace Oukilestkiki.Controllers
{
    public class UtilisateursController : Controller
    {
        private Context db = new Context();

        // GET: Utilisateurs
        public ActionResult Index()
        {
            if (IsValid())
            {
                return View(db.Utilisateurs.ToList());
            }
            return View("Error");
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public ActionResult Create(bool isInscription = false)
        {
            return View(new UtilisateurCreateEditVm
            {
                Utilisateur = new Utilisateur(),
                ListeRoles = db.Roles.ToList(),
                IsInscription = isInscription
            });
        }

        // POST: Utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UtilisateurCreateEditVm vm)
        {
            if (ModelState.IsValid)
            {
                var role = vm.IsInscription ? db.Roles.FirstOrDefault(r => r.Libelle == "Membre") : db.Roles.Find(vm.Utilisateur.Role.Id);
                vm.Utilisateur.Role = role;
                db.Utilisateurs.Add(vm.Utilisateur);
                db.SaveChanges();

                if (vm.IsInscription)
                {
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id, bool isInscription = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new UtilisateurCreateEditVm();
            vm.Utilisateur = db.Utilisateurs.Find(id);
            vm.IsInscription = isInscription;
            vm.ListeRoles = db.Roles.ToList();
            vm.Id = vm.Utilisateur.Id;
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UtilisateurCreateEditVm vm)
        {
            if (ModelState.IsValid)
            {
                var role = db.Roles.Find(vm.Utilisateur.Role.Id);
                var user = db.Utilisateurs.Find(vm.Utilisateur.Id);
                user.Role = role;
                user.Mail = vm.Utilisateur.Mail;
                user.Password = vm.Utilisateur.Password;
                user.Nom = vm.Utilisateur.Nom;
                user.Prenom = vm.Utilisateur.Prenom;
                user.Suspendu = vm.Utilisateur.Suspendu;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsValid())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = db.Utilisateurs.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
            return View("Error");
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = (Utilisateur)Session["Utilisateur"];
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (user != null)
            {
                if (user.Id == utilisateur.Id)
                {
                    Session["Utilisateur"] = null;
                }
            }
            db.Utilisateurs.Remove(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Inscription()
        {
            return RedirectToAction("Create", new {isInscription = true});
        }

        public ActionResult EditProfil()
        {
            var user = (Utilisateur) Session["Utilisateur"];
            var vm = new UtilisateurCreateEditVm();
            vm.Utilisateur = user;
            vm.IsInscription = true;
            vm.ListeRoles = db.Roles.ToList();
            return RedirectToAction("Edit", new { id = user.Id, isInscription = true });
        }

        public bool IsValid()
        {
            var user = (Utilisateur)Session["Utilisateur"];
            if (user != null)
            {
                if (user.Role.Libelle.Equals("Admin"))
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult Recherche(string nom)
        {
            var currentUser = (Utilisateur)Session["Utilisateur"];
            var listeUsers = db.Utilisateurs.ToList();
            var listeUserSearch = new List<Utilisateur>();
            if (nom.Contains(" "))
            {
                var mot1 = nom.Split(' ');
                if (mot1.Length >= 2)
                {
                    listeUsers.ForEach(user =>
                        {
                            foreach (var prenom in mot1)
                            {
                                if (user.Nom.ToLower().Equals(prenom) || user.Nom.ToLower().Equals(prenom))
                                {
                                    listeUserSearch.Add(user);
                                }
                            }
                        }
                    );
                }
            }
            else
            {
                listeUsers.ForEach(user =>
                    {
                        if (user.Prenom.ToLower().Equals(nom) || user.Nom.ToLower().Equals(nom))
                        {
                            listeUserSearch.Add(user);
                        }
                    }
                );
            }

            if (listeUserSearch.Count == 1)
            {
                return RedirectToAction("Details",new { id = listeUserSearch.FirstOrDefault().Id});
            }
            if (currentUser != null)
            {
                listeUserSearch.Remove(listeUserSearch.Find(user => user.Id == currentUser.Id));
                return View(listeUserSearch);
            }
            return View(listeUserSearch);
        }
    }
}
