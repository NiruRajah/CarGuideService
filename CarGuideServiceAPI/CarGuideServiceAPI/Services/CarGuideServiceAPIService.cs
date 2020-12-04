﻿using CarGuideServiceAPI.Models;
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

            _vehicles = database.GetCollection<Vehicle>("vehicles");
            _vehicleReviews = database.GetCollection<VehicleReview>("vehicleReviews");
            _users = database.GetCollection<User>("users");
            _maintenances = database.GetCollection<Maintenance>("maintenances");
            _recalls = database.GetCollection<Recall>("recalls");
            _warrantys = database.GetCollection<Warranty>("warrantys");

        }

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

        public double GetScore(List<KeyValuePair<string, double>> vehicle, List<KeyValuePair<string, double>> requestCritierias)
        {
            double avg = 0;
            double total = 0;
            double length = vehicle.Count();
            double weight = 0;

            for (int i = 0; i < vehicle.Count(); i++)
            {
                if (vehicle[i].Value >= requestCritierias[i].Value)
                {
                    weight = 10 - (vehicle[i].Value - requestCritierias[i].Value);

                    if (weight < 5)
                    {
                        weight = 5;
                    }

                }
                else
                {
                    weight = 10 - 2 * (requestCritierias[i].Value - vehicle[i].Value);
                }

                total += weight * vehicle[i].Value;
            }

            avg = total / length;

            return avg;
        }

        public List<KeyValuePair<string, double>> ConvertToKeyValuePairs(Type t, RequestedVehicleCriterias requestedVehicleCriterias, Vehicle vehicle)
        {
            var keyValuePairs = new List<KeyValuePair<string, double>>();

            if ((requestedVehicleCriterias != null) && (t.Name.Equals("RequestedVehicleCriterias")))
            {
                keyValuePairs.Add(new KeyValuePair<string, double>("FuelEfficiency", requestedVehicleCriterias.FuelEfficiency));
                keyValuePairs.Add(new KeyValuePair<string, double>("Power", requestedVehicleCriterias.Power));
                keyValuePairs.Add(new KeyValuePair<string, double>("Handling", requestedVehicleCriterias.Handling));
                keyValuePairs.Add(new KeyValuePair<string, double>("Safety", requestedVehicleCriterias.Safety));
                keyValuePairs.Add(new KeyValuePair<string, double>("Reliability", requestedVehicleCriterias.Reliability));
                keyValuePairs.Add(new KeyValuePair<string, double>("SteeringFeelAndResponse", requestedVehicleCriterias.SteeringFeelAndResponse));
                keyValuePairs.Add(new KeyValuePair<string, double>("ComfortLevel", requestedVehicleCriterias.ComfortLevel));
                keyValuePairs.Add(new KeyValuePair<string, double>("RideQuality", requestedVehicleCriterias.RideQuality));
                keyValuePairs.Add(new KeyValuePair<string, double>("BuildQuality", requestedVehicleCriterias.BuildQuality));
                keyValuePairs.Add(new KeyValuePair<string, double>("Technology", requestedVehicleCriterias.Technology));
                keyValuePairs.Add(new KeyValuePair<string, double>("Styling", requestedVehicleCriterias.Styling));
                keyValuePairs.Add(new KeyValuePair<string, double>("ResaleValue", requestedVehicleCriterias.ResaleValue));
            }
            else
            {
                keyValuePairs.Add(new KeyValuePair<string, double>("FuelEfficiency", vehicle.FuelEfficiency));
                keyValuePairs.Add(new KeyValuePair<string, double>("Power", vehicle.Power));
                keyValuePairs.Add(new KeyValuePair<string, double>("Handling", vehicle.Handling));
                keyValuePairs.Add(new KeyValuePair<string, double>("Safety", vehicle.Safety));
                keyValuePairs.Add(new KeyValuePair<string, double>("Reliability", vehicle.Reliability));
                keyValuePairs.Add(new KeyValuePair<string, double>("SteeringFeelAndResponse", vehicle.SteeringFeelAndResponse));
                keyValuePairs.Add(new KeyValuePair<string, double>("ComfortLevel", vehicle.ComfortLevel));
                keyValuePairs.Add(new KeyValuePair<string, double>("RideQuality", vehicle.RideQuality));
                keyValuePairs.Add(new KeyValuePair<string, double>("BuildQuality", vehicle.BuildQuality));
                keyValuePairs.Add(new KeyValuePair<string, double>("Technology", vehicle.Technology));
                keyValuePairs.Add(new KeyValuePair<string, double>("Styling", vehicle.Styling));
                keyValuePairs.Add(new KeyValuePair<string, double>("ResaleValue", vehicle.ResaleValue));
            }

            return keyValuePairs;
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
                    if (vehicle != null && vehicle.NumberOfReviews > 1 )
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
                    else if(vehicle != null && vehicle.NumberOfReviews == 1)
                    {
                        vehicle.NumberOfReviews--;
                        vehicle.FuelEfficiency = 0;
                        vehicle.Power = 0;
                        vehicle.Handling = 0;
                        vehicle.Safety = 0;
                        vehicle.Reliability = 0;
                        vehicle.SteeringFeelAndResponse = 0;
                        vehicle.ComfortLevel = 0;
                        vehicle.RideQuality = 0;
                        vehicle.BuildQuality = 0;
                        vehicle.Technology = 0;
                        vehicle.Styling = 0;
                        vehicle.ResaleValue = 0;

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



        public Maintenance GetMaintenanceOfVehicle(string year, string make, string model) =>
            _maintenances.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();

        public string CreateMaintenance(Maintenance maintenance)
        {
            _maintenances.InsertOne(maintenance);
            return maintenance.MaintenanceInfo.ToString();
        }


        public Recall GetRecallOfVehicle(string year, string make, string model) =>
            _recalls.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();

        public string CreateRecall(Recall recall)
        {
            _recalls.InsertOne(recall);
            return recall.RecallInfo.ToString();
        }


        public Warranty GetWarrantyOfVehicle(string year, string make, string model) =>
            _warrantys.Find(v => v.Year.Equals(year) && v.Make.Equals(make) && v.Model.Equals(model)).FirstOrDefault();


        public string CreateWarranty(Warranty warranty)
        {
            _warrantys.InsertOne(warranty);
            return warranty.WarrantyInfo.ToString();
        }

    }
}
