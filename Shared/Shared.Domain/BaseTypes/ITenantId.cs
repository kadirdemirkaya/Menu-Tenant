using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.BaseTypes
{
    public interface ITenantId
    {
        public string TenantId { get; set; }
    }
}
