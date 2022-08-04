using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Validators
{
    public class ListMembership : IValidator<int>
    {
        private readonly List<int> _list;
        public ListMembership(IEnumerable<int> list)
        {
            _list = list.ToList();
        }

        public bool IsValid(int value)
        {
            return _list.Contains(value);
        }
    }
}
