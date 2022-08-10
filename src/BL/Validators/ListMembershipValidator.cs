using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Validators
{
    public class ListMembershipValidator : IValidator<int>
    {
        private readonly List<int> _list;
        public ListMembershipValidator(IEnumerable<int> list)
        {
            _list = list.ToList();
        }

        public bool IsValid(int value)
        {
            return _list.Contains(value);
        }
    }
}
