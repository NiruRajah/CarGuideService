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

        /*[HttpGet("yo")]
        public ActionResult<List<Shipwreck>> GetYo() =>
            _carGuideAPIService.GetYo();

        [HttpGet]
        public ActionResult<List<Shipwreck>> Get() =>
            _carGuideAPIService.Get();*/


        #region Vehicle

        [HttpGet("vehicle/all")]
        public ActionResult<List<Vehicle>> GetAllVehicles() => _carGuideAPIService.GetAllVehicles();

        [HttpGet("vehicle/{id}")]
        public ActionResult<Vehicle> GetVehicle(string id) => _carGuideAPIService.GetVehicle(id);

        [HttpPost("vehicle")]
        public ActionResult<Vehicle> CreateVehicle(Vehicle vehicle) =>_carGuideAPIService.CreateVehicle(vehicle);

        [HttpPut("vehicle/{id}")]
        public ActionResult<Vehicle> UpdateVehicle(string id, Vehicle vehicle) => _carGuideAPIService.UpdateVehicle(id, vehicle);

        [HttpDelete("vehicle/{id}")]
        public ActionResult<string> DeleteVehicle(string id) => _carGuideAPIService.RemoveVehicle(id);

        #endregion Vehicle


        #region VehicleReview

        [HttpGet("vehiclereview/all")]
        public ActionResult<List<VehicleReview>> GetAllVehicleReviews() => _carGuideAPIService.GetAllVehicleReviews();

        [HttpGet("vehiclereview/{id}")]
        public ActionResult<VehicleReview> GetVehicleReview(string id) => _carGuideAPIService.GetVehicleReview(id);

        [HttpPost("vehiclereview")]
        public ActionResult<VehicleReview> CreateVehicleReview(VehicleReview vehicleReview) => 
            _carGuideAPIService.CreateVehicleReview(vehicleReview);

        [HttpDelete("vehiclereview/{id}")]
        public ActionResult<string> DeleteVehicleReview(string id) => 
            _carGuideAPIService.RemoveVehicleReview(id);

        #endregion VehicleReview


        #region User
        [HttpGet("user")]
        public ActionResult<List<User>> GetAllUsers() => _carGuideAPIService.GetAllUsers();

        [HttpGet("user/{username}")]
        public ActionResult<User> GetUser(string username) => _carGuideAPIService.GetUser(username);

        [HttpPost("user")]
        public ActionResult<User> CreateUser(User user) => _carGuideAPIService.CreateUser(user);

        [HttpPut("user/{username}")]
        public ActionResult<User> UpdateUser(string username, User user) => _carGuideAPIService.UpdateUser(username, user);

        [HttpDelete("user/{username}")]
        public ActionResult<string> RemoveUser(string username) => _carGuideAPIService.RemoveUser(username);

        #endregion User






        /*
         [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }
         */
    }
}
