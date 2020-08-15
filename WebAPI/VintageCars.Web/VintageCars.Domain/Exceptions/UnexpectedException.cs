using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Domain.Exceptions
{
    public class UnexpectedException : Exception
    {
        #region Ctor
        public UnexpectedException()
        {
                
        }

        public UnexpectedException(string message)
            : base(message)
        {
                
        }
        #endregion
    }
}
