using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Domain.Extensions
{
    public static class Utils
    {
        public static bool IsEmpty(this Guid guid)
            => guid == Guid.Empty;
    }
}
