using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Feedback;
using api.models;

namespace api.Mappers
{
  public static class FeedbackMappers
  {
    public static FeedbackDto ToFeedbackDto(this Feedback feedbackDto)
    {
      return new FeedbackDto
      {
        Id = feedbackDto.Id,
        Comment = feedbackDto.Comment,
        CreatedOn = feedbackDto.CreatedOn,
        ProductId = feedbackDto.ProductId,
        CreatedBy = feedbackDto.AppUser.UserName
      };
    }
    public static Feedback ToFeedbackFromCreate(this CreateFeedbackDto feedbackDto, int ProductId)
    {
      return new Feedback
      {

        Comment = feedbackDto.Comment,
        ProductId = ProductId




      };

    }
  }
}




