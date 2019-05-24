using System.Collections.Generic;
using System.Linq;
using Oukilestkiki.Models;
using Oukilestkiki.ViewModels;
using WebGrease.Css.Extensions;

namespace Oukilestkiki.Services.Photos
{
    public class PhotosService
    {
        private Context db = new Context();

        public Photo Add(PhotoRechercheModel prm)
        {
            if (prm.Photo == null)
            {
                return null;
            }

            var photo = UploaderService.Upload(prm.Photo);
            if (photo == null)
            {
                return null;
            }

            if (prm.Recherche != null)
            {

            }

            photo.Animal = prm.Animal;

            db.Photos.Add(photo);
            db.SaveChanges();

            return photo;
        }

        public void UpdatePhotosRecherche(List<Photo> photos, Recherche r)
        {
            var ps = db.Photos.Where(p => p.Recherches.Select(rr => rr.Id).Contains(r.Id));
            ps.ForEach(p => db.Photos.Remove(p));

            var recherche = db.Recherches.Find(r.Id);
            if (recherche == null)
            {
                return;
            }
            recherche.Photos = photos;

            db.SaveChanges();
        }

        public void UpdatePhotosAnimal(List<Photo> photos, Animal a)
        {
            var ps = db.Photos.Where(p => p.Animal.Id == a.Id);
            ps.ForEach(p => db.Photos.Remove(p));
            
            photos.ForEach(p =>
            {
                p.Animal = a;
                db.Photos.Add(p);
            });

            db.SaveChanges();
        }

        public void DeletePhotos(int id)
        {
            var photo = db.Photos.Find(id);
            if (photo == null)
            {
                return;
            }

            db.Photos.Remove(photo);
            db.SaveChanges();
        }

        public void DeletePhotosAnimal(Animal a)
        {
            var photos = db.Photos.Where(p => p.Animal.Id == a.Id);
            photos.ForEach(p => db.Photos.Remove(p));
            db.SaveChanges();
        }

        public void DeletePhotosRecherche(Recherche r)
        {
            var photos = db.Photos.Where(p => p.Recherches.Select(rr => rr.Id).Contains(r.Id));
            photos.ForEach(p => db.Photos.Remove(p));

            var recherche = db.Recherches.Find(r.Id);
            if (recherche == null)
            {
                return;
            }

            recherche.Photos = null;

            db.SaveChanges();
        }
    }
}
