using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FluentScheduler;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Logging;

namespace Nop.Service.Tasks
{
    /// <summary>
    /// Represents task manager
    /// </summary>
    public partial class TaskManager
    {
        #region Fields
        private Registry _registry = new Registry();
        private readonly ILogger _logger;
        #endregion
        #region Ctor

        private TaskManager()
        {
            _logger = EngineContext.Current.Resolve<ILogger>();
            ConfigureJobManager();
        }

        #endregion

        #region Properties
        public static TaskManager Instance { get; } = new TaskManager();
        #endregion

        #region Methods

        /// <summary>
        /// Initializes the task manager
        /// </summary>
        public void Initialize()
        {
            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            var taskService = EngineContext.Current.Resolve<IScheduleTaskService>();

            var scheduleTasks = taskService
                .GetAllTasks()
                .OrderBy(x => x.Seconds)
                .ToList();
            _registry = new Registry();
            foreach (var scheduleTask in scheduleTasks)
            {
                var instance = EngineContext.Current.Resolve(Type.GetType(scheduleTask.Type)) as IJob;
                if (instance == null) 
                    continue;

                _registry.Schedule(instance).ToRunEvery(scheduleTask.Seconds);
            }
        }

        /// <summary>
        /// Starts the task manager
        /// </summary>
        public void Start()
        {
            JobManager.Initialize(_registry);
        }

        /// <summary>
        /// Stops the task manager
        /// </summary>
        public void Stop()
        {
            JobManager.StopAndBlock();
        }

        private void ConfigureJobManager()
        {
            JobManager.JobStart += info => _logger.Information($"{info.Name}: started");
            JobManager.JobEnd += info => _logger.Information($"{info.Name}: ended ({info.Duration})");
            JobManager.JobException += error => _logger.Error($"An error happened with a scheduled job: {error.Exception}");
        }
        #endregion

    }
}
