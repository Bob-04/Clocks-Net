using System;

namespace Clocks.Shared.DtoModels
{
    public class ClockDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string TimeZoneId { get; set; }

        public DateTime CurrentTime { get; set; }
    }
}
