using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using CarGuideServiceAPI.Services;
using CarGuideServiceAPI.Models;

namespace CarGuideServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarGuideServiceAPIController : ControllerBase
    {
        private readonly CarGuideServiceAPIService _carGuideAPIService;

        public CarGuideServiceAPIController(CarGuideServiceAPIService carGuideServiceAPIService)
        {
            _carGuideAPIService = carGuideServiceAPIService;
        }

        [HttpGet("yo")]
        public ActionResult<List<Shipwreck>> GetYo() =>
            _carGuideAPIService.GetYo();

        [HttpGet]
        public ActionResult<List<Shipwreck>> Get() =>
            _carGuideAPIService.Get();
    }
}
