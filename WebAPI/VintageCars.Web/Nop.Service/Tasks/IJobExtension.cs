using System;
using FluentScheduler;

namespace Nop.Service.Tasks
{
    public interface IJobExtension : IJob
    {
        event EventHandler OnAfterExecute;
        event EventHandler OnBeforeExecute;
    }
}
