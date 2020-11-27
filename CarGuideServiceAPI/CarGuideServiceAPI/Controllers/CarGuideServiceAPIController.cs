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
using System.IO;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Text;

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

        [HttpGet("vehicle/category")]
        public ActionResult<List<Vehicle>> GetVehicleBasedOffCriteria(int year, LuxuryLevel luxuryLevel, VehicleSize size, VehicleType type, PriceRange priceRange)
        {
            List<Vehicle> vehicles = _carGuideAPIService.GetVehiclesByCategory(year, luxuryLevel, size, type, priceRange);

            return vehicles;
        }

        [HttpPost("vehicle/criterias")]
        public ActionResult<Vehicle> GetVehicleBasedOffCriterias(RequestedVehicleCriterias requestedVehicleCriterias)
        {
            List<Vehicle> vehicles = _carGuideAPIService.GetVehiclesBasedOffCriterias(requestedVehicleCriterias);
            Vehicle vehicle = new Vehicle();
            
            if(vehicles.Count() == 0)
            {
                return vehicle;
            }

            var requestedCriterias = new List<KeyValuePair<string, double>>();
            Type t = typeof(RequestedVehicleCriterias);
            requestedCriterias = _carGuideAPIService.ConvertToKeyValuePairs(t, requestedVehicleCriterias, null);

            var vehicleIndexAndScoreList = new List<KeyValuePair<int, double>>();

            for (int i = 0; i < vehicles.Count(); i++)
            {
                var localVehicle = new List<KeyValuePair<string, double>>();
                Type localT = typeof(Vehicle);
                localVehicle = _carGuideAPIService.ConvertToKeyValuePairs(localT, null, vehicles[i]);
                double score = _carGuideAPIService.GetScore(localVehicle, requestedCriterias);
                
                vehicleIndexAndScoreList.Add(new KeyValuePair<int, double>(i, score));
            }

            vehicleIndexAndScoreList = vehicleIndexAndScoreList.OrderByDescending(y => y.Value).ToList();
            vehicle = vehicles[vehicleIndexAndScoreList[0].Key];

            return vehicle;
        }

        [HttpPost("vehicle")]
        public ActionResult<string> CreateVehicle(Vehicle vehicle)
        {
            if (!_carGuideAPIService.VehicleExists(vehicle.Year, vehicle.Make, vehicle.Model))
            {
                Vehicle createdVehicle = _carGuideAPIService.CreateVehicle(vehicle);
                return "Created Vehicle ID: " + createdVehicle.Id;
            }
            
            return "Vehicle Already Exists";
        }

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
            if(_carGuideAPIService.UserAlreadyCreatedReviewForVehicleExists(vehicleReview))
            {
                return vehicleReview.UserName + " has already made a review for the " + vehicleReview.Year + " " + vehicleReview.Make + " " + vehicleReview.Model;
            }

            if(_carGuideAPIService.UserNameExists(vehicleReview.UserName) && _carGuideAPIService.VehicleExists(vehicleReview.Year, vehicleReview.Make, vehicleReview.Model))
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

        [HttpGet("dataapi/maintenance")]
        public async Task<string> CallDataAPIMaintenanceInfo(string year, string make, string model)
        {
            Maintenance maintenance = new Maintenance();
            maintenance = _carGuideAPIService.GetMaintenanceOfVehicle(year, make, model);

            if (maintenance != null)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(maintenance.MaintenanceInfo.ToJson(jsonWriterSettings));

                return json.ToString();
            }

            maintenance = new Maintenance();
            string url = "https://api.carmd.com/v3.0/maintlist?year=" + year + "&make=" + make + "&model=" + model;
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

                if(response.IsSuccessStatusCode)
                {
                    maintenance.Year = year;
                    maintenance.Make = make;
                    maintenance.Model = model;
                    maintenance.MaintenanceInfo = BsonDocument.Parse(jObjString);
                    _carGuideAPIService.CreateMaintenance(maintenance);
                }

                return jObjString;
            }
        }


        [HttpGet("dataapi/recall")]
        public async Task<string> CallDataAPIRecallInfo(string year, string make, string model)
        {
            Recall recall = new Recall();
            recall = _carGuideAPIService.GetRecallOfVehicle(year, make, model);

            if(recall != null)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(recall.RecallInfo.ToJson(jsonWriterSettings));

                return json.ToString();
            }

            recall = new Recall();
            string url = "https://api.carmd.com/v3.0/recall?year=" + year + "&make=" + make + "&model=" + model;
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

                if (response.IsSuccessStatusCode)
                {
                    recall.Year = year;
                    recall.Make = make;
                    recall.Model = model;
                    recall.RecallInfo = BsonDocument.Parse(jObjString);
                    _carGuideAPIService.CreateRecall(recall);
                }

                return jObjString;
            }
        }


        [HttpGet("dataapi/warranty")]
        public async Task<string> CallDataAPIWarrantyInfo(string year, string make, string model)
        {
            Warranty warranty = new Warranty();
            warranty = _carGuideAPIService.GetWarrantyOfVehicle(year, make, model);

            if(warranty != null)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(warranty.WarrantyInfo.ToJson(jsonWriterSettings));
                
                return json.ToString();
            }

            warranty = new Warranty();
            string url = "https://api.carmd.com/v3.0/warranty?year=" + year + "&make=" + make + "&model=" + model;
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

                if (response.IsSuccessStatusCode)
                {
                    warranty.Year = year;
                    warranty.Make = make;
                    warranty.Model = model;
                    warranty.WarrantyInfo = BsonDocument.Parse(jObjString);
                    _carGuideAPIService.CreateWarranty(warranty);
                }

                return jObjString;
            }
        }
        /*
        [HttpGet("dataapi/maintenance/all")]
        public ActionResult<List<Maintenance>> GetAllMaintenances() => _carGuideAPIService.GetAllMaintenances();

        [HttpGet("dataapi/recall/all")]
        public ActionResult<List<Recall>> GetAllRecalls() => _carGuideAPIService.GetAllRecalls();

        [HttpGet("dataapi/warranty/all")]
        public ActionResult<List<string>> GetAllWarrantys() => _carGuideAPIService.GetAllWarrantys();
        */
    }
}
