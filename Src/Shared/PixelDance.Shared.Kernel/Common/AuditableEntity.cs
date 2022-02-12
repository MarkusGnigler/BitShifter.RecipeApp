#nullable enable
using System;
using PixelDance.Shared.Kernel.Enums;

namespace PixelDance.Shared.Kernel.Common
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; protected set; }
        public PriorityLevel Priority { get; protected set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; internal set; }

        public DateTime? LastModified { get; set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; internal set; }
    }
}
