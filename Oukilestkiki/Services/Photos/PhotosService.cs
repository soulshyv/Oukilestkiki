using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;
using BO;
using Oukilestkiki.Services.Photos.Models;
using Oukilestkiki.ViewModels;
using WebGrease.Css.Extensions;

namespace Oukilestkiki.Services.Photos
{
    public class PhotosService
    {
        private Context db = new Context();

        public List<Photo> Add(PhotoRechercheViewModel prvm)
        {
            if (!prvm.Photos.Any())
            {
                return null;
            }

            var photos = new List<Photo>();

            foreach (var p in prvm.Photos)
            {
                var photo = FileManagerService.Upload(p);
                if (photo == null)
                {
                    return null;
                }

                if (prvm.Recherche != null)
                {
                    photo.Recherches.Add(prvm.Recherche);
                }

                photo.Animal = prvm.Animal;

                db.Photos.Add(photo);

                photos.Add(photo);
            }

            db.SaveChanges();

            return photos;
        }

        public List<Photo> UpdatePhotosRecherche(PhotoRechercheViewModel prvm)
        {
            DeletePhotosRecherche(prvm.Recherche);
            return Add(prvm);
        }

        public List<Photo> UpdatePhotosAnimal(PhotoRechercheViewModel prvm)
        {
            DeletePhotosAnimal(prvm.Animal);
            return Add(prvm);
        }

        public void DeletePhoto(int id)
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

        public ZipArchive DownloadPhotosRecherche(Recherche r)
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

                    return archive;
                }
            }
        }

        public PhotoFileResult DownloadPhotoById(int id)
        {
            var photo = db.Photos.Find(id);

            if (photo == null)
            {
                return null;
            }

            var fileBytes = System.IO.File.ReadAllBytes(photo.FilePath);
            return new PhotoFileResult{
                FileContent = fileBytes,
                MimeType = System.Net.Mime.MediaTypeNames.Application.Octet,
                FileName = photo.FileName
            };
        }
    }
}
