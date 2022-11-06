namespace TravellerAPI.Models
{
    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public string countryName { get; set; }
        public string iata { get; set; }
        public int rank { get; set; }
        public string countryId { get; set; }
        public string dest { get; set; }
        public List<string> airports { get; set; }
        public List<string> images { get; set; }
        public double popularity { get; set; }
        public string regId { get; set; }
        public string contId { get; set; }
        public string subId { get; set; }
        public string terId { get; set; }
        public int con { get; set; }
    }
}