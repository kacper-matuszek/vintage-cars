using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Service.Infrastructure
{
    public interface IInfrastructureService
    {
        DefaultCache Cache { get; }
        void SetDefaultCache();
    }
}
