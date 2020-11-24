using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Models
{
    [BsonIgnoreExtraElements]
    public class IgnoreClass //Ignore this entire page, useless to our project. Only here for testing purposes
    {
        public Object Id { get; set; }
        
        [BsonElement("feature_type")]
        public string Featuretype { get; set; }

        [BsonElement("chart")]
        public string Chart { get; set; }

        [BsonElement("latdec")]
        public double Latitude { get; set; }

        [BsonElement("londec")]
        public double Longitude { get; set; }

    }
}
