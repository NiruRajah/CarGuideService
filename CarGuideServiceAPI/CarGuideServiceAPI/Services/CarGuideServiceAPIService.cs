using CarGuideServiceAPI.Models;
using MongoDB.Driver;
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



        public CarGuideServiceAPIService(ICarGuideServiceAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

           // _carGuideServiceAPIs = database.GetCollection<Shipwreck>("shipwrecks");
            _vehicles = database.GetCollection<Vehicle>("vehicles");
            _vehicleReviews = database.GetCollection<VehicleReview>("vehicleReviews");
            _users = database.GetCollection<User>("users");


        }

        /*public List<Shipwreck> GetYo() =>
            _carGuideServiceAPIs.Find(s=> s.Featuretype == "Wrecks - Visible" && s.Chart == "US,U1,graph,DNC H1409860").ToList();

        public List<Shipwreck> Get() =>
            _carGuideServiceAPIs.Find(s => s.Featuretype == "Wrecks - Visible").ToList();*/

        public List<Vehicle> GetAllVehicles() => _vehicles.Find(v => true).ToList();

        public Vehicle GetVehicle(string id) =>
            _vehicles.Find(v => v.Id == id).FirstOrDefault();

        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            _vehicles.InsertOne(vehicle);
            return vehicle;
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            _vehicles.ReplaceOne(v => v.Id == vehicle.Id, vehicle);
            return vehicle;
        }
            

        public Vehicle RemoveVehicle(Vehicle vehicle)
        {
            _vehicles.DeleteOne(v => v.Id == vehicle.Id);
            return vehicle;
        }
            

        public string RemoveVehicle(string id)
        {
            _vehicles.DeleteOne(v => v.Id == id);
            return id;
        }
            

        public List<VehicleReview> GetAllVehicleReviews() =>
            _vehicleReviews.Find(v => true).ToList();

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
            

        public List<User> GetAllUsers() =>
            _users.Find(u => true).ToList();

        public User GetUser(string userName) =>
            _users.Find(u => u.UserName.Equals(userName)).FirstOrDefault();

        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public string RemoveUser(string userName)
        {
            _users.DeleteOne(u => u.UserName.Equals(userName));
            return userName;

        }
            


        /*
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);
        */
    }
}
