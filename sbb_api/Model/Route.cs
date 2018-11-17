using System;
namespace sbb_api.Model
{
    public class Route
    {
        public City FromCity { get; set;  }
        public City ToCity { get; set; }

        public Route(City fromCity, City toCity)
        {
            this.FromCity = fromCity;
            this.ToCity = toCity;
        }

    }
}
