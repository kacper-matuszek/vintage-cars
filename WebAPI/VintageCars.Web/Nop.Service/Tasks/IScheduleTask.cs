﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Service.Tasks
{
    /// <summary>
    /// Interface that should be implemented by each task
    /// </summary>
    public partial interface IScheduleTask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        void Execute();
    }
}
