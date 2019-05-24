using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;

namespace Oukilestkiki.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utilisateur utilisateur)
        {
            var userConnect = db.Utilisateurs.FirstOrDefault(user => user.Mail == utilisateur.Mail);
            if (userConnect.Password == utilisateur.Password)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }
    }
}