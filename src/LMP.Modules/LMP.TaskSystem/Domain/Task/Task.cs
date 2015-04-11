using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LMP.TaskSystem.Domain;
using LMP.Core.Security.Users;

namespace LMP.TaskSystem.Domain
{
    [Table("Tasks")]
    public class Task : CreationAuditedEntity<int, User>
    {
        /// <summary>
        /// Describes the task.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Current state of the task.
        /// </summary>
        public virtual TaskState State { get; set; }

        /// <summary>
        /// Default costructor.
        /// Assigns some default values to properties.
        /// </summary>
        public Task()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Active;
        }
    }
}
