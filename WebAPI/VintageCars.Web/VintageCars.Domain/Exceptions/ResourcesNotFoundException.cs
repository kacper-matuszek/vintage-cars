using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Domain.Exceptions
{
    public class ResourcesNotFoundException : Exception
    {
        public Type ResourceType { get; }

        #region Ctor

        public ResourcesNotFoundException(Type resourceType)
        {
            ResourceType = resourceType;
        }
        public ResourcesNotFoundException(string message)
            : base(message)
        {
        }

        public ResourcesNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
        public ResourcesNotFoundException(Type resourceType, string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ResourcesNotFoundException(Type resourceType, string message)
            : base(message)
        {
            ResourceType = resourceType;
        }
        #endregion
    }
}
