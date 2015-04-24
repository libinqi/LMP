using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace LMP.TaskSystem.Services.Dtos
{
    public class CreateTaskInput : CreationAuditedEntityDto
    {
        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, Description = {1}]", CreatorUserId, Description);
        }
    }
}