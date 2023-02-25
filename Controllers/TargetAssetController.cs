using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TargetAssetController : ControllerBase
    {
        private readonly ITargetAssetService _targetAssetService;

        public TargetAssetController(ITargetAssetService targetAssetService)
        {
            _targetAssetService = targetAssetService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var targetAssets = await _targetAssetService.GetTargetAssets();
            return Ok(targetAssets);
            
        }
    }
}
