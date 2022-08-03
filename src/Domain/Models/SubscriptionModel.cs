using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.Domain.Models
{
    public class SubscriptionModel : BaseModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Cities { get; set; } = new List<string>();
        public int Hours { get; set; }
    }
}
