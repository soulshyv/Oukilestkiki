using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using Oukilestkiki.Enums;
using Oukilestkiki.Models;
using Oukilestkiki.Services.Photos;
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
                var utilisateur = Authentification.GetSessionUtilisateur();
                var message = new Message
                {
                    Contenu = mvm.ContenuMessage,
                    DateEnvoi = DateTime.Now,
                    Expediteur = db.Utilisateurs.Find(utilisateur.Id),
                    Type = TypeMessageEnum.Public
                };

                db.Messages.Add(message);

                db.SaveChanges();

                var photoService = new PhotosService();
                var photos = photoService.Add(new PhotoRechercheViewModel
                {
                    Photos = mvm.PiecesJointes,
                    Message = message
                });

                message.PiecesJointes.AddRange(photos.Select(p => db.Photos.Find(p.Id)));

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}