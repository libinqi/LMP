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
    public class TaskDto : CreationAuditedEntityDto<int>
    {
        public string Description { get; set; }
        public byte State { get; set; }
        public string CreatorUserName { get; set; }
    }
}