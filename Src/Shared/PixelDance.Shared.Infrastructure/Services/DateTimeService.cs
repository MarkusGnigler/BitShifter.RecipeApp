using System;
using PixelDance.Shared.Abstractions.Interfaces;

namespace PixelDance.Shared.Infrastructure.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
