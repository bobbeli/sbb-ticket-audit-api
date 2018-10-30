using System;
namespace sbb_api.Model
{
    public class City
    {
        public string Name { get; private set; }
        public int Population { get; private set; }

        public City(string name, int population)
        {
            Name = name;
            Population = population;        
        }
    }
}
