﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Models
{
    public class RequestedVehicleCriterias
    {

        public int Year { get; set; }

        public LuxuryLevel LuxuryLevel { get; set; }

        public VehicleSize Size { get; set; }

        public VehicleType Type { get; set; }

        public PriceRange PriceRange { get; set; }

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
