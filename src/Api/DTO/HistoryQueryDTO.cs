namespace Exadel.Forecast.Api.DTO
{
    public class HistoryQueryDTO : BaseQueryDTO
    {
        public DateTime StartDateTime { get; set; } = DateTime.Now.AddDays(-1);
        public DateTime EndDateTime { get; set; } = DateTime.Now;
    }
}
