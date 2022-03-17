using System;

namespace BitShifter.Shared.Kernel.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base("Entity was not found.") { }

        public EntityNotFoundException(string message)
            : base(message)
        { }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public EntityNotFoundException(string name, object searchKey)
            : base($"Entity \"{name}\" ({searchKey}) was not found.")
        { }
    }
}
