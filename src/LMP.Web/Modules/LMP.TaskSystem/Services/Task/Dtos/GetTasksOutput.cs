using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace LMP.TaskSystem.Services.Dtos
{
    public class GetTasksOutput : IOutputDto
    {
        public List<TaskDto> Tasks { get; set; }
    }
}