using Microsoft.AspNetCore.Identity;

namespace mapkowanie.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Series { get; set; }
        public int ExcerciceTypeId { get; set; }
        public virtual ExcerciceType? ExcerciceType { get; set; }
        public int SessionId { get; set; }
        public virtual Session? Session { get; set; }

        public string? userId { get; set; }
        public virtual IdentityUser? user { get; set; }
    }
}
