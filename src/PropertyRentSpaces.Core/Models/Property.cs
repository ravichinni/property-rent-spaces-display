using System.Collections.Generic;

namespace PropertyRentSpaces.Core.Models
{
    public class Property
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; }
        public List<string> Highlights { get; set; }
        public List<Transportation> Transportation { get; set; }
        public List<Space> Spaces { get; set; }
    }

    public class Transportation
    {
        public string Name { get; set; }
        public string Distance { get; set; }
    }

    public class Space
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RentRoll> RentRoll { get; set; }
    }

    public class RentRoll
    {
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
} 