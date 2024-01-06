using Microsoft.AspNetCore.Identity;

namespace mapkowanie.Models
{
    public class Session
    {
        public int Id { get; set; }

        public string? userId { get; set; }
        public virtual IdentityUser? user { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
