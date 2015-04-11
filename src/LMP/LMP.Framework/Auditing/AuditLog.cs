﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Extensions;
using Abp.Auditing;

namespace LMP.Auditing
{
    /// <summary>
    /// Used to store audit logs.
    /// </summary>
    [Table("LMPAuditLogs")]
    public class AuditLog : Entity<long>
    {
        /// <summary>
        /// Maximum length of <see cref="ServiceName"/> property.
        /// </summary>
        public const int MaxServiceNameLength = 256;

        /// <summary>
        /// Maximum length of <see cref="MethodName"/> property.
        /// </summary>
        public const int MaxMethodNameLength = 256;

        /// <summary>
        /// Maximum length of <see cref="Parameters"/> property.
        /// </summary>
        public const int MaxParametersLength = 1024;

        /// <summary>
        /// Maximum length of <see cref="ClientIpAddress"/> property.
        /// </summary>
        public const int MaxClientIpAddressLength = 64;

        /// <summary>
        /// Maximum length of <see cref="ClientName"/> property.
        /// </summary>
        public const int MaxClientNameLength = 128;

        /// <summary>
        /// Maximum length of <see cref="BrowserInfo"/> property.
        /// </summary>
        public const int MaxBrowserInfoLength = 256;

        /// <summary>
        /// Maximum length of <see cref="Exception"/> property.
        /// </summary>
        public const int MaxExceptionLength = 2000;

        /// <summary>
        /// TenantId.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// UserId.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        [MaxLength(MaxServiceNameLength)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Executed method name.
        /// </summary>
        [MaxLength(MaxMethodNameLength)]
        public string MethodName { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        [MaxLength(MaxParametersLength)]
        public string Parameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// Total duration of the method call as milliseconds.
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        [MaxLength(MaxClientIpAddressLength)]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        [MaxLength(MaxClientNameLength)]
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        [MaxLength(MaxBrowserInfoLength)]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Exception object, if an exception occured during execution of the method.
        /// </summary>
        [MaxLength(MaxExceptionLength)]
        public string Exception { get; set; }

        /// <summary>
        /// Creates a new CreateFromAuditInfo from given <see cref="auditInfo"/>.
        /// </summary>
        /// <param name="auditInfo">Source <see cref="AuditInfo"/> object</param>
        /// <returns>The <see cref="AuditLog"/> object that is created using <see cref="auditInfo"/></returns>
        public static AuditLog CreateFromAuditInfo(AuditInfo auditInfo)
        {
            var exceptionMessage = auditInfo.Exception != null ? auditInfo.Exception.ToString() : null;
            return new AuditLog
                   {
                       TenantId = auditInfo.TenantId,
                       UserId = auditInfo.UserId,
                       ServiceName = auditInfo.ServiceName.TruncateWithPostfix(MaxServiceNameLength),
                       MethodName = auditInfo.MethodName.TruncateWithPostfix(MaxMethodNameLength),
                       Parameters = auditInfo.Parameters.TruncateWithPostfix(MaxParametersLength),
                       ExecutionTime = auditInfo.ExecutionTime,
                       ExecutionDuration = auditInfo.ExecutionDuration,
                       ClientIpAddress = auditInfo.ClientIpAddress.TruncateWithPostfix(MaxClientIpAddressLength),
                       ClientName = auditInfo.ClientName.TruncateWithPostfix(MaxClientNameLength),
                       BrowserInfo = auditInfo.BrowserInfo.TruncateWithPostfix(MaxBrowserInfoLength),
                       Exception = exceptionMessage.TruncateWithPostfix(MaxExceptionLength)
                   };
        }

        public override string ToString()
        {
            return string.Format(
                "AUDIT LOG: {0}.{1} is executed by user {2} in {3} ms from {4} IP address.",
                ServiceName, MethodName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }
}
