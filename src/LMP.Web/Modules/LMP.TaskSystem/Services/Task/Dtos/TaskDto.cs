using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LMP.TaskSystem.Domain;

namespace LMP.TaskSystem.Services.Dtos
{
    /// <summary>
    /// A DTO class that can be used in various application service methods when needed to send/receive Task objects.
    /// </summary>
    [AutoMapFrom(typeof(Task))]
    public class TaskDto : EntityDto<long>
    {
        public long? AssignedUserId { get; set; }

        public string AssignedUserName { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public byte State { get; set; }
    }
}