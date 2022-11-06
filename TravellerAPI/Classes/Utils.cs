using System.Text.Json;
using TravellerAPI.Models;

namespace TravellerAPI.Classes
{
    public static class Utils
    {
        public static List<City> GetCities(string cityName = "", bool ExactMatch = true)
        {
            var cities = new List<City>();
            var file = File.ReadAllText(@"..\TravellerAPI\Data\cities.json");
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, City>>(file);

            if (dictionary != null)
            {
                foreach (var item in dictionary)
                    cities.Add(item.Value);

                if (!string.IsNullOrWhiteSpace(cityName))
                    if (ExactMatch)
                        cities = cities.Where(c => c.name == cityName).ToList();
                    else
                        cities = cities.Where(c => c.name.Contains(cityName)).ToList();
            }

            return cities;
        }

        public static City GetNextNearestCity(City originCity, IEnumerable<string> continentsToIgnore)
        {
            var cities = GetCities();
            var cityDistance = new Dictionary<City, double>();

            foreach (var continent in continentsToIgnore)
                cities.RemoveAll(c => c.contId == continent);

            foreach (var nextCity in cities)
                cityDistance[nextCity] = GetDistanceInKm(originCity.location.lat, originCity.location.lon, nextCity.location.lat, nextCity.location.lon);

            return cityDistance.MinBy(kvp => kvp.Value).Key;
        }

        public static double GetDistanceInKm(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var radius = 6371;
            var dLat = deg2rad(latitude2 - latitude1);
            var dLon = deg2rad(longitude2 - longitude1);

            var x = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(deg2rad(latitude1)) * Math.Cos(deg2rad(latitude2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            return 2 * radius * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));
        }

        public static double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}