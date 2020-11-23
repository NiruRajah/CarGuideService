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
        /*
        [HttpGet("vehicle/{id}")]
        public ActionResult<Vehicle> GetVehicle(string id) => _carGuideAPIService.GetVehicle(id);*/

        [HttpGet("vehicle")]
        public ActionResult<Vehicle> GetVehicle(int year, string make, string model) => 
            _carGuideAPIService.GetVehicle(year, make, model);


        [HttpGet("vehicle/RequestedVehicleCriterias")]
        public ActionResult<Vehicle> GetVehicleBasedOffCriteria(RequestedVehicleCriterias requestedVehicleCriterias)
        {
            List<Vehicle> vehicles = _carGuideAPIService.GetVehiclesBasedOffCategory(requestedVehicleCriterias);
            Vehicle vehicle = new Vehicle();
            
            if(vehicles.Count() > 0)
            {
                vehicle = vehicles[0];
            }
            
            
            return vehicle;
        }

        //create a new httpget based off requestedvehiclecriterias
        //returns a list of all the cars in that class
        //now design an algorithm to get the best fit car based off the criteria
        // returns the best fit vehicle

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

        [HttpGet("vehiclereview/user")]
        public ActionResult <List<VehicleReview>> GetVehicleReviewsOfUser(string username) => _carGuideAPIService.GetVehicleReviewsOfUser(username);

        [HttpPost("vehiclereview")]
        public ActionResult<VehicleReview> CreateVehicleReview(VehicleReview vehicleReview)
        {
            _carGuideAPIService.CreateVehicleReview(vehicleReview);
            Vehicle vehicle = _carGuideAPIService.GetVehicle(vehicleReview.Year, vehicleReview.Make, vehicleReview.Model);
            vehicle.NumberOfReviews++;
            vehicle.FuelEfficiency = (vehicle.FuelEfficiency + vehicleReview.FuelEfficiency)/ vehicle.NumberOfReviews;
            vehicle.Power = (vehicle.Power + vehicleReview.Power) / vehicle.NumberOfReviews;
            vehicle.Handling = (vehicle.Handling + vehicleReview.Handling) / vehicle.NumberOfReviews;
            vehicle.Safety = (vehicle.Safety + vehicleReview.Safety) / vehicle.NumberOfReviews;
            vehicle.Reliability = (vehicle.Reliability + vehicleReview.Reliability) / vehicle.NumberOfReviews;
            vehicle.SteeringFeelAndResponse = (vehicle.SteeringFeelAndResponse + vehicleReview.SteeringFeelAndResponse) / vehicle.NumberOfReviews;
            vehicle.ComfortLevel = (vehicle.ComfortLevel + vehicleReview.ComfortLevel) / vehicle.NumberOfReviews;
            vehicle.RideQuality = (vehicle.RideQuality + vehicleReview.RideQuality) / vehicle.NumberOfReviews;
            vehicle.BuildQuality = (vehicle.BuildQuality + vehicleReview.BuildQuality) / vehicle.NumberOfReviews;
            vehicle.Technology = (vehicle.Technology + vehicleReview.Technology) / vehicle.NumberOfReviews;
            vehicle.Styling = (vehicle.Styling + vehicleReview.Styling) / vehicle.NumberOfReviews;
            vehicle.ResaleValue = (vehicle.ResaleValue + vehicleReview.ResaleValue) / vehicle.NumberOfReviews;
            
            _carGuideAPIService.UpdateVehicle(vehicle.Id, vehicle);
            return vehicleReview;
        }

        [HttpDelete("vehiclereview/{id}")]
        public ActionResult<string> DeleteVehicleReview(string id)
        {
            VehicleReview vehicleReview = _carGuideAPIService.GetVehicleReview(id);
            _carGuideAPIService.RemoveVehicleReview(id);
            Vehicle vehicle = _carGuideAPIService.GetVehicle(vehicleReview.Year, vehicleReview.Make, vehicleReview.Model);
            vehicle.NumberOfReviews--;
            vehicle.FuelEfficiency = (vehicle.FuelEfficiency - vehicleReview.FuelEfficiency) / vehicle.NumberOfReviews;
            vehicle.Power = (vehicle.Power - vehicleReview.Power) / vehicle.NumberOfReviews;
            vehicle.Handling = (vehicle.Handling - vehicleReview.Handling) / vehicle.NumberOfReviews;
            vehicle.Safety = (vehicle.Safety - vehicleReview.Safety) / vehicle.NumberOfReviews;
            vehicle.Reliability = (vehicle.Reliability - vehicleReview.Reliability) / vehicle.NumberOfReviews;
            vehicle.SteeringFeelAndResponse = (vehicle.SteeringFeelAndResponse - vehicleReview.SteeringFeelAndResponse) / vehicle.NumberOfReviews;
            vehicle.ComfortLevel = (vehicle.ComfortLevel - vehicleReview.ComfortLevel) / vehicle.NumberOfReviews;
            vehicle.RideQuality = (vehicle.RideQuality - vehicleReview.RideQuality) / vehicle.NumberOfReviews;
            vehicle.BuildQuality = (vehicle.BuildQuality - vehicleReview.BuildQuality) / vehicle.NumberOfReviews;
            vehicle.Technology = (vehicle.Technology - vehicleReview.Technology) / vehicle.NumberOfReviews;
            vehicle.Styling = (vehicle.Styling - vehicleReview.Styling) / vehicle.NumberOfReviews;
            vehicle.ResaleValue = (vehicle.ResaleValue - vehicleReview.ResaleValue) / vehicle.NumberOfReviews;

            _carGuideAPIService.UpdateVehicle(vehicle.Id, vehicle);
            return id;
        }
            

        #endregion VehicleReview


        #region User
        [HttpGet("user")]
        public ActionResult<List<User>> GetAllUsers() => _carGuideAPIService.GetAllUsers();

        [HttpGet("user/{username}")]
        public ActionResult<User> GetUser(string username) => _carGuideAPIService.GetUserByUserName(username);

        [HttpPost("user")]
        public ActionResult<string> CreateUser(User user)
        {
            if(!_carGuideAPIService.UserNameExists(user.UserName) && !_carGuideAPIService.EmailExists(user.Email))
            {
                _carGuideAPIService.CreateUser(user);
                return "Created User";
            }
            

            return "Username or Email Already Exists";
        }

        [HttpPut("user/{username}")]
        public ActionResult<string> UpdateUser(string username, User user)
        {
            if(_carGuideAPIService.UserNameExists(username))
            {
                _carGuideAPIService.UpdateUser(username, user);
                return "Updated User";
            }
            
            return "Username Not Found";
        }


        [HttpDelete("user/{username}")]
        public ActionResult<string> RemoveUser(string username)
        {
            if (_carGuideAPIService.UserNameExists(username))
            {
                _carGuideAPIService.RemoveUser(username);
                return "Deleted User";
            }

            return "Username Not Found";
        }

        #endregion User


        [HttpGet("user/exists/username/{username}")]
        public ActionResult<bool> UserNameExists(string username) =>
            _carGuideAPIService.UserNameExists(username);

        [HttpGet("user/exists/email/{email}")]
        public ActionResult<bool> EmailExists(string email) =>
            _carGuideAPIService.EmailExists(email);

        [HttpPost("user/login")]
        public ActionResult<string> LoginUser(string username, string password)
        {
            if(_carGuideAPIService.LoginUser(username, password))
            {
                return "Logged In Successfully";
            }

            return "Incorrect Username or Password";
        }
            



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
