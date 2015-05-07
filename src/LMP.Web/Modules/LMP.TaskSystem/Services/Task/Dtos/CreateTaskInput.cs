using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace LMP.TaskSystem.Services.Dtos
{
    public class CreateTaskInput : IInputDto
    {
        public long? AssignedUserId { get; set; }

        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedUserId = {0}, Description = {1}]", AssignedUserId, Description);
        }
    }
}