using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oukilestkiki.Enums;
using Oukilestkiki.Models;
using Oukilestkiki.ViewModels;

namespace Oukilestkiki.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(new MessageViewModel
            {
                Messages = db.Messages.Where(m => m.Type == TypeMessageEnum.Public).OrderByDescending(m => m.DateEnvoi).ToList()
            });
        }

        public ActionResult EnvoyerMessage(MessageViewModel mvm)
        {
            if (ModelState.IsValid)
            {
                mvm.NewMessage.DateEnvoi = DateTime.Now;
                var utilisateur = Authentification.GetSessionUtilisateur();
                mvm.NewMessage.Expediteur = db.Utilisateurs.Find(utilisateur?.Id);
                mvm.NewMessage.Type = TypeMessageEnum.Public;

                db.Messages.Add(mvm.NewMessage);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}