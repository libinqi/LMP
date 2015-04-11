using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using LMP.TaskSystem.Domain;

namespace LMP.TaskSystem.Services.Dtos
{
    /// <summary>
    /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
    /// 
    /// Implements <see cref="IInputDto"/>, thus ABP applies standard input process (like automatic validation) for it. 
    /// Implements <see cref="ICustomValidate"/> for additional custom validation.
    /// </summary>
    public class UpdateTaskInput : CreationAuditedEntityDto, ICustomValidate
    {
        public TaskState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (CreatorUserId == null && State == null)
            {
                results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Task!", new[] { "AssignedPersonId", "State" }));
            }
        }

        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, AssignedPersonId = {1}, State = {2}]", Id, CreatorUserId, State);
        }
    }
}
