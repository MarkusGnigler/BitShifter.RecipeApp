using System;
using BitShifter.Shared.Abstractions.Interfaces;

namespace BitShifter.Shared.Infrastructure.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
