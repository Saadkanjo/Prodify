using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Feedback;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
     [Route("api/Feedback")]
     [ApiController]
    public class FeedbackController :ControllerBase
    {
        private readonly IFeedbackRepository _FeedbackRepo;
        private readonly IProductRepository _ProductRepo;
        private readonly UserManager<AppUser> _userManager;
    
    public FeedbackController(IFeedbackRepository feedbackRepo,IProductRepository ProductRepo, UserManager<AppUser> userManager)
    {
        _FeedbackRepo=feedbackRepo;
        _ProductRepo= ProductRepo;
        _userManager=userManager;
    }
      // GET api/feedback/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var Feedback = await _FeedbackRepo.GetByIdAsync(id);
            if (Feedback == null)
            {
                return NotFound(); // Return a 404 if not found
            }

            return Ok(Feedback.ToFeedbackDto());
        }
        // POST api/feedback/{stockId}
        [HttpPost("{ProductId}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int ProductId, [FromBody] CreateFeedbackDto feedbackDto)
        {
            if (!await _ProductRepo.ProductExists(ProductId))
            {
                return BadRequest("Product does not exist");
            }
            //get the username in CreateAsync
              var username = User.GetUsername();

           //get the user from the DB
            var appUser = await _userManager.FindByNameAsync(username);

            var feedbackModel = feedbackDto.ToFeedbackFromCreate(ProductId);
            feedbackModel.AppUserId=appUser.Id;
            await _FeedbackRepo.CreateAsync(feedbackModel);
            return CreatedAtAction(nameof(GetByID), new { id = feedbackModel.Id }, feedbackModel.ToFeedbackDto());
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFeedbackRequestDto updateDto)
        {
            var feedbackModel = await _FeedbackRepo.UpdateAsync(id, updateDto);
            if (feedbackModel == null)
            {
                return NotFound();
            }

            return Ok(feedbackModel.ToFeedbackDto());
        }
         // DELETE api/comment/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var feedbackModel = await _FeedbackRepo.Delete(id);
            if (feedbackModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }



}}