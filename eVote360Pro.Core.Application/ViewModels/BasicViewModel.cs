using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels
{
    public class BasicViewModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
