namespace WebApplication1.Entities
{
    public class Weather
    {

    }

    public class City
    {
        public string insee { get; set; }
        public int cp { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int altitude { get; set; }
    }

    public class Forecast
    {
        public string insee { get; set; }
        public int cp { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int day { get; set; }
        public string datetime { get; set; }
        public int wind10m { get; set; }
        public int gust10m { get; set; }
        public int dirwind10m { get; set; }
        public float rr10 { get; set; }
        public float rr1 { get; set; }
        public int probarain { get; set; }
        public int weather { get; set; }
        public int tmin { get; set; }
        public int tmax { get; set; }
        public int sun_hours { get; set; }
        public int etp { get; set; }
        public int probafrost { get; set; }
        public int probafog { get; set; }
        public int probawind70 { get; set; }
        public int probawind100 { get; set; }
        public int gustx { get; set; }
    }

    public class WeatherResponse
    {
        public City city { get; set; }
        public DateTime update { get; set; }
        //public Forecast forecast { get; set; }
        public List<Forecast> forecast { get; set; }
    }
}
