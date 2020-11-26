using CarGuideServiceAPI.Models;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Services
{
    public class CarGuideServiceAPIService
    {
        //private readonly IMongoCollection<Shipwreck> _carGuideServiceAPIs;
        private readonly IMongoCollection<Vehicle> _vehicles;
        private readonly IMongoCollection<VehicleReview> _vehicleReviews;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Maintenance> _maintenances;
        private readonly IMongoCollection<Recall> _recalls;
        private readonly IMongoCollection<Warranty> _warrantys;


        public CarGuideServiceAPIService(ICarGuideServiceAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            // _carGuideServiceAPIs = database.GetCollection<Shipwreck>("shipwrecks");
            _vehicles = database.GetCollection<Vehicle>("vehicles");
            _vehicleReviews = database.GetCollection<VehicleReview>("vehicleReviews");
            _users = database.GetCollection<User>("users");
            _maintenances = database.GetCollection<Maintenance>("maintenances");
            _recalls = database.GetCollection<Recall>("recalls");
            _warrantys = database.GetCollection<Warranty>("warrantys");

        }

        /*public List<Shipwreck> GetYo() =>
            _carGuideServiceAPIs.Find(s=> s.Featuretype == "Wrecks - Visible" && s.Chart == "US,U1,graph,DNC H1409860").ToList();

        public List<Shipwreck> Get() =>
            _carGuideServiceAPIs.Find(s => s.Featuretype == "Wrecks - Visible").ToList();*/

        public List<Vehicle> GetAllVehicles() => _vehicles.Find(v => true).ToList();

        public Vehicle GetVehicleById(string id) =>
            _vehicles.Find(v => v.Id == id).FirstOrDefault();

        public Vehicle GetVehicle(int year, string make, string model) =>
            _vehicles.Find(v => v.Year == year && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();

        public List<Vehicle> GetVehiclesByCategory(int year, LuxuryLevel luxuryLevel, VehicleSize size, VehicleType type, PriceRange priceRange)
        {
            List<Vehicle> vehicles = _vehicles.Find(v => v.Year == year
                                               && v.LuxuryLevel == luxuryLevel
                                               && v.Size == size
                                               && v.Type == type
                                               && v.PriceRange == priceRange).ToList();
            return vehicles;
        }

        public List<Vehicle> GetVehiclesBasedOffCriterias(RequestedVehicleCriterias requestedVehicleCriterias)
        {
            List<Vehicle> vehicles = GetVehiclesByCategory(requestedVehicleCriterias.Year, 
                                                           requestedVehicleCriterias.LuxuryLevel, 
                                                           requestedVehicleCriterias.Size,
                                                           requestedVehicleCriterias.Type,
                                                           requestedVehicleCriterias.PriceRange);
            return vehicles;
        }

        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            _vehicles.InsertOne(vehicle);
            return vehicle;
        }

        public Vehicle UpdateVehicle(string id, Vehicle vehicle)
        {
            _vehicles.ReplaceOne(v => v.Id == id, vehicle);
            return vehicle;
        }
            

        public string RemoveVehicle(string id)
        {
            _vehicles.DeleteOne(v => v.Id == id);
            return id;
        }

        public bool VehicleExists(int year, string make, string model)
        {
            Vehicle vehicleInDb = GetVehicle(year, make, model);
            if (vehicleInDb == null)
            {
                return false;
            }
            return true;
        }


        public List<VehicleReview> GetAllVehicleReviews() =>
            _vehicleReviews.Find(v => true).ToList();

        public VehicleReview GetVehicleReview(string id) =>
            _vehicleReviews.Find(v => v.Id == id).FirstOrDefault();

        public List<VehicleReview> GetVehicleReviewsOfUser(string username) =>
            _vehicleReviews.Find(v => v.UserName.Equals(username)).ToList();

        public List<VehicleReview> GetVehicleReviewsOfVehicle(int year, string make, string model) =>
            _vehicleReviews.Find(v => v.Year == year && v.Make.Equals(make) && v.Model.Equals(model)).ToList();

        public VehicleReview CreateVehicleReview (VehicleReview vehicleReview)
        {
            _vehicleReviews.InsertOne(vehicleReview);
            return vehicleReview;
        }

        public string RemoveVehicleReview(string id)
        {
            _vehicleReviews.DeleteOne(v => v.Id == id);
            return id;
        }

        public string RemoveVehicleReviewsOfVehicle(int year, string make, string model)
        {
            List<VehicleReview> vehicleReviews = GetVehicleReviewsOfVehicle(year, make, model);
            if (vehicleReviews.Count() == 0)
            {
                return "No Reviews Exist For This Vehicle";
            }
            else
            {
                for (int i = 0; i < vehicleReviews.Count(); i++)
                {
                    RemoveVehicleReview(vehicleReviews[i].Id);
                    Vehicle vehicle = GetVehicle(vehicleReviews[i].Year, vehicleReviews[i].Make, vehicleReviews[i].Model);
                    if (vehicle != null)
                    {
                        vehicle.NumberOfReviews--;
                        vehicle.FuelEfficiency = (vehicle.FuelEfficiency * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].FuelEfficiency) / vehicle.NumberOfReviews;
                        vehicle.Power = (vehicle.Power * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Power) / vehicle.NumberOfReviews;
                        vehicle.Handling = (vehicle.Handling * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Handling) / vehicle.NumberOfReviews;
                        vehicle.Safety = (vehicle.Safety * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Safety) / vehicle.NumberOfReviews;
                        vehicle.Reliability = (vehicle.Reliability * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Reliability) / vehicle.NumberOfReviews;
                        vehicle.SteeringFeelAndResponse = (vehicle.SteeringFeelAndResponse * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].SteeringFeelAndResponse) / vehicle.NumberOfReviews;
                        vehicle.ComfortLevel = (vehicle.ComfortLevel * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].ComfortLevel) / vehicle.NumberOfReviews;
                        vehicle.RideQuality = (vehicle.RideQuality * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].RideQuality) / vehicle.NumberOfReviews;
                        vehicle.BuildQuality = (vehicle.BuildQuality * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].BuildQuality) / vehicle.NumberOfReviews;
                        vehicle.Technology = (vehicle.Technology * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Technology) / vehicle.NumberOfReviews;
                        vehicle.Styling = (vehicle.Styling * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].Styling) / vehicle.NumberOfReviews;
                        vehicle.ResaleValue = (vehicle.ResaleValue * (vehicle.NumberOfReviews + 1) - vehicleReviews[i].ResaleValue) / vehicle.NumberOfReviews;

                        UpdateVehicle(vehicle.Id, vehicle);
                    }
                }
            }
            return "Deleted Vehicle Reviews Of The Vehicle: " + year + " " + make + " " + model;
        }

        public bool UserAlreadyCreatedReviewForVehicleExists(VehicleReview vehicleReview)
        {
            VehicleReview foundVReview = _vehicleReviews.Find(v => v.Year == vehicleReview.Year 
                                                                && v.Make.Equals(vehicleReview.Make) 
                                                                && v.Model.Equals(vehicleReview.Model) 
                                                                && v.UserName.Equals(vehicleReview.UserName)).FirstOrDefault();
            if(foundVReview == null)
            {
                return false;
            }

            return true;
        }


            

        public List<User> GetAllUsers() =>
            _users.Find(u => true).ToList();

        public User GetUserByUserName(string userName) =>
            _users.Find(u => u.UserName.Equals(userName)).FirstOrDefault();

        public User GetUserByEmail(string email) =>
            _users.Find(u => u.Email.Equals(email)).FirstOrDefault();

        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User UpdateUser(string username, User user)
        {
            _users.ReplaceOne(u => u.UserName.Equals(username), user);
            return user;
        }

        public string RemoveUser(string userName)
        {
            _users.DeleteOne(u => u.UserName.Equals(userName));
            return userName;

        }

        public bool UserNameExists(string username)
        {
            User userInDb = GetUserByUserName(username);
            if (userInDb == null)
            {
                return false;
            }
            return true;
        }

        public bool EmailExists(string email)
        {
            User userInDb = GetUserByEmail(email);
            if (userInDb == null)
            {
                return false;
            }
            return true;
        }

        public bool LoginUser(string username, string password)
        {
            User userInDb = GetUserByUserName(username);
            if (userInDb == null)
            {
                return false;
            }
            else if(userInDb.UserName.Equals(username) && userInDb.Password.Equals(password))
            {
                return true;
            }

            return false;
        }



        //public List<Maintenance> GetAllMaintenances() => _maintenances.Find(v => true).ToList();

        public Maintenance GetMaintenanceOfVehicle(string year, string make, string model) =>
            _maintenances.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();

        public string CreateMaintenance(Maintenance maintenance)
        {
            _maintenances.InsertOne(maintenance);
            return maintenance.MaintenanceInfo.ToString();
        }

        //public List<Recall> GetAllRecalls() => _recalls.Find(v => true).ToList();

        public Recall GetRecallOfVehicle(string year, string make, string model) =>
            _recalls.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();

        public string CreateRecall(Recall recall)
        {
            _recalls.InsertOne(recall);
            return recall.RecallInfo.ToString();
        }

        //public List<Warranty> GetAllWarrantys() => _warrantys.Find(v => true).ToList();

        public Warranty GetWarrantyOfVehicle(string year, string make, string model) =>
            _warrantys.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();


        public string CreateWarranty(Warranty warranty)
        {
            _warrantys.InsertOne(warranty);
            return warranty.WarrantyInfo.ToString();
        }

    }
}
