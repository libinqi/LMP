using Abp.Application.Services.Dto;
using LMP.TaskSystem.Domain;

namespace LMP.TaskSystem.Services.Dtos
{
    public class GetTasksInput : IInputDto
    {
        public TaskState? State { get; set; }

        public long? AssignedUserId { get; set; }
    }
}