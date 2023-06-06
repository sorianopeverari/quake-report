using QuakeReport.Domain.Models.Enums;

namespace QuakeReport.Domain.Models
{
    public class Kill
    {
        public Player? Killer { get; set; }
        public Player? Killed { get; set; }
        public DeathCause DeathCause { get; set; }
    }
}

