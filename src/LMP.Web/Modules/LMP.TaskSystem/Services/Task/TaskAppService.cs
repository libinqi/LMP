using System.Collections.Generic;
using Abp.AutoMapper;
using AutoMapper;
using System.Linq;
using System.Linq.Dynamic;
using Abp.EntityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using LMP.TaskSystem.Domain;
using LMP.TaskSystem.Services.Dtos;
using LMP.Core.Security.Users;

namespace LMP.TaskSystem.Services
{
    /// <summary>
    /// Implements <see cref="ITaskAppService"/> to perform task related application functionality.
    /// 
    /// Inherits from <see cref="ApplicationService"/>.
    /// <see cref="ApplicationService"/> contains some basic functionality common for application services (such as logging and localization).
    /// </summary>
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        //These members set in constructor using constructor injection.

        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<User, long> _userRepository;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public TaskAppService(ITaskRepository taskRepository, IRepository<User, long> userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            //var users = _userRepository.GetAll().ToList();
            //Called specific GetAllWithPeople method of task repository.
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedUserId, input.State);

            //Used AutoMapper to automatically convert List<Task> to List<TaskDto>.
            var tasksOutput = new GetTasksOutput
             {
                 Tasks = tasks.MapTo<List<TaskDto>>()
             };

            return tasksOutput;
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Updating a task for input: " + input);

            //Retrieving a task entity with given id using standard Get method of repositories.
            var task = _taskRepository.Get(input.Id);

            //Updating changed properties of the retrieved task entity.

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }

            if (input.CreatorUserId.HasValue)
            {
                task.CreatorUser = _userRepository.Load(input.CreatorUserId.Value);
            }

            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }

        public void CreateTask(CreateTaskInput input)
        {
            //We can use Logger, it's defined in ApplicationService class.
            Logger.Info("Creating a task for input: " + input);

            //Creating a new Task entity with given input's properties
            var task = new Task { Description = input.Description };

            if (input.AssignedUserId.HasValue)
            {
                task.AssignedUserId = input.AssignedUserId.Value;
            }

            //Saving entity with standard Insert method of repositories.
            _taskRepository.Insert(task);
        }
    }
}