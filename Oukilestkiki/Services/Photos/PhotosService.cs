using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Oukilestkiki.Models;
using Oukilestkiki.ViewModels;
using WebGrease.Css.Extensions;

namespace Oukilestkiki.Services.Photos
{
    public class PhotosService
    {
        private Context db = new Context();

        public List<Photo> Add(PhotoRechercheModel prm)
        {
            if (!prm.Photos.Any())
            {
                return null;
            }

            var photos = new List<Photo>();

            foreach (var p in prm.Photos)
            {
                var photo = FileManagerService.Upload(p);
                if (photo == null)
                {
                    return null;
                }

                if (prm.Recherche != null)
                {
                    photo.Recherches.Add(prm.Recherche);
                }

                photo.Animal = prm.Animal;

                db.Photos.Add(photo);

                photos.Add(photo);
            }

            db.SaveChanges();

            return photos;
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

        public void DownloadPhotosRecherche(Recherche r)
        {
            var photos = db.Photos.Where(p => p.Recherches.Select(rr => rr.Id).Contains(r.Id)).ToList();

            using (var fileStream = new FileStream($@"C:\temp\photos_recherche_{r.Id}_{DateTime.Now.ToString("ddMMyyyssmmHH")}.zip", FileMode.CreateNew))
            {
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
                {
                    foreach (var photo in photos)
                    {
                        var zipArchiveEntry = archive.CreateEntry(photo.FileName, CompressionLevel.Fastest);
                        var fileBytes = File.ReadAllBytes(photo.FilePath);
                        using (var zipStream = zipArchiveEntry.Open())
                        {
                            zipStream.Write(fileBytes, 0, fileBytes.Length);
                        }
                    }
                }
            }
        }
    }
}
