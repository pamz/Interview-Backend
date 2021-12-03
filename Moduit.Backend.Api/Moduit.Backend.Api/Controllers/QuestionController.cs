using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moduit.Backend.Api.CoreBusiness;
using Moduit.Backend.Api.CoreBusiness.Interfaces;
using Moduit.Backend.Api.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduit.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class QuestionController : ControllerBase
    {
        public readonly IQuestionRepository _repository;
        public QuestionController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("one")]
        public async Task<IActionResult> One()
        {
            var response = await _repository.GetQuestionOneAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("two")]
        public async Task<IActionResult> Two([FromQuery] QuestionQuery query)
        {
            var title = query.Title ?? "Ergonomics";
            var tags = query.Tags ?? "Sports";
            var pageSize = query.PageSize.HasValue ? query.PageSize : 3;
            var response = await _repository.GetQuestionTwoAsync(title, tags, (int)pageSize).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost]
        [Route("three")]
        public async Task<IActionResult> Three([FromBody] BackendQuestionThreeResponse model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid Data Model");
                var response = await _repository.GetQuestionThreeAsync(model).ConfigureAwait(false);
                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
