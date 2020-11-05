using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceGuideAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarServiceGuideAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMongoCollection<Shipwreck> _shipwreckCollection;

        public WeatherForecastController(IMongoClient client)
        {
            var database = client.GetDatabase("sample_geospatial");
            _shipwreckCollection = database.GetCollection<Shipwreck>("shipwrecks");
        }

        [HttpGet("yo")]
        public IEnumerable<Shipwreck> GetYo()
        {
            return _shipwreckCollection.Find(s=> s.Featuretype == "Wrecks - Visible" && s.Chart == "US,U1,graph,DNC H1409860").ToList();
        }

        [HttpGet]
        public IEnumerable<Shipwreck> Get()
        {
            return _shipwreckCollection.Find(s => s.Featuretype == "Wrecks - Visible").ToList();
        }
    }
}
