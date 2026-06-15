using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Dtos
{
    public class BasicDto<TKey>
    {
        public required TKey Id { get; set; }
    }
}
