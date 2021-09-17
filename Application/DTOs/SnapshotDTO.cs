using Domain;

namespace Application.DTOs
{
    public class SnapshotDTO
    {
        public string Id { get; set; }

        public long high { get; set; }

        public long highTime { get; set; }

        public long low { get; set; }

        public long lowTime { get; set; }
    }
}