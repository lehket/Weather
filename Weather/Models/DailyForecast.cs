namespace Weather.Models
{
    public class DailyForecast
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Temperature { get; set; }
        public String WindSpeed { get; set; }
        public String WindDirection { get; set; }
        public String ShortForecast { get; set; }

    }
}
