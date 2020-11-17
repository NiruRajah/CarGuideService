using CarGuideServiceAPI.Models;
using CarServiceGuideAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Services
{
    public class CarGuideServiceAPIService
    {
        private readonly IMongoCollection<Shipwreck> _carGuideServiceAPIs;

        public CarGuideServiceAPIService(ICarGuideServiceAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _carGuideServiceAPIs = database.GetCollection<Shipwreck>("shipwrecks");
        }

        public List<Shipwreck> GetYo() =>
            _carGuideServiceAPIs.Find(s=> s.Featuretype == "Wrecks - Visible" && s.Chart == "US,U1,graph,DNC H1409860").ToList();

        public List<Shipwreck> Get() =>
            _carGuideServiceAPIs.Find(s => s.Featuretype == "Wrecks - Visible").ToList();
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
