using System.Web;
using BO;

namespace Oukilestkiki.Models
{
    public static class Authentification
    {
        public static bool EstConnecte()
        {
            var user = (Utilisateur)HttpContext.Current.Session["Utilisateur"];
            if (user != null)
            {
                if (user.Role.Libelle.Equals("Admin"))
                {
                    return true;
                }
            }
            return false;
        }

        public static Utilisateur GetSessionUtilisateur()
        {
            var user = HttpContext.Current.Session["Utilisateur"];
            if (user != null)
            {
                return (Utilisateur) user;
            }

            return null;
        }
    }
}