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
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

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


        [HttpPost("vehicle/criterias")]
        public ActionResult<Vehicle> GetVehicleBasedOffCriteria(RequestedVehicleCriterias requestedVehicleCriterias)
        {
            List<Vehicle> vehicles = _carGuideAPIService.GetVehiclesBasedOffCategory(requestedVehicleCriterias);
            Vehicle vehicle = new Vehicle();
            
            if(vehicles.Count() > 0)
            {
                vehicle = vehicles[0];
            }
            // This is where you need to put the algorithm in effect to determine the best suitable car based off the criterias
            
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
        public ActionResult<string> DeleteVehicle(string id)
        {
            Vehicle vehicle = _carGuideAPIService.GetVehicleById(id);
            if(vehicle == null)
            {
                return "Invalid Id: " + id;
            }
            else
            {
                _carGuideAPIService.RemoveVehicle(id);
                _carGuideAPIService.RemoveVehicleReviewsOfVehicle(vehicle.Year, vehicle.Make, vehicle.Model);
            }

            return "Deleted Vehicle ID: " + id;
        }

        #endregion Vehicle


        #region VehicleReview

        [HttpGet("vehiclereview/all")]
        public ActionResult<List<VehicleReview>> GetAllVehicleReviews() => _carGuideAPIService.GetAllVehicleReviews();

        [HttpGet("vehiclereview/{id}")]
        public ActionResult<VehicleReview> GetVehicleReview(string id) => _carGuideAPIService.GetVehicleReview(id);

        [HttpGet("vehiclereview/user")]
        public ActionResult <List<VehicleReview>> GetVehicleReviewsOfUser(string username) => _carGuideAPIService.GetVehicleReviewsOfUser(username);

        [HttpGet("vehiclereview/vehicle")]
        public ActionResult<List<VehicleReview>> GetVehicleReviewsOfVehicle(int year, string make, string model) => 
            _carGuideAPIService.GetVehicleReviewsOfVehicle(year, make, model);

        [HttpPost("vehiclereview")]
        public ActionResult<string> CreateVehicleReview(VehicleReview vehicleReview)
        {
            if(_carGuideAPIService.UserNameExists(vehicleReview.UserName) && _carGuideAPIService.VehicleExists(vehicleReview))
            {
                VehicleReview createdVR = _carGuideAPIService.CreateVehicleReview(vehicleReview);
                Vehicle vehicle = _carGuideAPIService.GetVehicle(vehicleReview.Year, vehicleReview.Make, vehicleReview.Model);
                vehicle.NumberOfReviews++;
                vehicle.FuelEfficiency = (vehicle.FuelEfficiency * (vehicle.NumberOfReviews - 1) + vehicleReview.FuelEfficiency) / vehicle.NumberOfReviews;
                vehicle.Power = (vehicle.Power * (vehicle.NumberOfReviews - 1) + vehicleReview.Power) / vehicle.NumberOfReviews;
                vehicle.Handling = (vehicle.Handling * (vehicle.NumberOfReviews - 1) + vehicleReview.Handling) / vehicle.NumberOfReviews;
                vehicle.Safety = (vehicle.Safety * (vehicle.NumberOfReviews - 1) + vehicleReview.Safety) / vehicle.NumberOfReviews;
                vehicle.Reliability = (vehicle.Reliability * (vehicle.NumberOfReviews - 1) + vehicleReview.Reliability) / vehicle.NumberOfReviews;
                vehicle.SteeringFeelAndResponse = (vehicle.SteeringFeelAndResponse * (vehicle.NumberOfReviews - 1) + vehicleReview.SteeringFeelAndResponse) / vehicle.NumberOfReviews;
                vehicle.ComfortLevel = (vehicle.ComfortLevel * (vehicle.NumberOfReviews - 1) + vehicleReview.ComfortLevel) / vehicle.NumberOfReviews;
                vehicle.RideQuality = (vehicle.RideQuality * (vehicle.NumberOfReviews - 1) + vehicleReview.RideQuality) / vehicle.NumberOfReviews;
                vehicle.BuildQuality = (vehicle.BuildQuality * (vehicle.NumberOfReviews - 1) + vehicleReview.BuildQuality) / vehicle.NumberOfReviews;
                vehicle.Technology = (vehicle.Technology * (vehicle.NumberOfReviews - 1) + vehicleReview.Technology) / vehicle.NumberOfReviews;
                vehicle.Styling = (vehicle.Styling * (vehicle.NumberOfReviews - 1) + vehicleReview.Styling) / vehicle.NumberOfReviews;
                vehicle.ResaleValue = (vehicle.ResaleValue * (vehicle.NumberOfReviews - 1) + vehicleReview.ResaleValue) / vehicle.NumberOfReviews;

                _carGuideAPIService.UpdateVehicle(vehicle.Id, vehicle);
                return "Review Created ID: " + createdVR.Id;
            }
            
            return "Invalid Username Or Vehicle Info";
        }

        [HttpDelete("vehiclereview/{id}")]
        public ActionResult<string> DeleteVehicleReview(string id)
        {
            VehicleReview vehicleReview = _carGuideAPIService.GetVehicleReview(id);
            if(vehicleReview == null)
            {
                return "Invalid ID: " + id;
            }
            else
            {
                _carGuideAPIService.RemoveVehicleReview(id);
                Vehicle vehicle = _carGuideAPIService.GetVehicle(vehicleReview.Year, vehicleReview.Make, vehicleReview.Model);
                if(vehicle != null)
                {
                    vehicle.NumberOfReviews--;
                    vehicle.FuelEfficiency = (vehicle.FuelEfficiency * (vehicle.NumberOfReviews + 1) - vehicleReview.FuelEfficiency) / vehicle.NumberOfReviews;
                    vehicle.Power = (vehicle.Power * (vehicle.NumberOfReviews + 1) - vehicleReview.Power) / vehicle.NumberOfReviews;
                    vehicle.Handling = (vehicle.Handling * (vehicle.NumberOfReviews + 1) - vehicleReview.Handling) / vehicle.NumberOfReviews;
                    vehicle.Safety = (vehicle.Safety * (vehicle.NumberOfReviews + 1) - vehicleReview.Safety) / vehicle.NumberOfReviews;
                    vehicle.Reliability = (vehicle.Reliability * (vehicle.NumberOfReviews + 1) - vehicleReview.Reliability) / vehicle.NumberOfReviews;
                    vehicle.SteeringFeelAndResponse = (vehicle.SteeringFeelAndResponse * (vehicle.NumberOfReviews + 1) - vehicleReview.SteeringFeelAndResponse) / vehicle.NumberOfReviews;
                    vehicle.ComfortLevel = (vehicle.ComfortLevel * (vehicle.NumberOfReviews + 1) - vehicleReview.ComfortLevel) / vehicle.NumberOfReviews;
                    vehicle.RideQuality = (vehicle.RideQuality * (vehicle.NumberOfReviews + 1) - vehicleReview.RideQuality) / vehicle.NumberOfReviews;
                    vehicle.BuildQuality = (vehicle.BuildQuality * (vehicle.NumberOfReviews + 1) - vehicleReview.BuildQuality) / vehicle.NumberOfReviews;
                    vehicle.Technology = (vehicle.Technology * (vehicle.NumberOfReviews + 1) - vehicleReview.Technology) / vehicle.NumberOfReviews;
                    vehicle.Styling = (vehicle.Styling * (vehicle.NumberOfReviews + 1) - vehicleReview.Styling) / vehicle.NumberOfReviews;
                    vehicle.ResaleValue = (vehicle.ResaleValue * (vehicle.NumberOfReviews + 1) - vehicleReview.ResaleValue) / vehicle.NumberOfReviews;

                    _carGuideAPIService.UpdateVehicle(vehicle.Id, vehicle);
                }
                
                return "Deleted Vehicle Review ID: " + id;
            }
            
        }

        [HttpDelete("vehiclereview/vehicle")]
        public ActionResult<string> DeleteVehicleReviewsOfVehicle(int year, string make, string model)
        {
            string result = _carGuideAPIService.RemoveVehicleReviewsOfVehicle(year, make, model);
            return result;
        }


        #endregion VehicleReview


        #region User
        [HttpGet("user/all")]
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

        [HttpGet("dataapi")]
        public async Task<string> CallDataAPIMaintenanceInfo(string year, string make, string model, string mileage)
        {
            string url = "https://api.carmd.com/v3.0/maint?year=" + year + "&make=" + make + "&model=" + model + "&mileage=" + mileage;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    //Content = new StringContent(JsonConvert.SerializeObject())
                };
                string contentType = "application/json";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                var authPassword = "ZjM3YmY0OGMtNzJkNy00MmY0LTgzYTQtZGIyY2UxNjdmZWRj";
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", authPassword));
                var partnerTokenValue = "5e6583fe22c146f98a7a74a86c57351a";
                client.DefaultRequestHeaders.Add("partner-token", partnerTokenValue);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                
                //string jsonString = JsonConvert.DeserializeObject<string>(responseBody);

                var jObj = JObject.Parse(responseBody);
                string jObjString = jObj.ToString();
                return jObjString;
            }
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
