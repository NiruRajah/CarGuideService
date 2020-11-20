using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Models
{
    public class VehicleReview
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string VehicleId { get; set; }

        public double FuelEfficiency { get; set; }

        public double Power { get; set; }

        public double Handling { get; set; }

        public double Safety { get; set; }

        public double Reliability { get; set; }

        public double SteeringFeelAndResponse { get; set; }

        public double ComfortLevel { get; set; }

        public double RideQuality { get; set; }

        public double BuildQuality { get; set; }

        public double Technology { get; set; }

        public double Styling { get; set; }

        public double ResaleValue { get; set; }
    }
}
