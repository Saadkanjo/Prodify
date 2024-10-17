using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Feedback
{
    public class UpdateFeedbackRequestDto
    {
        public string Comment {get;set;}=string.Empty;
    }
}