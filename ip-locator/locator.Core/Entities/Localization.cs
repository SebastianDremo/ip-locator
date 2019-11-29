namespace locator.Core.Entities
{
    public class Localization
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Region { get; set; }
    }
}