using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum OrderState
    {
        Ordered = 0,
        Send = 1,
        Delivered = 2,
        ReportedAsLost = 3,
        Lost = 4
    }
}
