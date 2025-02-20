using Core.Application.Helper;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionUniversal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JsonPlaceHolderController : ControllerBase
    {
        private readonly IJsonPlaceHolderService _jsonPlaceHolderService;
        public JsonPlaceHolderController(IJsonPlaceHolderService jsonPlaceHolderService){_jsonPlaceHolderService = jsonPlaceHolderService;}

        [HttpGet("getData")]
        public async Task<IActionResult> GetData()
        {
            var data = await _jsonPlaceHolderService.GetData();
            return Ok(data);
        }


        [HttpPost("createPlaceHolder")]
        public async Task<IActionResult> Create(JsonPlaceHolderModel request)
        {
            var data = await _jsonPlaceHolderService.Create(request);
            return Ok(data);
        }
    }
}
