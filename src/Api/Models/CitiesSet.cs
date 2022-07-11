using System.Text;

namespace Exadel.Forecast.Api.Models
{
    public class CitiesSet
    {
        public City[] Cities { get; set; } = Array.Empty<City>();

        public string GetAllTimerAsString()
        {
            StringBuilder sb = new();
            foreach (int timer in Cities.Select(c => c.Timer).OrderBy(c=>c).Distinct())
            {
                sb.Append(timer);
            }

            return sb.ToString();
        }
    }
}
