using System;

namespace BugTrackingAPI.Entities
{
    public class TaskFilter
    {
        public int? Priority { get; set; } = null;
        public int? StatusId { get; set; } = null;
        public DateTime? StartTimeCreate { get; set; } = null;
        public DateTime? EndTimeCreate { get; set; } = null;
        public DateTime? StartTimeUpdate { get; set; } = null;
        public DateTime? EndTimeUpdate { get; set; } = null;
    }
}
