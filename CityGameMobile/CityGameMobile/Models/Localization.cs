using System.Collections.Generic;

namespace CityGameMobile.Models
{
    public class Localization
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PlaceName { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}