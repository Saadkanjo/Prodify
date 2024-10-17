using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Feedback;
using api.models;

namespace api.Interfaces
{
    public interface IFeedbackRepository
    {
      Task <List<Feedback>> GetAllAsync();  
       Task <Feedback?> GetByIdAsync(int id);   
       Task <Feedback> CreateAsync(Feedback feedbackModel);
       Task<Feedback?> UpdateAsync(int id,UpdateFeedbackRequestDto feedbackRequestDto);
       Task<Feedback?> Delete(int id);    
    }
}