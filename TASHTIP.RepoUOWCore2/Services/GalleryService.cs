using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    /// <summary>CRUD for units in the portfolio gallery, plus the extra 2D/3D/VR media attached to each.</summary>
    public class GalleryService
    {
        private readonly GeneralDBContext _db;

        public GalleryService(GeneralDBContext db)
        {
            _db = db;
        }

        public Task<List<BussinessGallary>> GetAllAsync()
        {
            return _db.BussinessGallary.OrderByDescending(g => g.ID).ToListAsync();
        }

        public Task<BussinessGallary?> GetByIdAsync(int id)
        {
            return _db.BussinessGallary.FirstOrDefaultAsync(g => g.ID == id);
        }

        public Task<List<BussinessGallaryImage>> GetImagesAsync(int galleryId)
        {
            return _db.BussinessGallaryImage
                .Where(i => i.BussinessGallaryID == galleryId)
                .OrderBy(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task AddImageAsync(int galleryId, string imagePath, int sortOrder = 0)
        {
            _db.BussinessGallaryImage.Add(new BussinessGallaryImage
            {
                BussinessGallaryID = galleryId,
                ImagePath = imagePath,
                SortOrder = sortOrder
            });
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _db.BussinessGallaryImage.FindAsync(imageId);
            if (image == null) return false;
            _db.BussinessGallaryImage.Remove(image);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMediaPathsAsync(int galleryId, string? model3DPath, string? panorama360Path)
        {
            var gallery = await _db.BussinessGallary.FirstOrDefaultAsync(g => g.ID == galleryId);
            if (gallery == null) return false;

            if (model3DPath != null) gallery.Model3DPath = model3DPath;
            if (panorama360Path != null) gallery.Panorama360Path = panorama360Path;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gallery = await _db.BussinessGallary.FirstOrDefaultAsync(g => g.ID == id);
            if (gallery == null) return false;

            var images = await _db.BussinessGallaryImage.Where(i => i.BussinessGallaryID == id).ToListAsync();
            _db.BussinessGallaryImage.RemoveRange(images);
            _db.BussinessGallary.Remove(gallery);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
