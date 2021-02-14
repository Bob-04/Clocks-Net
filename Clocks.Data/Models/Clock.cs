using System;

namespace Clocks.Data.Models
{
    public class Clock
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string TimeZoneId { get; set; }
    }
}
