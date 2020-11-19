using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuideServiceAPI.Models
{
    public class Vehicle
    {
        public object Id { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public LuxuryLevel LuxuryLevel { get; set; }

        public VehicleSize Size { get; set; }

        public VehicleType Type { get; set; }

        public PriceRange PriceRange { get; set; }

        public List<VehicleReview> VehicleReview { get; set; }

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


    public enum LuxuryLevel
    {
        Normal = 0,
        Luxury = 1,
        Sport = 2

    }

    public enum VehicleSize
    {
        SubCompact = 0,
        Compact = 1,
        Midsize = 2,
        Large = 3

    }

    public enum VehicleType
    {
        Sedan = 0,
        Coupe = 1,
        Hatchback = 2,
        SUV = 3,
        PickUp = 4,
        Minivan = 5

    }

    public enum PriceRange
    {
        Under30g = 0,
        Between30And50g = 1,
        Between50gAnd80g = 2,
        Between80gAnd100g = 3,
        Above100g = 4
    }

}
