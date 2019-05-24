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
using Oukilestkiki.Models;

namespace Oukilestkiki.Controllers
{
    public class TypeAnimalsController : Controller
    {
        private Context db = new Context();

        // GET: TypeAnimals
        public ActionResult Index()
        {
            if (IsValid())
            {
                return View(db.TypeAnimaux.ToList());
            }
            return View("Error");
        }

        // GET: TypeAnimals/Details/5
        public ActionResult Details(int? id)
        {
            if (IsValid())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TypeAnimal typeAnimal = db.TypeAnimaux.Find(id);
                if (typeAnimal == null)
                {
                    return HttpNotFound();
                }
                return View(typeAnimal);
            }
            return View("Error");
        }

        // GET: TypeAnimals/Create
        public ActionResult Create()
        {
            if (IsValid())
            {
                return View();
            }
            return View("Error");
        }

        // POST: TypeAnimals/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Libelle")] TypeAnimal typeAnimal)
        {
            if (ModelState.IsValid)
            {
                db.TypeAnimaux.Add(typeAnimal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeAnimal);
        }

        // GET: TypeAnimals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsValid())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TypeAnimal typeAnimal = db.TypeAnimaux.Find(id);
                if (typeAnimal == null)
                {
                    return HttpNotFound();
                }
                return View(typeAnimal);
            }
            return View("Error");
        }

        // POST: TypeAnimals/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Libelle")] TypeAnimal typeAnimal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeAnimal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeAnimal);
        }

        // GET: TypeAnimals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsValid())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TypeAnimal typeAnimal = db.TypeAnimaux.Find(id);
                if (typeAnimal == null)
                {
                    return HttpNotFound();
                }
                return View(typeAnimal);
            }
            return View("Error");
        }

        // POST: TypeAnimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeAnimal typeAnimal = db.TypeAnimaux.Find(id);
            db.TypeAnimaux.Remove(typeAnimal);
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
    }
}
