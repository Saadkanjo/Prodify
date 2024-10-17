using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.Feedback;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDBContext _context;
        public FeedbackRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Feedback> CreateAsync(Feedback feedbackModel)
        {
            await _context.Feedback.AddAsync(feedbackModel);
              await _context.SaveChangesAsync();
              return feedbackModel;
        }

        public async Task<Feedback?> Delete(int id)
        {
            var feedbackModel= await _context.Feedback.FirstOrDefaultAsync(x=>x.Id==id);
           if (feedbackModel==null){
                return null;
            
            }
            _context.Feedback.Remove(feedbackModel);
             await _context.SaveChangesAsync();
            return feedbackModel;
        }

        public  async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedback.Include(a => a.AppUser).ToListAsync();
           
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedback.Include(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
            
        }

        public async Task<Feedback?> UpdateAsync(int id, UpdateFeedbackRequestDto feedbackRequestDto)
        {
            var existingFeedback= await _context.Feedback.FindAsync(id);
             if (existingFeedback==null){
                return null;
             }
            existingFeedback.Comment=feedbackRequestDto.Comment;
             await _context.SaveChangesAsync();
             return existingFeedback;
        }
    }
}