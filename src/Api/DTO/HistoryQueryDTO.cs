namespace Exadel.Forecast.Api.DTO
{
    public class HistoryQueryDTO : BaseQueryDTO
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
