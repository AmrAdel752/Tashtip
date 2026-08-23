using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;
using TASHTIP.InfraDB.ContextDB;

namespace TASHTIP.RepoUOWCore.Services
{
    /// <summary>Customer reviews: submission, public listing of approved ones, and admin moderation.</summary>
    public class ReviewsService
    {
        private readonly GeneralDBContext _db;

        public ReviewsService(GeneralDBContext db)
        {
            _db = db;
        }

        public async Task SubmitAsync(Review review)
        {
            review.IsApproved = false;
            _db.Review.Add(review);
            await _db.SaveChangesAsync();
        }

        public Task<List<Review>> GetApprovedAsync(int take = 12)
        {
            return _db.Review
                .Where(r => r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .Take(take)
                .ToListAsync();
        }

        public Task<List<Review>> GetPendingAsync()
        {
            return _db.Review
                .Where(r => !r.IsApproved)
                .OrderBy(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ApproveAsync(int id)
        {
            var review = await _db.Review.FindAsync(id);
            if (review == null) return false;
            review.IsApproved = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAsync(int id)
        {
            var review = await _db.Review.FindAsync(id);
            if (review == null) return false;
            _db.Review.Remove(review);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
