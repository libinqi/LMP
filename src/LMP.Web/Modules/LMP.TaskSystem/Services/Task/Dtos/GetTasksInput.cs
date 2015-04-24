using Abp.Application.Services.Dto;
using LMP.TaskSystem.Domain;

namespace LMP.TaskSystem.Services.Dtos
{
    public class GetTasksInput : EntityRequestInput
    {
        public TaskState? State { get; set; }

        public long? CreatorUserId { get; set; }
    }
}