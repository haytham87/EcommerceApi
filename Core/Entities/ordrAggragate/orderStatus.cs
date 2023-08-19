using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ordrAggragate
{
    public enum orderStatus
    {
        [EnumMember(Value ="pending")]
        pending,
        [EnumMember(Value = "succses")]
        succses,
        [EnumMember(Value = "failed")]
        failed
    }
}
